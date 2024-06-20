namespace ElectronicVotingSystem.WebAPI.Services;

public interface IAuthService
{
    string? GenerateToken(string userName, string userId, string userRole);
}
