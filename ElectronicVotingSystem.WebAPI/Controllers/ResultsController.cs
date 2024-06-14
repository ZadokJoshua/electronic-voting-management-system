using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

[Authorize]
[Route("api/election/{electionId}/results")]
[ApiController]
[Produces("application/json")]
public class ResultsController : ControllerBase
{

}
