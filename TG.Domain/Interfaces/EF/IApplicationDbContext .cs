using System.Threading.Tasks;

namespace TG.Domain.Interfaces.EF
{
	public interface IApplicationDbContext
	{
		Task<int> SaveChangesAsync();
	}
}
