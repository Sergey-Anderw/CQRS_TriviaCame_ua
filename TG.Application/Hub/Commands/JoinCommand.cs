using MediatR;

namespace TG.Application.Hub.Commands
{
	public class JoinCommand : IRequest
	{
		public string CharacterColor { get; set; }
		public int? ConnectionId { get; set; }
	}
}
