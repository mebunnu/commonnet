# CommonNet

do not forget to empty DocumentationFile in CommonNet.csproj xml before creating a nuget package so that the documentation will be visible to consuming application 


## blob storage more content 

   https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-dotnet-get-started


## file storage 

https://docs.microsoft.com/en-us/azure/storage/files/storage-dotnet-how-to-use-files?tabs=dotnet

## Project Structure

📦CommonNet
 ┣ 📂Azure
 ┃ ┣ 📂AI
 ┃ ┣ 📂KeyVault
 ┃ ┃ ┗ 📜KeyVault.cs
 ┃ ┗ 📂Storage
 ┃ ┃ ┗ 📜BlobStorage.cs
 ┣ 📂Dapper
 ┃ ┗ 📜Dapper.cs
 ┣ 📂Helpers
 ┃ ┗ 📜Helper.cs
 ┣ 📂Models
 ┃ ┣ 📜Keys.cs
 ┃ ┗ 📜TokenModel.cs
 ┣ 📂NugetHelpers
 ┃ ┣ 📜documentation.xml
 ┃ ┗ 📜icon.png
 ┣ 📜.gitattributes
 ┣ 📜.gitignore
 ┣ 📜CommonNet.csproj
 ┣ 📜CommonNet.sln
 ┣ 📜LICENSE.txt
 ┣ 📜README.md
 ┗ 📜Usings.cs