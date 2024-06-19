using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers;

[Authorize]
[Route("api/election/{electionId}/results")]
[ApiController]
[Produces("application/json")]
public class ResultsController(IElectionRepository electionRepository, IPositionRepository positionRepository, IBallotRepository ballotRepository) : ControllerBase
{
    private readonly IElectionRepository _electionRepository = electionRepository;
    private readonly IPositionRepository _positionRepository = positionRepository;
    private readonly IBallotRepository _ballotRepository = ballotRepository;

    /// <summary>
    /// Get Election Results
    /// </summary>
    /// <param name="electionId">Election ID</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ElectionResultsDto>> GetElectionResultsById(Guid electionId)
    {
        if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");

        var ballots = await _ballotRepository.GetResultsByElectionAsync(electionId);
        if (ballots == null || !ballots.Any()) return NotFound("No vote yet");

        var castedBallots = ballots.SelectMany(ballot => ballot.PositionCandidates).ToList();

        var electionPositions = await _positionRepository.GetAllPositionsInElectionAsync(electionId);
        if (electionPositions == null || !electionPositions.Any()) return NotFound("No positions found for the election");

        List<Candidate> contestants = [];
        foreach (var position in electionPositions)
        {
            contestants.AddRange(position.Candidates);
        }
        
        if (contestants.Count == 0) return NotFound("No candidates found for the election");

        ElectionResultsDto resultsDto = new() 
        { 
            ElectionId = electionId
        };

        foreach (var position in electionPositions)
        {
            var positionDto = new PositionResultsDto
            {
                PositionId = position.Id,
                CandidateResults = contestants
                .Where(candidate => candidate.PositionId == position.Id)
                .Select(candidate => new CandidateResultsDto
                {
                    CandidateId = candidate.Id,
                    TotalVotes = 0
                })
                .ToList()
            };

            resultsDto.PositionsResults.Add(positionDto);
        }

        foreach (var position in resultsDto.PositionsResults)
        {
            var votesForPosition = castedBallots.Where(pc => pc.PositionId == position.PositionId).ToList();
            foreach (var candidate in position.CandidateResults)
            {
                candidate.TotalVotes = votesForPosition.Count(pc => pc.CandidateId == candidate.CandidateId);
            }
        }

        return Ok(resultsDto);
    }
}

public record ElectionResultsDto
{
    public Guid ElectionId { get; set; }
    public List<PositionResultsDto> PositionsResults { get; set; } = [];
}

public record PositionResultsDto
{
    public Guid PositionId { get; set; }
    public ICollection<CandidateResultsDto> CandidateResults { get; set; } = [];
}

public record CandidateResultsDto
{
    public Guid CandidateId { get; set; }
    public int TotalVotes { get; set; }
}
