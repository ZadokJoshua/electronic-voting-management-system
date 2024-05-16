namespace ElectronicVotingSystem.WebAPI.Models;

public class Position
{
    public Guid Id { get; set; }
    public required Guid ElectionId { get; set; }
    public required Election Election { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    public ICollection<Candidate> Candidates { get; set; }
}
