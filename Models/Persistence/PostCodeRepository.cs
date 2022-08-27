using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Infrastructure.Persistence.Repositories.Implement;
using Umbraco.Extensions;

namespace PostCodeSearch.Models.Persistence
{
    public class PostCodeRepository : RepositoryBase, IPostCodeRepository
    {
        public PostCodeRepository(IScopeAccessor scopeAccessor, AppCaches appCaches) : base(scopeAccessor, appCaches)
        {
        }

        public async Task<IEnumerable<PostCodes>> Find(string filter)
        {
            var cleanFilter = filter.Trim();
            if (cleanFilter.IsNullOrWhiteSpace())
            {
                return Enumerable.Empty<PostCodes>();
            }

            return await Database.FetchAsync<PostCodes>(Database.SqlContext.Sql().Where("PostCode like @0 + '%' OR Locality LIKE @0 + '%'", filter.Trim()));
        }

        public async Task<IEnumerable<PostCodes>> FindByLGA(string filter)
        {
            var cleanFilter = filter.Trim();
            if (cleanFilter.IsNullOrWhiteSpace())
            {
                return Enumerable.Empty<PostCodes>();
            }

            return await Database.FetchAsync<PostCodes>(Database.SqlContext.Sql().Where("Region like @0 + '%' OR Electorate LIKE @0 + '%'", filter.Trim()));
        }

        public async Task<bool> ContainsData()
        {
            var data = await Database.FirstAsync<int>(Database.SqlContext.Sql().Select("count(*)").From<PostCodes>());
            return data > 0;
        }

        /// <summary>
        /// Creates or updates a postcode
        /// </summary>
        /// <param name="postCode"></param>
        public async void SavePostCode(PostCodes postCode)
        {
            if (Database.IsNew(postCode))
            {
                await Database.InsertAsync(postCode);
            }
            else
            {
                await Database.UpdateAsync(postCode);
            }
        }

        public void ClearTable()
        {
            Database.TruncateTable(Database.SqlContext.SqlSyntax, PostCodes.TableName);
        }

        public async Task InsertBatch(IEnumerable<PostCodes> data)
        {
            await Database.InsertBatchAsync(data);
        }
    }
}
