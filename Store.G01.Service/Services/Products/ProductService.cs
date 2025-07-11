using AutoMapper;
using Store.G01.Core;
using Store.G01.Core.Dtos.Products;
using Store.G01.Core.Entities;
using Store.G01.Core.Helper;
using Store.G01.Core.Services.Contract;
using Store.G01.Core.Specifications;
using Store.G01.Core.Specifications.ProductSpecification;
using Store.G01.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Service.Services.Products
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpecParams)
		{
			var spec = new ProductSpecification(productSpecParams);
			var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
			var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);

			var countSpec = new ProductWithCountSpec(productSpecParams);
			var count = await _unitOfWork.Repository<Product, int>().GetCountAsync(countSpec);

			return new PaginationResponse<ProductDto>(productSpecParams.pageSize, productSpecParams.pageIndex, count, mappedProducts);
			}

		public async Task<ProductDto> GetProductByIdAsync(int id)
		{
			var spec = new ProductSpecification(id);
			return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec));

		}

		public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
		{
			var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
			var mappedBrands = _mapper.Map<IEnumerable<TypeBrandDto>>(brands);
			return mappedBrands;
		}
		
		public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
		{
			return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductType, int>().GetAllAsync());

			
		}

		
		
	}
}
