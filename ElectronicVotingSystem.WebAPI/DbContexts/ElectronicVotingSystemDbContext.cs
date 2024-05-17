using ElectronicVotingSystem.WebAPI.Entitites;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.DbContexts;

public class ElectronicVotingSystemDbContext(DbContextOptions<ElectronicVotingSystemDbContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Election> Elections {  get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Vote> Votes { get; set; }
}
