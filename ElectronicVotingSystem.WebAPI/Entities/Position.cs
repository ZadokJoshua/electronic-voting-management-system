namespace ElectronicVotingSystem.WebAPI.Entities;

public class Position
{
    public Guid Id { get; set; }
    public Guid ElectionId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}
