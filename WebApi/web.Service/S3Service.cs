using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Services;

namespace web.Service
{
    public class S3Service:IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            var awsOptions = configuration.GetSection("AWS");
            var accessKey = awsOptions["AccessKey"];
            var secretKey = awsOptions["SecretKey"];
            var region = awsOptions["Region"];
            _bucketName = awsOptions["BucketName"];

            _s3Client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(region));
        }
        public async Task<string> GeneratePresignedUrlAsync(string fileName, string contentType)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(10),
                ContentType = contentType
            };
            var url = _s3Client.GetPreSignedURL(request);

            return url;
        }
        public async Task<string> GetDownloadUrlAsync(string fileName)
        {
            var request = new GetPreSignedUrlRequest
            {
                //BucketName = _awsOptions.BucketName,
                Key = fileName,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(30) // תוקף של 30 דקות
            };

            return _s3Client.GetPreSignedURL(request);
        }
    }
}


//private readonly IAmazonS3 _s3Client;
//private readonly AWSOptions _awsOptions;

//public S3Service(IAmazonS3 s3Client, IOptions<AWSOptions> awsOptions)
//{
//    // הוספתי הדפסות כדי לבדוק שהערכים נטענים כראוי
//    Console.WriteLine("***********************************************************");
//    Console.WriteLine($"Region: {_awsOptions.Region}");  // הדפסת Region
//    Console.WriteLine($"BucketName: {_awsOptions.BucketName}");  // הדפסת BucketName
//    Console.WriteLine("***********************************************************");
//    _s3Client = s3Client;
//    _awsOptions = awsOptions.Value;
//}

//public async Task<string> GeneratePresignedUrlAsync(string fileName, string contentType)
//{
//    Console.WriteLine("***********************************************************");
//    Console.WriteLine(fileName);
//    Console.WriteLine(contentType);
//    Console.WriteLine("***********************************************************");

//    var request = new GetPreSignedUrlRequest
//    {
//        BucketName = _awsOptions.BucketName,
//        Key = fileName,
//        Verb = HttpVerb.PUT,
//        Expires = DateTime.UtcNow.AddMinutes(5),
//        ContentType = contentType
//    };

//    var url = _s3Client.GetPreSignedURL(request);

//    return url;
//}
