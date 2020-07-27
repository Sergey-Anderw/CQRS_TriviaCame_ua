using MediatR;
using TG.Domain.Models;

namespace TG.Application.Hub.Commands
{
	public class JoinCommand : IRequest<GameplayRoom>
	{
		public string CharacterColor { get; set; }
		public string ConnectionId { get; set; }
	}
}
