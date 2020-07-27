using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TG.Domain.Interfaces.Hub
{
	public interface IGameClient
	{
		Task OpponentJoined(string name, string characterColor, bool isGameOrganizer);
		Task CanPlay();
		Task OpponentLeave();
		Task Send(string jsonData);
		Task Leave();
	}
}
