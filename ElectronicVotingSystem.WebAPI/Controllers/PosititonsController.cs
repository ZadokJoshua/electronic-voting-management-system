using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Controllers
{
    [Route("api/elections/{electionId}/positions")]
    [ApiController]
    public class PosititonsController(IPositionRepository positionRepository, IElectionRepository electionRepository) : ControllerBase
    {
        private readonly IPositionRepository _positionRepository = positionRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetAllPositions()
        {
        }
    }
}
