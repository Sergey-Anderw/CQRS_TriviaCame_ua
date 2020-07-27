using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TG.Domain.Entities;
using TG.Domain.Models;
using TG.Infrastructure.EF;

namespace TG.Application.Hub.Commands
{
	public class JoinCommandHandler : IRequestHandler<JoinCommand, GameplayRoom>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<JoinCommandHandler> _logger;//TO DO
		private UnitOfWork _unitOfWork;

 		public JoinCommandHandler(
			ILogger<JoinCommandHandler> logger,
			IMapper mapper,
			UnitOfWork unitOfWork)
		{
			_logger = logger;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<GameplayRoom> Handle(JoinCommand request, CancellationToken cancellationToken)
		{
			using(_unitOfWork)
			{
				var playerEntitys = await _unitOfWork
					.PlayerRepository
					.Get(p => p.ConnectionId == request.ConnectionId) as List<PlayerEntity>;

				PlayerEntity playerEntity = null;
				if (!playerEntitys.Any())
				{
					playerEntity = new PlayerEntity
					{
						CharacterColor = request.CharacterColor,
						ConnectionId = request.ConnectionId,
						LastGameDate = DateTime.Now,
						Score = 0,
					};

					await _unitOfWork.PlayerRepository.Create(playerEntity);
					
				}

				var gameplayRoomEntity = (await _unitOfWork
					.GameplayRoomRepository
					.Get(gr => gr
					.Players.Count == 1, gr => gr.Include(p => p.Players), false)
					as List<GameplayRoomEntity>)
					.FirstOrDefault();

				if(gameplayRoomEntity is null)
				{
					playerEntity.Name = $"Player 1 ({request.CharacterColor})";
					playerEntity.IsGameOrganizer = true;
					await _unitOfWork.PlayerRepository.Create(playerEntity);

					gameplayRoomEntity = new GameplayRoomEntity
					{
						Players = new List<PlayerEntity> { playerEntity }
					};
					await _unitOfWork.GameplayRoomRepository.Create(gameplayRoomEntity);

				}
				else 
				{
					playerEntity.Name = playerEntity.Name != null ? playerEntity.Name :  $"Player 2 ({request.CharacterColor})";
					playerEntity.IsGameOrganizer = false;
					gameplayRoomEntity.Players.Add(playerEntity);
					_unitOfWork.GameplayRoomRepository.Update(gameplayRoomEntity);
				}

				await _unitOfWork.Save();
				return _mapper.Map<GameplayRoom>(gameplayRoomEntity);
			}

		}
	}
}
