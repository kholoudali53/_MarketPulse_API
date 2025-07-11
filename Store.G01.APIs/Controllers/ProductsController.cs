using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G01.APIs.Attributes;
using Store.G01.APIs.Errors;
using Store.G01.Core.Dtos.Products;
using Store.G01.Core.Helper;
using Store.G01.Core.Services.Contract;
using Store.G01.Core.Specifications.ProductSpecification;

namespace Store.G01.APIs.Controllers
{
	public class ProductsController : BaseAPIController
    {
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
        {
			_productService = productService;
		}

		[ProducesResponseType(typeof(PaginationResponse<ProductDto>),StatusCodes.Status200OK)]
		[HttpGet]
		[Cached(100)]
		[Authorize]
		// sort : name, priceAsec, PriceDesc
		public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpecParams) //end point
		{
			var result = await _productService.GetAllProductsAsync(productSpecParams);
			return Ok(result);
		}


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
		{
			var result = await _productService.GetAllBrandsAsync();
			return Ok(result);
		}



        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")]
		public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
		{
			var result = await _productService.GetAllBrandsAsync();
			return Ok(result);
		}



        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
		public async Task<ActionResult<ProductDto>> GetProductById(int? id)
		{
			if (id is null) return BadRequest(new APIErrorResponse(400));
			var result = await _productService.GetProductByIdAsync(id.Value);

			if (result is null) return NotFound(new APIErrorResponse(404, $"the product with id: {id} not found at DB: ("));
			return Ok(result);
		}


	}
}
