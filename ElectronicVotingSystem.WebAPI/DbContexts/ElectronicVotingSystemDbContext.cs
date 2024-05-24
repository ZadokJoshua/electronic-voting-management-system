using ElectronicVotingSystem.WebAPI.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.DbContexts;

public class ElectronicVotingSystemDbContext : IdentityDbContext<User>
{
    public DbSet<Ballot> Ballots { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Election> Elections {  get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Vote> Votes { get; set; }

    public ElectronicVotingSystemDbContext(DbContextOptions<ElectronicVotingSystemDbContext> options) : base(options)
    {
    }
}