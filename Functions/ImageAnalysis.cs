using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Functions
{
    public static class ImageAnalysis
    {
        [FunctionName("ImageAnalysis")]
        public static async Task Run(
            [BlobTrigger("images/{name}", Connection = "azurecourse2")]CloudBlockBlob blob, 
            ILogger log,
            [CosmosDB("azure-course-nosql", "images", ConnectionStringSetting = "cosmosdb")]
            IAsyncCollector<FaceAnalysisResults> result
            )
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{blob.Name} \n Size: {blob.Properties.Length} Bytes");

            var sas = GetSas(blob);
            var url = blob.Uri + sas;
            log.LogInformation($"Blob uri is {url}");
            var faces = await GetAnalysisAsync(url);
            await result.AddAsync(new FaceAnalysisResults { Faces = faces, ImageId = blob.Name });
        }

        public static string GetSas(CloudBlockBlob blob)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };

            var sas = blob.GetSharedAccessSignature(sasPolicy);
            return sas;
        }

        public static async Task<Face[]> GetAnalysisAsync(string url)
        {
            var client = new FaceServiceClient("8da8caf3c1f14e5d846d3e7c9e0699f9", "https://westeurope.api.cognitive.microsoft.com/face/v1.0");
            var types = new[] { FaceAttributeType.Emotion };
            var result = await client.DetectAsync(url, false, false, types);
            return result;
        }
    }
}