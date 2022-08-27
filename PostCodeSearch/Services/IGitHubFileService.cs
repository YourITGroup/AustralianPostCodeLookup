using System.IO;
using System.Threading.Tasks;

namespace PostCodeSearch.Services
{
    public interface IGitHubFileService
    {
        Task<string?> RetrieveBlobContents(string path);
        Task<Stream> RetrieveData(string path);
        Task<T?> RetrieveData<T>();
    }
}