using ElectronicVotingSystem.WebAPI.Models;

namespace ElectronicVotingSystem.WebAPI.Services;

public interface IAzureStorageService
{
    Task<BlobResponseDto> UploadAsync(IFormFile blob, bool isIcon);
    Task<BlobResponseDto> DeleteAsync(string blobFilename, bool isIcon);
}
