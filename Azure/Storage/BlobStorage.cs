﻿namespace CommonNet.Azure.Storage;

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
    public async Task CreateContainerAsync(string containerName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        //write public access type
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
    }

    /// <summary>
    /// deletes a container if exists
    /// </summary>
    /// <param name="containerName">name of the container</param>
    /// <returns>true if deleted, false if not deleted</returns>
    public async Task<bool> DeleteContainerAsync(string containerName)
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
    public async Task<List<BlobContainerItem>> GetListofContainersAsync(string prefix = "", int segmentSize = int.MaxValue)
    {

        List<BlobContainerItem> containerItems = new();

        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        var resultSegment =
            blobServiceClient.GetBlobContainersAsync(BlobContainerTraits.Metadata, prefix, default)
            .AsPages(default, segmentSize);

        await foreach (Page<BlobContainerItem> containerPage in resultSegment)
        {
            foreach (BlobContainerItem item in containerPage.Values)
            {
                containerItems.Add(item);
            }
        }

        return containerItems;
    }

    /// <summary>
    /// uploads file to the storage
    /// </summary>
    /// <param name="containerName">container name</param>
    /// <param name="path">path of the file along with folder and extension</param>
    /// <param name="file">IFormFile</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    public async Task UploadAsync(string containerName, string path, IFormFile file, CancellationToken cancellationToken = default)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(path);

        using MemoryStream ms = new MemoryStream();
        file.CopyTo(ms);

        await blobClient.UploadAsync(BinaryData.FromStream(ms), true, cancellationToken);
    }

    /// <summary>
    /// uploads file to the storage
    /// </summary>
    /// <param name="containerName">container name</param>
    /// <param name="path">path of the file along with folder and extension</param>
    /// <param name="data">byte array</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    public async Task UploadAsync(string containerName, string path, byte[] data, CancellationToken cancellationToken = default)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(path);

        await blobClient.UploadAsync(BinaryData.FromBytes(data), true, cancellationToken);
    }

    /// <summary>
    /// uploads file to the storage
    /// </summary>
    /// <param name="containerName">container name</param>
    /// <param name="path">path of the file along with folder and extension</param>
    /// <param name="binaryData">BinaryData</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    public async Task UploadAsync(string containerName, string path, BinaryData binaryData, CancellationToken cancellationToken = default)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(path);

        await blobClient.UploadAsync(binaryData, true, cancellationToken);
    }


    /// <summary>
    /// uploads file to the storage
    /// </summary>
    /// <param name="containerName">container name</param>
    /// <param name="path">path of the file along with folder and extension</param>
    /// <param name="stream">stream</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    public async Task UploadAsync(string containerName, string path, Stream stream, CancellationToken cancellationToken = default)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(path);

        await blobClient.UploadAsync(BinaryData.FromStream(stream), true, cancellationToken);
    }

    /// <summary>
    /// downloads a blob
    /// </summary>
    /// <param name="containerName">container name</param>
    /// <param name="path">path of the file</param>
    /// <returns>stream</returns>
    public async Task<Stream> DownloadBlobAsync(string containerName, string path)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(path);

        return await blobClient.OpenReadAsync();
    }


    /// <summary>
    /// checks if file exists
    /// </summary>
    /// <param name="containerName">container name</param>
    /// <param name="path">file path</param>
    /// <returns>bool</returns>
    public async Task<bool> CheckIfFileExistsAsync(string containerName, string path)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(path);

        return await blobClient.ExistsAsync();
    }


    /// <summary>
    /// deletes a blob if exists
    /// </summary>
    /// <param name="containerName">container name</param>
    /// <param name="path">path of the file</param>
    /// <returns>bool</returns>
    public async Task<bool> DeleteBlobAsync(string containerName, string path)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(Keys.BLOBSTORAGECONNECTIONSTRING);

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(path);

        return await blobClient.DeleteIfExistsAsync();
    }
}
