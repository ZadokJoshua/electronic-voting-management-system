﻿namespace ElectronicVotingSystem.WebAPI.Models;

public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? CompanyName { get; set; }

}