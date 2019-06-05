using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureCourse.Services
{
    public class ImageStore
    {
        private readonly CloudBlobClient _client;
        private readonly string _baseUrl = "https://azurecourse2.blob.core.windows.net";

        public ImageStore()
        {
            var credentials = new StorageCredentials("azurecourse2", "xDfj8kPisVQ3cGkxCwkfZ6Oy51Dbi0al/sd2b2Cy9nBOS803vNs8F51VLgzslsdKdiO1eC6WRqDD9fB7C8vQHQ==");
            _client = new CloudBlobClient(new Uri(_baseUrl), credentials);
        }

        internal string UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };

            var container = _client.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);
            var sas = blob.GetSharedAccessSignature(sasPolicy);
            return $"{_baseUrl}/images/{imageId}{sas}";
        }

        public async Task<string> SaveAsync(Stream stream)
        {
            var imageId = Guid.NewGuid().ToString();
            var container = _client.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);
            await blob.UploadFromStreamAsync(stream);

            return imageId;
        }
    }
}