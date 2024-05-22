using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Extensions;
using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers
{
    [Route("api/elections")]
    [ApiController]
    [Produces("application/json")]
    public class ElectionsController(IElectionRepository electionRepository) : ControllerBase
    {
        private readonly IElectionRepository _electionRepository = electionRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Election>>> GetAllElections()
        {
            var elections = await _electionRepository.GetAllAsync();
            return Ok(elections);
        }

        [HttpGet("{electionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Election>> GetElectionById(Guid electionId)
        {
            var election = await _electionRepository.GetByIdAsync(electionId);

            if (election == null) 
            { 
                return NotFound();
            }

            return Ok(election);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateElection(CreateElectionDto election)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var electionEntity = new Election
            {
                Name = election.Name,
                Description = election.Description,
                EndDate = election.EndDate,
                StartDate = election.StartDate,
                HasVotingEnded = election.HasVotingEnded,
                ImageUrl = election.ImageUrl,
                Instructions = election.Instructions,
                IsVotingOn = election.IsVotingOn
            };

            await _electionRepository.AddAsync(electionEntity);
            await _electionRepository.SaveChangesAsync();

            var createdElectionToReturn = electionEntity.AsDto();

            return CreatedAtRoute(nameof(GetElectionById),
                new { id = electionEntity.Id }, createdElectionToReturn);
        }
    }
}
