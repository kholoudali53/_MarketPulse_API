using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Helper
{
	public class PaginationResponse<TEntity>
	{
		public PaginationResponse(int pageSize, int pageIndex, int count, IEnumerable<TEntity> data)
		{
			this.pageSize = pageSize;
			this.pageIndex = pageIndex;
			Count = count;
			Data = data;
		}

		public int pageSize { get; set; }
		public int pageIndex { get; set; }
		public int Count { get; set; }
		public IEnumerable<TEntity> Data { get; set; }
	}
}
