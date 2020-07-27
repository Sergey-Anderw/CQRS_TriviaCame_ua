using System;
using System.Collections.Generic;

namespace TG.Domain.Models
{
	public class GameplayRoom
	{
		private List<Player> _player;
		public GameplayRoom()
		{
			_player = new List<Player>();
		}
		public int Id { get; set; }
		public int MaxPlayers { get; set; }
		public List<Player> Players
		{
			get => _player;
			set => _player = value ?? throw new ArgumentNullException(nameof(value));
		}
	}
}
