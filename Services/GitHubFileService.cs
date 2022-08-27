using Microsoft.Extensions.Options;
using Octokit;
using PostCodeSearch.Configuration;
using PostCodeSearch.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostCodeSearch.Services
{
    public class GitHubFileService : IGitHubFileService
    {
        private readonly IOptions<PostCodes> options;

        public GitHubFileService(IOptions<Configuration.PostCodes> options)
        {
            this.options = options;
        }

        /// <summary>
        /// Retrieves the blob from the repository as a base64 encoded string.
        /// </summary>
        /// <returns>Stream (make sure it is disposed properly)</returns>
        public async Task<Stream> RetrieveData(string path)
        {
            var client = new GitHubClient(new ProductHeaderValue("postcode-lookup"));
            var blob = await client.Repository.Content.GetRawContent(options.Value.RepositoryOwner, options.Value.Repository, path);
            return new MemoryStream(blob);
        }

        /// <summary>
        /// Retrieves the blob from the repository as a base64 encoded string.
        /// </summary>
        /// <returns>Base64 Encoded file content</returns>
        public async Task<string?> RetrieveBlobContents(string path)
        {
            var client = new GitHubClient(new ProductHeaderValue("postcode-lookup"));
            var repo = await client.Repository.Get(options.Value.RepositoryOwner, options.Value.Repository);
            var branchRef = await client.Git.Reference.Get(repo.Id, $"heads/{options.Value.Branch}");
            var tree = await client.Git.Tree.Get(repo.Id, branchRef.Object.Sha);
            var blobRef = tree.Tree.FirstOrDefault(t => t.Path == path);
            if (blobRef != null)
            {
                var blob = await client.Git.Blob.Get(repo.Id, blobRef.Sha);
                return blob.Content;
            }
            return default;
        }

        /// <summary>
        /// Retrieves the file from the repository.
        /// </summary>
        /// <returns></returns>
        public async Task<T?> RetrieveData<T>()
        {
            T? data = default;
            var base64 = await RetrieveBlobContents(options.Value.FileName);
            var serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(new NullableIntJsonConverter());
            serializerOptions.Converters.Add(new NullableDecimalJsonConverter());
            serializerOptions.Converters.Add(new NullableDoubleJsonConverter());
            if (!string.IsNullOrEmpty(base64))
            {
                data = JsonSerializer.Deserialize<T>(Convert.FromBase64String(base64), serializerOptions);
            }
            return data;
        }
    }
}
