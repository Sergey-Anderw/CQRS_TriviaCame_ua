using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TG.Application.Queries;
using System.Diagnostics.CodeAnalysis;
using System;

namespace Gateway.Controllers
{
	[ApiController]
	public class GameController : ControllerBase
	{
		private readonly ILogger<GameController> _logger;
		private readonly IMediator _mediator;

		public GameController(
			[NotNull] IMediator mediator,
			[NotNull] ILogger<GameController> logger)
		{
			if (mediator == null)
				throw new ArgumentNullException(nameof(mediator));
			_mediator = mediator;
			_logger = logger;
		}
		
		/// <summary>
		/// Returns the list of all existing categories
		/// </summary>
		[Route("api/Categories")]
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			return Ok(await _mediator.Send(new GetAllCategoriesQuery()));
		}

		/// <summary>
		/// Returns random question from specified category(answers array included)
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		[Route("api/Questions/By_Category/{categoryId}")]
		[HttpGet]
		public async Task<IActionResult> GetQuestionsByCategoryId(int categoryId)
		{
			return Ok(await _mediator.Send(new GetQuestionsByCategoryIdQuery { Id = categoryId }));
		}


		/// <summary>
		/// Returns the list of all players, who played a game for the last time in a specified period (sorted descending). For example, if daysPeriod = 30, all the users who played the game during the last month, should be sorted and returned
		/// </summary>
		/// <param name="daysPeriod"></param>
		/// <returns></returns>
		[Route("api/Players/leaderboard/{daysPeriod}")]
		[HttpGet]
		public async Task<IActionResult> GetPlayersByDaysPeriod(int daysPeriod)
		{
			return Ok(await _mediator.Send(new GetPlayersByDaysPeriodQuery { DaysPeriod = daysPeriod }));
		}
	}
}
