using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TG.Application.Hub.Commands
{
	public class GameJoinCommandHandler : IRequestHandler<GameJoinCommand>
	{
		private readonly ILogger<GameJoinCommandHandler> _logger;//TO DO
		private readonly IMediator _mediator;
		public GameJoinCommandHandler(
			IMediator mediator,
			ILogger<GameJoinCommandHandler> logger)
		{
			if (mediator == null)
				throw new ArgumentNullException(nameof(mediator));
			_mediator = mediator;
			_logger = logger;
		}

		public Task<Unit> Handle(GameJoinCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		/*public async Task<Unit> Handle(GameJoinCommand request, CancellationToken cancellationToken)
		{
			var joinCommand = new JoinCommand();
			return await _mediator.Send()
		}*/

	}
}
