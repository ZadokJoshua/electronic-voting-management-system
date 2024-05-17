namespace ElectronicVotingSystem.WebAPI.Models;

public class BlobResponseDto
{
    public string? Name { get; set; }
    public string? Status { get; set; }
    public bool Error { get; set; }
    public string? Uri { get; set; }
}
