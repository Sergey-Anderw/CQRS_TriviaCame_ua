using System.Collections.Generic;
using TG.Domain.Models;

namespace TG.Domain.Interfaces.Hub
{
	public interface IGameRepository
	{
		List<GameplayRoom> GameRooms { get; }
	}
}
