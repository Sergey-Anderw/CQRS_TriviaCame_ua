using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TG.Domain.Entities;
using TG.Infrastructure.EF;

namespace TG.Application.Hub.Commands.Leave
{
	public class LeaveCommandHandler : IRequestHandler<LeaveCommand, int>
	{
		private readonly ILogger<LeaveCommandHandler> _logger;//TO DO
		private UnitOfWork _unitOfWork;

		public LeaveCommandHandler(ILogger<LeaveCommandHandler> logger,
			UnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}
		public async Task<int> Handle(LeaveCommand request, CancellationToken cancellationToken)
		{
			using (_unitOfWork)
			{
				var gameplayRoomEntity = (await _unitOfWork
					.GameplayRoomRepository
					.Get(gr => gr.Players.Any<PlayerEntity>(p => p.ConnectionId == request.ConnectionId),
					gr => gr.Include(p => p.Players)))
					.FirstOrDefault();

					if (gameplayRoomEntity is null)
						throw new ArgumentNullException(nameof(GameplayRoomEntity));
				_unitOfWork.GameplayRoomRepository.Delete(gameplayRoomEntity);
				await _unitOfWork.Save();

				return gameplayRoomEntity.Id;
			}
		}
	}
}
