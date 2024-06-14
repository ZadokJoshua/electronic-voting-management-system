using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Entities;

public class Candidate : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public AppUser User { get; set; }

    [ForeignKey(nameof(PositionId))]
    public Guid? PositionId { get; set; }
    public Position? Position { get; set; }

    [ForeignKey(nameof(PartyId))]
    public Guid? PartyId { get; set; }
    public Party? Party { get; set; }
}
