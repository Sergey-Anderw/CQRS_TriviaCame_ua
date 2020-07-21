using System;

namespace TG.Domain.Entities
{
	public class PlayerEntity : EntityBase
	{
		public string Name { get; set; }
		public int Score { get; set; }
		public DateTime LastGameDate { get; set; }
		public bool IsGameOrganizer { get; set; }
		public int ConnectionId { get; set; }
		public string CharacterColor { get; set; }
	}
}
