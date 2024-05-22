using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class Candidate : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(PositionId))]
    public Guid? PositionId { get; set; }
    public Position? Position { get; set; }

    [ForeignKey(nameof(PartyId))]
    public Guid? PartyId { get; set; }
    public Party? Party { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(300)]
    public string? About { get; set; }
    [Url]
    public string? PhotoUrl { get; set; }
}
