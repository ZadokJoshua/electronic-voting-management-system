using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

[Authorize]
[Route("api/elections/{electionId}/positions")]
[ApiController]
public class PosititonsController(IPositionRepository positionRepository, IElectionRepository electionRepository) : ControllerBase
{
    private readonly IPositionRepository _positionRepository = positionRepository;
    private readonly IElectionRepository _electionRepository = electionRepository;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Position>>> GetAllPositions(Guid electionId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");

        var positions = await _positionRepository.GetAllPositionsInAnElectionAsync(electionId);
        return Ok(positions);
    }

    [HttpGet("{positionId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Position>> GetPosition(Guid electionId, Guid positionId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");

        var postition = await _positionRepository.GetAPositionInAnElectionAsync(electionId, positionId);

        if (postition == null) return NotFound($"Position with ID {electionId} not found!");
        return Ok(postition);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreatePosition(Guid electionId, Position position)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await _electionRepository.ExistsAsync(electionId))
        {
            return NotFound($"Election with ID {electionId} not found!");
        }

        _positionRepository.Add(position);
        await _positionRepository.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]
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

        _positionRepository.DeletePostion(position);
        await _positionRepository.SaveChangesAsync();

        return NoContent();
    }
}
