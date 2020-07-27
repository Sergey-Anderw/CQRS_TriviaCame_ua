using System.Threading.Tasks;

namespace TG.Domain.Interfaces.Hub
{
	public interface IGameHub
	{
		Task Join(string characterColor);
		Task Send(string jsonData);
		Task Leave();
	}
}
