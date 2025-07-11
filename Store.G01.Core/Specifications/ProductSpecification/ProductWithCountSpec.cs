using Store.G01.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Specifications.ProductSpecification
{
	public class ProductWithCountSpec : BaseSpecification<Product, int>
	{
		public ProductWithCountSpec(ProductSpecParams productSpecParams) 
			: base(
			     P =>
			          (!productSpecParams.brandId.HasValue || productSpecParams.brandId == P.BrandId)
			          &&
			          (!productSpecParams.typeId.HasValue || productSpecParams.typeId == P.TypeId)
			)
		{
		}
	}
}
