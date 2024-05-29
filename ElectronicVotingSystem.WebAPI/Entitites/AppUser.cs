using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class AppUser : IdentityUser
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(300)]
    public string? About { get; set; }

    [Url]
    public string? PhotoUrl { get; set; }

    // If the user wants to be added to an election as a candidate
    public bool CanContest { get; set; } = true;

    // If the user wants to be added to an election as a voter
    public bool CanVote { get; set; } = true;

    public string Role { get; set; }

    public ICollection<Ballot>? Ballots { get; set; }
    public ICollection<Candidate>? Candidates { get; set; }
}