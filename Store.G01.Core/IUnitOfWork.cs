using Store.G01.Core.Entities;
using Store.G01.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core
{
	public interface IUnitOfWork
	{
		Task<int> CompleteAsync();

		// create Repository<T> And return
		IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
	}
}
