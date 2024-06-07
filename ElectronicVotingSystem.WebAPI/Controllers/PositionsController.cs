using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

/// <summary>
/// Controller for managing positions in an Election
/// </summary>
/// <param name="positionRepository"></param>
/// <param name="electionRepository"></param>
/// <param name="mapper"></param>
[Authorize]
[Route("api/elections/{electionId}/positions")]
[ApiController]
[Produces("application/json")]
public class PositionsController(IElectionRepository electionRepository, IPositionRepository positionRepository, IMapper mapper) : ControllerBase
{
    private readonly IPositionRepository _positionRepository = positionRepository;
    private readonly IElectionRepository _electionRepository = electionRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Get All positions in an Election
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Position>>> GetAllPositions(Guid electionId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");

        var positions = await _positionRepository.GetAllPositionsInAnElectionAsync(electionId);
        return Ok(positions);
    }

    /// <summary>
    /// Get details of a specific position in an election
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="positionId">Position ID</param>
    /// <returns></returns>
    [HttpGet("{positionId}", Name = nameof(GetPositionById))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Position>> GetPositionById(Guid electionId, Guid positionId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) 
            return NotFound($"Election with ID {electionId} not found!");

        var postition = await _positionRepository.GetAPositionInAnElectionAsync(electionId, positionId);
        if (postition == null) return NotFound($"Position with ID {positionId} not found!");
        return Ok(postition);
    }

    /// <summary>
    /// Create a new Position in an Election
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="positionDto">Position DTO</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreatePosition(Guid electionId, UpsertPositionDto positionDto)
    {
        if (!await _electionRepository.ExistsAsync(electionId))
            return NotFound($"Election with ID {electionId} not found!");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var positionEntity = _mapper.Map<Position>(positionDto);

        await _electionRepository.AddPositionToAnElection(electionId, positionEntity);
        await _electionRepository.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetPositionById), new
        {
            electionId,
            positionId = positionEntity.Id
        },
        positionEntity);
    }

    /// <summary>
    /// Update a Position in an Election
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="positionDto">Position DTO</param>
    /// <returns></returns>
    [HttpPut("{positionId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdatePosition(Guid electionId, Guid positionId, UpsertPositionDto positionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var positionEntity = await _positionRepository.GetAPositionInAnElectionAsync(electionId, positionId);

        if (positionEntity == null) return NotFound($"Position with ID {positionId} not found!");

        _mapper.Map(positionDto, positionEntity);
        await _electionRepository.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a specific Position in an Election
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <param name="positionId">Position ID</param>
    /// <returns></returns>
    [HttpDelete("{positionId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePosition(Guid electionId, Guid positionId)
    {
        if (!await _electionRepository.ExistsAsync(electionId))
        {
            return NotFound($"Election with ID {electionId} not found!");
        }

        var position = await _positionRepository.GetAPositionInAnElectionAsync(electionId, positionId);

        if (position == null)
        {
            return BadRequest($"Position with ID {positionId} not found in election {electionId}.");
        }

        _positionRepository.DeletePosition(position);
        await _positionRepository.SaveChangesAsync();

        return NoContent();
    }
}