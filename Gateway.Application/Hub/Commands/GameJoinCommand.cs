using MediatR;

namespace TG.Application.Hub.Commands
{
	public class GameJoinCommand : IRequest
	{
		public string CharacterColor { get; set; }
		public string ConnectionId { get; set; }
}
}
