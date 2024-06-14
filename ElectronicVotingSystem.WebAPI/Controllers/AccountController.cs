using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Enums;
using ElectronicVotingSystem.WebAPI.Models;
using ElectronicVotingSystem.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

/// <summary>
/// Controller for managing user account operations such as registration and login
/// </summary>
[AllowAnonymous]
[Route("api/account")]
[ApiController]
public class AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService) : ControllerBase
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Registers a new user with the provided details
    /// </summary>
    /// <param name="userDto">User DTO</param>
    /// <returns>A JWT Token</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] UpsertAppUserDto userDto)
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
                // await _userManager.AddToRoleAsync(user, userRole); // We need ensure that the role exist in the APPROLe db before we create it

                var token = _authService.GenerateToken(userDto.UserName, user.Id, userRole);
                return Ok(new { token });
            }

            foreach(var error in result.Errors) ModelState.AddModelError("", error.Description);
        }

        return BadRequest(ModelState);
    }

    /// <summary>
    /// Logs in a user with the provided credentials
    /// </summary>
    /// <param name="loginDto">The login credentials of the user</param>
    /// <returns>A JWT Token</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
