using MediatR;
using System.Collections.Generic;
using TG.Domain.Models;

namespace TG.Application.Queries
{
	public class GetPlayersByDaysPeriodQuery : IRequest<IEnumerable<Player>>
	{
		public int DaysPeriod { get; set; }

	}
}
