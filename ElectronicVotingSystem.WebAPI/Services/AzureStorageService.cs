
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ElectronicVotingSystem.WebAPI.Models;
using System.Text;

namespace ElectronicVotingSystem.WebAPI.Services;

public class AzureStorageService(IConfiguration configuration) : IAzureStorageService
{
    private readonly string _azureStorageConnectionString = configuration["AzureStorage:ConnectionString"]!;

    private const string IconsContainer = "icons-container";
    private const string ProfilePicturesContainer = "profile-pictures-container";

    public static string GenerateFileName(string fileName, string prefix)
    {
        try
        {
            var strName = fileName.Split('.');
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(prefix);
            stringBuilder.Append("_");
            stringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd"));
            stringBuilder.Append("_");
            stringBuilder.Append(DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff"));
            stringBuilder.Append(".");
            stringBuilder.Append(strName[^1]); // Using the ^1 syntax to get the last element

            return stringBuilder.ToString();
        }
        catch (Exception)
        {
            return fileName;
        }
    }

    public async Task<BlobResponseDto> UploadAsync(IFormFile blob, bool isIcon)
    {
        string containerName = isIcon ? IconsContainer : ProfilePicturesContainer;
        BlobContainerClient blobContainerClient = new(_azureStorageConnectionString, containerName);

        BlobResponseDto response = new();

        try
        {
            BlobClient client = blobContainerClient.GetBlobClient(blob.FileName);
            await using Stream? data = blob.OpenReadStream();
            var blobContentInfoResponse
                = await client.UploadAsync(data);

            if (blobContentInfoResponse != null)
            {
                response.Error = false;
                response.Uri = client.Uri.AbsoluteUri;
                response.Name = client.Name;
                response.Status = "File uploaded Successfully.";
            }
        }
        catch (RequestFailedException ex) 
        when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
        {
            response.Status = $"File already exists.";
            response.Error = true;
            return response;
        }
        catch (RequestFailedException ex)
        {
            response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
            response.Error = true;
            return response;
        }


        return response;
    }

    public async Task<BlobResponseDto> DeleteAsync(string blobFilename, bool isIcon)
    {
        string containerName = isIcon ? IconsContainer : ProfilePicturesContainer;
        BlobContainerClient blobContainerClient = new(_azureStorageConnectionString, containerName);

        BlobClient file = blobContainerClient.GetBlobClient(blobFilename);

        try
        {
            await file.DeleteAsync();
        }
        catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            return new BlobResponseDto { 
                Error = true, 
                Status = $"File with name {blobFilename} not found." };
        }

        return new BlobResponseDto { 
            Error = false, 
            Status = $"File: {blobFilename} has been successfully deleted." };
    }
}
