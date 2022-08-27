using PostCodeSearch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostCodeSearch.Services
{
    public interface IPostCodeService
    {
        Task<IEnumerable<PostCodeLookup>> FindByLGA(string search);
        Task<IEnumerable<PostCodeLookup>> FindPostCodes(string search);
        Task<bool> PostCodesInitialised();
        Task<long> RefreshPostCodes();
    }
}