using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TG.Domain.Entities;
using TG.Domain.Interfaces.EF;

namespace TG.Infrastructure
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		/*public ApplicationDbContext()
		{
		}*/

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options){}


		public DbSet<CategoryEntity> Categories { get; set; }
		public DbSet<QuestionEntity> Questions { get; set; }
		public DbSet<AnswerEntity> Answers { get; set; }
		public DbSet<PlayerEntity> Players { get; set; }
		public DbSet<GameplayRoomEntity> GameplayRooms { get; set; }

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

    }
}
