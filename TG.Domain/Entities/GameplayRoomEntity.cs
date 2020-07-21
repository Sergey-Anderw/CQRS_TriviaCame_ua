using System;
using System.Collections.Generic;

namespace TG.Domain.Entities
{
	public class GameplayRoomEntity : EntityBase
	{
		private List<PlayerEntity> _player;
		public GameplayRoomEntity()
		{
			_player = new List<PlayerEntity>();
		}
		public int MaxPlayers { get; set; }
		public List<PlayerEntity> Players
		{
			get => _player;
			set => _player = value ?? throw new ArgumentNullException(nameof(value));
		}

	}
}
