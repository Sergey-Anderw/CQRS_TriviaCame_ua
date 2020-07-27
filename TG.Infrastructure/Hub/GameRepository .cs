using System;
using System.Collections.Generic;
using System.Text;
using TG.Domain.Interfaces.Hub;
using TG.Domain.Models;

namespace TG.Infrastructure.Hub
{
	public class GameRepository : IGameRepository
	{
		public List<GameplayRoom> GameRooms { get; } = new List<GameplayRoom>();
	}
}
