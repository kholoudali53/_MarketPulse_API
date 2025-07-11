using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Specifications.ProductSpecification
{
	public class ProductSpecParams
	{
        public string? Sort { get; set; }
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public int pageSize { get; set; } = 5;
        public int pageIndex { get; set; } = 1;
    }
}
