using ElectronicVotingSystem.WebAPI.Services;
namespace ElectronicVotingSystem.Tests;


public class AzureStrorageServiceTest
{
    [Theory]
    [InlineData("example.png", "profile-picture")]
    [InlineData("pdp-random123.jpeg", "profile-picture")]
    [InlineData("avater.jpeg", "icon")]
    [InlineData("apc-234542u.jpeg", "icon")]
    public void GenerateFileName_ShouldGenerateCorrectFileName(string fileName, string fileType)
    {
        // Arrange
        var expectedExtension = fileName.Split('.')[^1];

        // Act
        var result = AzureStorageService.GenerateFileName(fileName, fileType);

        // Assert
        Assert.StartsWith(fileType, result);
        Assert.Contains(DateTime.Now.ToString("yyyy-MM-dd"), result);
        Assert.Contains(DateTime.Now.ToUniversalTime().ToString("yyyyMMdd"), result);
        Assert.EndsWith(expectedExtension, result);
    }
}
