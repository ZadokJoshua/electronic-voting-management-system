using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Enums;
using ElectronicVotingSystem.WebAPI.Models;
using ElectronicVotingSystem.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElectronicVotingSystem.WebAPI.Controllers;

[AllowAnonymous]
[Route("api/account")]
[ApiController]
public class AccountController(UserManager<AppUser> userManager, IAuthService authService) : ControllerBase
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AddOrUpdateAppUserDto userDto)
    {
        if (ModelState.IsValid)
        {
            var existedUser = await _userManager.FindByNameAsync(userDto.UserName);
            if (existedUser != null)
            {
                ModelState.AddModelError("", "User name is already taken");

                return BadRequest(ModelState);
            }

            var user = new AppUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                About = userDto.About,
                Role = Roles.User.ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                var userRole = Roles.User.ToString();
                await _userManager.AddToRoleAsync(user, userRole);

                var token = _authService.GenerateToken(userDto.UserName, user.Id, userRole);
                return Ok(new { token });
            }

            foreach(var error in result.Errors) ModelState.AddModelError("", error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.
                Password))
            {
                var token = _authService.GenerateToken(user.UserName!, user.Id, user.Role);
                return Ok(new { token });
            }

            ModelState.AddModelError("", "Invalid username or password");
        }

        return BadRequest(ModelState);
    }
}
