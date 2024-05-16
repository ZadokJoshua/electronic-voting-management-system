using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Models;

public class Candidate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required Guid PositionId { get; set; }
    public required Position Position { get; set; }
    public required Guid PartyId { get; set; }
    public required Party Party { get; set; }
    public required string Name { get; set; }
    public string? About { get; set; }
    public string? PhotoUrl { get; set; }
}
