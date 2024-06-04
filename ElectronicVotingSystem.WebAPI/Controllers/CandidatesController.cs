using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

/// <summary>
/// Controller for managing Candidates
/// </summary>
/// <param name="electionRepository"></param>
/// <param name="candidateRepository"></param>
/// <param name="positionRepository"></param>
[Authorize]
[Route("api/election/{electionId}/positions/{postitionId}/candidates")]
[ApiController]
public class CandidatesController(IElectionRepository electionRepository, ICandidateRepository candidateRepository, IPositionRepository positionRepository) : ControllerBase
{
    private readonly IElectionRepository _electionRepository = electionRepository;
    public readonly ICandidateRepository _candidateRepository = candidateRepository;
    private readonly IPositionRepository _positionRepository = positionRepository;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidatesInAnElection(Guid electionId, Guid postitionId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");
        if (!await _positionRepository.ExistsAsync(electionId)) return NotFound($"Position with ID {postitionId} not found!");

        var candidate = 

        return Ok(new() { });
    }

    [HttpGet("{candidateId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Candidate>> GetCandidateInAnElectionById(Guid electionId, Guid candidateId)
    {
        if (!(await ElectionExists(electionId)))
        {
            return NotFound();
        }
    }

}
