using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Extensions;
using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

/// <summary>
/// Controller for managing Elections.
/// </summary>
/// <param name="electionRepository"></param>
[Authorize]
[Route("api/elections")]
[ApiController]
[Produces("application/json")]
public class ElectionsController(IElectionRepository electionRepository) : BaseController
{
    private readonly IElectionRepository _electionRepository = electionRepository;

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
    /// <param name="electionId">The ID of the election to be retrieved.</param>
    /// <returns>An Election</returns>
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

    /// <summary>
    /// Create a new Election
    /// </summary>
    /// <param name="election">The election DTO containing the details of the election to be created.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateElection(UpsertElectionDto election)
    {
        string? userId = GetUserId();
        if (userId == null)
        {
            return NotFound("User not authenticated or found");
        }

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var electionEntity = election.AsEntity(userId);

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
    /// <param name="electionId">The ID of the election to be updated.</param>
    /// <param name="electionDto">The election DTO containing the updated details of the election.</param>
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
    /// <param name="electionId">The ID of the election to be deleted.</param>
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
