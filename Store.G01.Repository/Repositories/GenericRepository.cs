using Microsoft.EntityFrameworkCore;
using Store.G01.Core.Entities;
using Store.G01.Core.Repositories.Contract;
using Store.G01.Core.Specifications;
using Store.G01.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Repository.Repositories
{
	public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
	{
		private readonly StoreDbContext _context;

		public GenericRepository(StoreDbContext context)
        {
			_context = context;
		}
        public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			if(typeof(TEntity) == typeof(Product))
			{
				return (IEnumerable<TEntity>)await _context.Products.OrderBy(p => p.Name).Include(P => P.Brand).Include(P => P.Type).ToListAsync();
			}

			return await _context.Set<TEntity>().ToListAsync();
		}

		public async Task<TEntity?> GetAsync(Tkey id)
		{
			if (typeof(TEntity) == typeof(Product))
			{
				return await _context.Products
					.Where(P => P.Id == id as int?)
					.Include(P => P.Brand)
					.Include(P => P.Type)
					.FirstOrDefaultAsync() as TEntity;
			}
			return await _context.Set<TEntity>().FindAsync(id);
		}
		public async Task AddAsync(TEntity entity)
		{
			await _context.AddAsync(entity);
		}
		public void UpdateAsync(TEntity entity)
		{
			_context.Update(entity);
		}
		public void DeleteAsync(TEntity entity)
		{
			_context.Remove(entity);
		}

		public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, Tkey> spec)
		{
			return await ApplySpecifications(spec).ToListAsync();
		}

		public async Task<TEntity> GetWithSpecAsync(ISpecification<TEntity, Tkey> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}
		public IQueryable<TEntity> ApplySpecifications(ISpecification<TEntity, Tkey> spec)
		{
			return SpecificationEvaluator<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), spec);
		}

		public Task<int> GetCountAsync(ISpecification<TEntity, Tkey> spec)
		{
			return ApplySpecifications(spec).CountAsync();
		}
	}
}
