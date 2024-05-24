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
            var elections = await _electionRepository.GetAllElectionsAsync();
            return Ok(elections);
        }

        [HttpGet("{electionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Election>> GetElectionById(Guid electionId)
        {
            var election = await _electionRepository.GetElectionByIdAsync(electionId);

            if (election == null) 
            { 
                return NotFound();
            }

            return Ok(election);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateElection(CreateElectionDto election)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var electionEntity = election.AsEntity();

            _electionRepository.Add(electionEntity);
            await _electionRepository.SaveChangesAsync();

            return Created();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteElection(Guid electionId)
        {
            var election = await _electionRepository.GetElectionByIdAsync(electionId);

            if (election == null) return BadRequest($"Election with ID {electionId} not found!");

            _electionRepository.DeleteElection(election);
            await _electionRepository.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
