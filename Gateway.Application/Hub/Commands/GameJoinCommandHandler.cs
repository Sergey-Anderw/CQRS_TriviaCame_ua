using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TG.Application.Hub.Commands
{
	public class GameJoinCommandHandler : IRequestHandler<GameJoinCommand, Unit>
	{
		public async Task<Unit> Handle(GameJoinCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
