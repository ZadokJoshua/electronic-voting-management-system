using AutoMapper;

namespace ElectronicVotingSystem.WebAPI.Profiles;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        // For POST request
        CreateMap<Models.UpsertPositionDto, Entities.Position>();
    }
}
