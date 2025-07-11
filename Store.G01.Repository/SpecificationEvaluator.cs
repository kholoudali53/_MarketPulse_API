using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.G01.Core.Entities;
using Store.G01.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Repository
{
	public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> spec)
		{
			var query = inputQuery.AsQueryable();

			if(spec.Criteria is not null)
			{
				query = query.Where(spec.Criteria);
			}

			if(spec.OrderBy is not null)
			{
				query = query.OrderBy(spec.OrderBy);
			}

			if (spec.OrderByDesc is not null)
			{
				query = query.OrderByDescending(spec.OrderByDesc);
			}
			if(spec.IsPagination)
			{
				query = query.Skip(spec.Skip).Take(spec.Take);
			}

			query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));
			return query;
		}
	}
}
