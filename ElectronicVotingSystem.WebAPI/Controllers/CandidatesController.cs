using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers
{
    [Route("api/election/{electionId}/positions/{postitionId}/candidates")]
    [ApiController]
    public class CandidatesController(ICandidateRepository candidateRepository, IElectionRepository electionRepository, IPositionRepository positionRepository) : ControllerBase
    {
        public readonly ICandidateRepository _candidateRepository = candidateRepository;
        private readonly IElectionRepository _electionRepository = electionRepository;
        private readonly IPositionRepository _positionRepository = positionRepository;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidatesInAnElection(Guid electionId, Guid postitionId)
        {
            if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");
            if (!await _positionRepository.ExistsAsync(postitionId)) return NotFound($"Position with ID {postitionId} not found!");



            return Ok(new() { });
        }

        //[HttpGet("{candidateId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Candidate>> GetCandidateInAnElectionById(Guid electionId, Guid candidateId)
        //{
        //    if (!(await ElectionExists(electionId)))
        //    {
        //        return NotFound();
        //    }
        //}

    }
}
