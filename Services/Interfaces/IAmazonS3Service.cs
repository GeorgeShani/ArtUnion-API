namespace ArtUnion_API.Services.Interfaces;

public interface IAmazonS3Service
{
    Task<string> UploadImageToS3(IFormFile image, string fileName);
}