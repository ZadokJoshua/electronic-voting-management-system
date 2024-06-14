using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;

namespace ElectronicVotingSystem.WebAPI.Controllers;

/// <summary>
/// Ballots Controller
/// </summary>
/// <param name="electionRepository"></param>
/// <param name="ballotRepository"></param>
/// <param name="mapper"></param>
[Authorize]
[Route("api/election/{electionId}/ballots")]
[ApiController]
[Produces("application/json")]
public class BallotsController(IElectionRepository electionRepository, IBallotRepository ballotRepository, IMapper mapper) : BaseController
{
    private readonly IElectionRepository _electionRepository = electionRepository;
    private readonly IBallotRepository _ballotRepository = ballotRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Get a details of a single ballot by ID
    /// </summary>
    /// <param name="electionId"></param>
    /// <param name="ballotId"></param>
    /// <returns></returns>
    [HttpGet("{ballotId}", Name = nameof(GetBallotById))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetBallotById(Guid electionId, Guid ballotId)
    {
        if (!await _electionRepository.ExistsAsync(electionId))
            return NotFound($"Election with ID {electionId} not found!");

        var ballotEntity = await _ballotRepository.GetBallotById(ballotId);

        return Ok(ballotEntity);
    }

    /// <summary>
    /// Cast ballot
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="ballotDto">Ballot ID</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CastBallot(Guid electionId, UpsertBallotDto ballotDto)
    {
        string? userId = GetUserId();
        if (userId == null)
        {
            return NotFound("User not authenticated or found");
        }

        if (!await _electionRepository.ExistsAsync(electionId))
            return NotFound($"Election with ID {electionId} not found!");
        // Other validations from values in the ballot dto

        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var ballotEntity = _mapper.Map<Ballot>(ballotDto);
        ballotEntity.VoterId = userId;
        ballotEntity.ElectionId = electionId;

        await _electionRepository.CastBallotInAnElection(electionId, ballotEntity);
        await _electionRepository.SaveChangesAsync();
            
        return CreatedAtAction(nameof(GetBallotById), new
        {
            electionId,
            ballotId = ballotEntity.Id,
        }, ballotEntity);
    }
}
