using AutoMapper;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;
using BugTrackerAPI.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) return BadRequest("Username is taken.");

            var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existingUser != null) return Unauthorized("Use a different email address.");

            var user = _mapper.Map<User>(registerDto);
            user.UserName = registerDto.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest("Invalid registration attempt.");

            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.JobTitle);

            if (!roleResult.Succeeded) return BadRequest("Invalid registration attempt.");

            var permissions = "";
            if (user.JobTitle == "Developer")
            {
                permissions = "Normal";
            }
            else
            {
                permissions = "Manager";
            }

            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Permissions = permissions,
                JobTitle = user.JobTitle,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null) return Unauthorized("User with that email not found.");

            var result = await _signInManager
             .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid login attempt.");

            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Permissions = "Normal",
                JobTitle = user.JobTitle,
                Token = await _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}
