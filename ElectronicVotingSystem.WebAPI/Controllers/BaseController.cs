using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElectronicVotingSystem.WebAPI.Controllers;

public class BaseController : ControllerBase
{
    protected string? GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
