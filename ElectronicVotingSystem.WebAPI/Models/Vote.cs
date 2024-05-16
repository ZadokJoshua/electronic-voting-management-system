using ElectronicVotingSystem.WebAPI.Models;

namespace ElectronicVotingSystem.WebAPI.Entities;

public class Vote
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required User User { get; set; }
    public required Guid ElectionId { get; set; }
    public required Election Election { get; set; }
    public bool HasVoted { get; set; }
    public DateTime VoteDate { get; set; }
}
