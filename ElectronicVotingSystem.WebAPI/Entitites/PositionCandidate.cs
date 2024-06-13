using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class PositionCandidate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public Guid? PositionId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Position? Position { get; set; }

    [Required]
    public Guid? CandidateId { get; set; }
    public Candidate? Candidate { get; set; }
}