using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class Result : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(ElectionId))]
    public required Guid ElectionId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Election Election { get; set; }

    [Required]
    [ForeignKey(nameof(PositionId))]
    public required Guid PositionId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Position Position { get; set; }

    [Required]
    [ForeignKey(nameof(CandidateId))]
    public required Guid CandidateId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Candidate Candidate { get; set; }

    public int VoteCount { get; set; }
}
