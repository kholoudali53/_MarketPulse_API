using AutoMapper;
using GraduationProject.Core.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.G01.APIs.Errors;
using Store.G01.APIs.Extensions;
using Store.G01.Core.Dtos.Auth;
using Store.G01.Core.Identity;
using Store.G01.Core.Services.Contract;
using Store.G01.Service.Services.Tokens;
using System.Security.Claims;

namespace Store.G01.APIs.Controllers
{
    public class AccountsController : BaseAPIController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(IUserService userService, ITokenService tokenService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("login")] // Post : /api/Accounts/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto);
            if (user is null) return Unauthorized(new APIErrorResponse(StatusCodes.Status401Unauthorized));

            return Ok(user);
        }

        [HttpPost("register")] // Post : /api/Accounts/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterSync(registerDto);
            if (user is null) return Unauthorized(new APIErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registration !!"));

            return Ok(user);
        }

        [HttpGet("GetCurrentUser")] // Get : /api/Accounts/GetCurrentUser
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return BadRequest(new APIErrorResponse(StatusCodes.Status400BadRequest));

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null) return BadRequest(new APIErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)

            });
        }

        [HttpGet("Address")] // Get : /api/Accounts/Address
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            if (user is null) return BadRequest(new APIErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<AddressDto>(user.Address));
        }

        // task : Update User Address
    }
}
