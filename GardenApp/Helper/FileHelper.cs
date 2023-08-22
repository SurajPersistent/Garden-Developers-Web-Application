using Azure.Storage.Blobs;

namespace GardenApp.Helper
{
    public static class FileHelper
    {
        public static async Task<string> UploadImage(IFormFile file)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=gardenfilestorage;AccountKey=TmB5ozbTpP4EKH0nIIf3YoEQW3lAyqdv2EzOf/VJt9slZkkr8M9dfJ/0uOdsFIeYenf2dF1usoWf+AStZv5ppw==;EndpointSuffix=core.windows.net";
            string containerName = "plansimages";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString,containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
