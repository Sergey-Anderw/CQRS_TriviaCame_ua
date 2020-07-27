using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TG.Domain.Entities;

namespace TG.Domain.Interfaces
{
	public interface IRepository<TEntity> where TEntity : EntityBase
	{
		Task<TEntity> GetById(
			int id,
			Func<IQueryable<TEntity>,
				IIncludableQueryable<TEntity, object>> include = null,
			bool disableTracking = true);

		Task<IEnumerable<TEntity>> Get(
			Expression<Func<TEntity, bool>> predicate = null,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
			bool disableTracking = true);

		Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);

		Task Create(TEntity item);

		void Update(TEntity item);

		void Delete(TEntity item);

	}
}
