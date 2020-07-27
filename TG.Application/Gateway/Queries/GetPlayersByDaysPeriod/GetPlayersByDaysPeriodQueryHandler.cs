using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TG.Domain.Entities;
using TG.Domain.Models;
using TG.Infrastructure.EF;

namespace TG.Application.Queries.GetPlayersByDaysPeriod
{
	public class GetPlayersByDaysPeriodQueryHandler : IRequestHandler<GetPlayersByDaysPeriodQuery, IEnumerable<Player>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<GetPlayersByDaysPeriodQueryHandler> _logger;//TO DO
		private UnitOfWork _unitOfWork;

		public GetPlayersByDaysPeriodQueryHandler(
			IMapper mapper,
			UnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Player>> Handle(GetPlayersByDaysPeriodQuery request, CancellationToken cancellationToken)
		{
			using (_unitOfWork)
			{
				var players = await _unitOfWork.PlayerRepository.Get(x => x
					.LastGameDate >= DateTime.Now.AddDays(-request.DaysPeriod));

				return _mapper.Map<List<Player>>(players.ToList().OrderByDescending(p => p.Score));
			}
		}
	}
}
