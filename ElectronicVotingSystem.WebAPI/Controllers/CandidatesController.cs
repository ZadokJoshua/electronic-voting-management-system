using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

/// <summary>
/// Controller for managing candidates
/// </summary>
/// <param name="electionRepository"></param>
/// <param name="candidateRepository"></param>
/// <param name="positionRepository"></param>
/// <param name="mapper"></param>
[Authorize]
[Route("api/election/{electionId}/positions/{positionId}/candidates")]
[ApiController]
[Produces("application/json")]
public class CandidatesController(IElectionRepository electionRepository, ICandidateRepository candidateRepository, IPositionRepository positionRepository, IMapper mapper) : ControllerBase
{
    private readonly IElectionRepository _electionRepository = electionRepository;
    public readonly ICandidateRepository _candidateRepository = candidateRepository;
    private readonly IPositionRepository _positionRepository = positionRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Get all candidates contesting for a position
    /// </summary>
    /// <param name="electionId"></param>
    /// <param name="positionId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidatesInAnElection(Guid electionId, Guid positionId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");
        if (!await _positionRepository.ExistsAsync(positionId)) return NotFound($"Position with ID {positionId} not found!");

        var candidates = await _candidateRepository.GetAllCandidatesInElectionAsync(positionId);

        return Ok(candidates);
    }

    /// <summary>
    /// Get a specific candidate by ID
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="candidateId">Candidate ID</param>
    /// <returns></returns>
    [HttpGet("{candidateId}", Name = nameof(GetCandidateById))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Candidate>> GetCandidateById(Guid electionId, Guid positionId, Guid candidateId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");
        if (!await _positionRepository.ExistsAsync(positionId)) return NotFound($"Position with ID {positionId} not found!");

        var candidate = await _candidateRepository.GetACandidateInPositionAsync(positionId, candidateId);

        if (candidate == null) return NotFound($"Candidate with ID {candidateId} not found!");
        return Ok(candidate);
    }

    /// <summary>
    /// Create a new candidate contesting for a position
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="candidateDto">Candidate ID</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateCandidate(Guid electionId, Guid positionId, UpsertCandidateDto candidateDto)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var candidateEntity = _mapper.Map<Candidate>(candidateDto);

        await _positionRepository.AddCandidateToPosition(electionId, positionId, candidateEntity);
        await _positionRepository.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetCandidateById), new
        {
            electionId,
            positionId = candidateEntity.PositionId,
            candidateId = candidateEntity.Id
        },
        candidateEntity);
    }

    /// <summary>
    /// Update candidate
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="candidateId">Candidate ID</param>
    /// <param name="candidateDto">Candidate DTO</param>
    /// <returns></returns>
    [HttpPut("{candidateId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateCandidate(Guid electionId, Guid positionId, Guid candidateId, UpsertCandidateDto candidateDto)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");
        if (!await _positionRepository.ExistsAsync(positionId)) return NotFound($"Position with ID {positionId} not found!");

        var candidateEntity = await _candidateRepository.GetACandidateInPositionAsync(positionId, electionId);

        if (candidateEntity == null) return NotFound($"Candidate with ID {candidateId} not found!");

        if (!ModelState.IsValid) return BadRequest(ModelState);
        _mapper.Map(candidateDto, candidateEntity);

        await _electionRepository.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete candidate
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="candidateId">Candidate ID</param>
    /// <returns></returns>
    [HttpDelete("{candidateId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCandidate(Guid electionId, Guid positionId, Guid candidateId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");
        if (!await _positionRepository.ExistsAsync(positionId)) return NotFound($"Position with ID {positionId} not found!");

        var candidateEntity = await _candidateRepository.GetACandidateInPositionAsync(positionId, candidateId);
        if (candidateEntity == null) return BadRequest($"Candidate with ID {candidateId} not found in Position {positionId}.");

        _candidateRepository.DeleteCandidate(candidateEntity);
        await _positionRepository.SaveChangesAsync();

        return NoContent();
    }
}