namespace ElectronicVotingSystem.WebAPI.Entities;

public class Votes
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ElectionId { get; set; }
    public bool HasVoted { get; set; }
    public DateTime VoteDate { get; set; }
}
