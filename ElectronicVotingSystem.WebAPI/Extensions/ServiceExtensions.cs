using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Repositories;

namespace ElectronicVotingSystem.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IElectionRepository, ElectionRepository>();
        services.AddScoped<IPartyRepository, PartyRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IVoteRepository, VoteRepository>();
    }
}
