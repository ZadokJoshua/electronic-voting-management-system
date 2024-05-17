﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Models;

public class Election
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    [StringLength(300)]
    public required string Description { get; set; }

    [StringLength(100)]
    public string? Instructions { get; set; }

    [Url]
    public string? ImageUrl { get; set; }
    public string? ElectionAccessKey { get; set; }
    public bool IsVotingOn { get; set; }
    public bool HasVotingEnded { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? TimeZone { get; set; }

    public ICollection<Party>? Parties { get; set; }
    public ICollection<Position>? Positions { get; set; }
}
