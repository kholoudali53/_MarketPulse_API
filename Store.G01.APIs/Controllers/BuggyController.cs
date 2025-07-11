using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G01.APIs.Errors;
using Store.G01.Repository.Data.Contexts;

namespace Store.G01.APIs.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            this._context = context;
        }
        [HttpGet("notfound")] // GET: /api/Buggy/notfound
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brand = await _context.Brands.FindAsync(100);

            if (brand is null) return NotFound(new APIErrorResponse(404, "brand with id : 100 is not found"));

            return Ok(brand);
        }

        [HttpGet("servererror")] // GET: /api/Buggy/servererror
        public async Task<IActionResult> GetServerError()
        {
            var brand = await _context.Brands.FindAsync(100);

            var brandToString = brand.ToString(); //will throw Exeption

            return Ok(brand);
        }

        [HttpGet("badrequest")] // GET: /api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")] // GET: /api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError(int id) //Validation Error
        {
            return BadRequest();
        }

        [HttpGet("Unauthorized")] // GET: /api/Buggy/badrequest
        public async Task<IActionResult> GetError()
        {
            return Unauthorized(new APIErrorResponse(401));
        }
    }
}
