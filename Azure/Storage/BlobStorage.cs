namespace CommonNet.Azure.Storage;

/// <summary>
/// blob storage
/// </summary>
public class BlobStorage
{
    /// <summary>
    /// sets the connection string
    /// </summary>
    /// <param name="connectionString">connection string of blob storage</param>
    public void SetConnectionString(string connectionString)
    {
        Keys.BLOBSTORAGECONNECTIONSTRING = connectionString;
    }

    /// <summary>
    /// creates a container if doesnot exist
    /// </summary>
    /// <param name="containerName">name of the container</param>
    /// <returns></returns>
    public async Task CreateContainer(string containerName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();
    }

    /// <summary>
    /// deletes a container if exists
    /// </summary>
    /// <param name="containerName">name of the container</param>
    /// <returns>true if deleted, false if not deleted</returns>
    public async Task<bool> DeleteContainer(string containerName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        return await containerClient.DeleteIfExistsAsync();
    }

    /// <summary>
    /// returns list of containers
    /// </summary>
    /// <param name="prefix">used to filter the containers</param>
    /// <param name="segmentSize">used to define the size of the segment</param>
    /// <returns>list of containers</returns>
    public async Task<List<BlobContainerItem>> GetListofContainers(string prefix = "", int segmentSize = int.MaxValue)
    {

        List<BlobContainerItem> containerItems = new();

        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        var resultSegment =
            blobServiceClient.GetBlobContainersAsync(BlobContainerTraits.Metadata, prefix, default)
            .AsPages(default, segmentSize);

        await foreach (Page<BlobContainerItem> containerPage in resultSegment)
        {
            foreach(BlobContainerItem item in containerPage.Values)
            {
                containerItems.Add(item);
            }
        }

        return containerItems;
    }
}
