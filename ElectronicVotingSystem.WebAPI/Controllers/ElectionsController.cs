using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

/// <summary>
/// Controller for managing Elections
/// </summary>
/// <param name="electionRepository"></param>
/// <param name="mapper"></param>
[Authorize]
[Route("api/elections")]
[ApiController]
[Produces("application/json")]
public class ElectionsController(IElectionRepository electionRepository, IMapper mapper) : BaseController
{
    private readonly IElectionRepository _electionRepository = electionRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Get all elections
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Election>>> GetAllElections()
    {
        string? userId = GetUserId();
        if (userId == null)
        {
            return BadRequest("User not authenticated or found");
        }
        var elections = await _electionRepository.GetAllElectionsAsync(userId);
        return Ok(elections);
    }

    /// <summary>
    /// Get an election by ID
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <returns></returns>
    [HttpGet("{electionId}", Name = nameof(GetElectionById))]
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

    /// <summary>
    /// Create a new Election
    /// </summary>
    /// <param name="electionDto">Election DTO</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateElection(UpsertElectionDto electionDto)
    {
        string? userId = GetUserId();
        if (userId == null)
        {
            return NotFound("User not authenticated or found");
        }

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var electionEntity = _mapper.Map<Election>(electionDto);
        electionEntity.UserId = userId;

        _electionRepository.Add(electionEntity);
        await _electionRepository.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetElectionById), new 
        {
            electionId = electionEntity.Id 
        }, 
        electionEntity);
    }

    /// <summary>
    /// Update an existing Election
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="electionDto">Election DTO</param>
    /// <returns></returns>
    [HttpPut("{electionId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateElection(Guid electionId, UpsertElectionDto electionDto)
    {
        var election = await _electionRepository.GetElectionByIdAsync(electionId);

        if (election == null) return NotFound($"Election with ID {electionId} not found!");

        election.Name = electionDto.Name;
        election.Description = electionDto.Description;
        election.ElectionAccessKey = electionDto.ElectionAccessKey;

        if(!ModelState.IsValid) return BadRequest(ModelState);

        await _electionRepository.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a specific election
    /// </summary>
    /// <param name="electionId">The ID of the election to be deleted</param>
    /// <returns></returns>
    [HttpDelete("{electionId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteElection(Guid electionId)
    {
        var election = await _electionRepository.GetElectionByIdAsync(electionId);

        if (election == null) return NotFound($"Election with ID {electionId} not found!");

        _electionRepository.DeleteElection(election);
        await _electionRepository.SaveChangesAsync();

        return NoContent();
    }
}