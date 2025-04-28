using Amazon.S3;
using Amazon.S3.Model;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class AmazonS3Service : IAmazonS3Service
{
    private static readonly string accessKey = Environment.GetEnvironmentVariable("AWS_IAM_USER_ACCESS_KEY")!;
    private static readonly string secretKey = Environment.GetEnvironmentVariable("AWS_IAM_USER_SECRET_KEY")!;
    private static readonly string bucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME")!;
    private static readonly string region = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_REGION")!;
    private static readonly string baseUrl = $"https://{bucketName}.s3.{region}.amazonaws.com/";
    
    private readonly IAmazonS3 s3Client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(region));

    public async Task<string> UploadImageToS3(IFormFile image, string fileName)
    {
        using var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        
        var request = new PutObjectRequest
        {
            Key = fileName,
            BucketName = bucketName,
            ContentType = image.ContentType,
            InputStream = memoryStream,
            CannedACL = S3CannedACL.PublicRead
        };

        await s3Client.PutObjectAsync(request);
        return $"{baseUrl}{fileName}";
    }
}