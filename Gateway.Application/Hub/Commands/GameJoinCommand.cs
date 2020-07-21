using MediatR;

namespace TG.Application.Hub.Commands
{
	public class GameJoinCommand : IRequest<Unit>
	{
		public string CharacterColor { get; set; }
	}
}
