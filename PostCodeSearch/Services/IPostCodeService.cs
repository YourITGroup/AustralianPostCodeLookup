using PostCodeSearch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostCodeSearch.Services
{
    public interface IPostCodeService
    {
        Task<IEnumerable<PostCodeLookup>> FindByRegion(string search);
        Task<IEnumerable<PostCodeLookup>> FindPostCodes(string search);
        Task<IEnumerable<RegionLookup>> FindRegions(string search);
        Task<bool> PostCodesInitialised();
        Task<long> RefreshPostCodes();
    }
}