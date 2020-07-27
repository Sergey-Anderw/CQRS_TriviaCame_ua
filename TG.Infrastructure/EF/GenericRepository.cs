using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TG.Domain.Entities;
using TG.Domain.Interfaces;

namespace TG.Infrastructure
{
	public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
	{

		private ApplicationDbContext _context;
		private DbSet<TEntity> _dbSet;
		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();

		}

		public async Task Create(TEntity item)
		{
			await _dbSet.AddAsync(item);
		}

		public async Task<IEnumerable<TEntity>> Get(
			Expression<Func<TEntity, bool>> predicate = null,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
			bool disableTracking = true)
		{
			var query = _dbSet.AsQueryable();
			if (disableTracking)
				query = query.AsNoTracking();

			if (include != null)
				query = include(query);
			if (predicate != null)
				query = query.Where(predicate);

			return await query.ToListAsync();
		}


		public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
		{
			var query = _dbSet.AsQueryable();
			return await query.Where(predicate).ToListAsync();
		}

		public async Task<TEntity> GetById(int id,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
			bool disableTracking = true)
		{
			var query = _dbSet.AsQueryable();
			if (disableTracking)
			{
				query = query.AsNoTracking();
			}
			if (include != null)
			{
				query = include(query);
			}

			return await query.SingleOrDefaultAsync(e => e.Id == id);
		}

		public void Update(TEntity item)
		{
			_dbSet.Update(item);
		}

		public void Delete(TEntity item)
		{
			_dbSet.Attach(item);
		    _dbSet.Remove(item);
		}
	}
}
