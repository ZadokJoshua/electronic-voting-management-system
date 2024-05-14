namespace ElectronicVotingSystem.WebAPI.Entities;

public class Party
{
    public Guid Id { get; set; }
    public Guid ElectionId { get; set; }
    public required string Name { get; set; }
    public string? Motto { get; set; }
    public string? About { get; set; }
    public string? IconUrl { get; set; }
}
