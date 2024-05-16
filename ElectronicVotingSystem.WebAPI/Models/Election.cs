using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Models;

public class Election
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    //public Guid UserId { get; set; }
    //public required User User { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? Instructions { get; set; }
    public string? ImageUrl { get; set; }
    public string? ElectionAccessKey { get; set; }
    public bool IsVotingOn { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? TimeZone { get; set; }
    public bool HasVotingEnded { get; set; }

    public ICollection<Party> Parties { get; set; }
    public ICollection<Position> Positions { get; set; }
}
