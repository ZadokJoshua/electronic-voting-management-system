using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

[Authorize]
[Route("api/ballots")]
[ApiController]
public class BallotsController : BaseController
{
    public BallotsController()
    {
        
    }
}
