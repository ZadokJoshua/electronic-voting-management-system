using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class Vote : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(CandidateId))]
    public required Guid CandidateId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Candidate Candidate { get; set; }

    [Required]
    [ForeignKey(nameof(BallotId))]
    public required Guid BallotId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Ballot Ballot { get; set; }
}