using Store.G01.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Specifications.ProductSpecification
{
	public class ProductSpecification : BaseSpecification<Product, int>
	{
        public ProductSpecification(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();

		}
        public ProductSpecification(ProductSpecParams productSpecParams):base(
            P => 
            (!productSpecParams.brandId.HasValue || productSpecParams.brandId==P.BrandId)
            &&
            (!productSpecParams.typeId.HasValue || productSpecParams.typeId == P.TypeId)
            )
        {
            if(!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch(productSpecParams.Sort)
                {
                    case "pricAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
						AddOrderByDesc(p => p.Price);
						break;
                    default:
						AddOrderBy(p => p.Name);
						break;
                }
            }

            ApplyIncludes();

            ApplyPagination(productSpecParams.pageSize * (productSpecParams.pageIndex - 1), productSpecParams.pageSize);
		}
        public void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
