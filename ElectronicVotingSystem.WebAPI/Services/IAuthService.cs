namespace ElectronicVotingSystem.WebAPI.Services;

public interface IAuthService
{
    string DecodeUserNameFromToken(string token);
    string? GenerateToken(string userName, string userId, string userRole);
}
