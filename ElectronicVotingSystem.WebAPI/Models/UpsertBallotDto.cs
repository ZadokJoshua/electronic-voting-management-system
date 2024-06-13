namespace ElectronicVotingSystem.WebAPI.Models;

public class UpsertBallotDto
{
    public ICollection<UpsertPositionCandidate> UpsertPositionCandidates { get; set; } = [];

    public DateTime SubmissionDate { get; set; } = DateTime.Now;
}
