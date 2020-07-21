using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TG.Application.Hub.Commands;
using TG.Domain.Interfaces.Hub;

namespace TG.Infrastructure.Hub
{
	public class GameHub : Hub<IGameClient>, IGameHub
	{
		private readonly ILogger<GameHub> _logger;
		private readonly IMediator _mediator;
		public GameHub(
			IMediator mediator,
			ILogger<GameHub> logger) 
		{
			if (mediator == null)
				throw new ArgumentNullException(nameof(mediator));
			_mediator = mediator;
			_logger = logger;
		}

		public async Task<object> Join(string characterColor)
		{
			return await _mediator.Send(new GameJoinCommand { CharacterColor = characterColor });

		}

		public Task Leave()
		{
			throw new NotImplementedException();
		}

		public Task Send(string jsonData)
		{
			throw new NotImplementedException();
		}
	}
}
