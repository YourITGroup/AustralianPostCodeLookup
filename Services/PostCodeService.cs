using Microsoft.Extensions.Logging;
using PostCodeSearch.Models;
using PostCodeSearch.Models.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services.Implement;
using Umbraco.Extensions;

namespace PostCodeSearch.Services
{
    public class PostCodeService : RepositoryService, IPostCodeService
    {
        private readonly IPostCodeRepository postCodeRepository;
        private readonly IGitHubFileService gitHubFileService;
        private readonly ILogger<PostCodeService> logger;

        public PostCodeService(IScopeProvider provider,
                               ILoggerFactory loggerFactory,
                               IEventMessagesFactory eventMessagesFactory,
                               IPostCodeRepository postCodeRepository,
                               IGitHubFileService gitHubFileService,
                               ILogger<PostCodeService> logger)
            : base(provider, loggerFactory, eventMessagesFactory)
        {
            this.postCodeRepository = postCodeRepository;
            this.gitHubFileService = gitHubFileService;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PostCodeLookup>> FindPostCodes(string search)
        {
            if (search?.Length > 2)
            {
                using (ScopeProvider.CreateScope(autoComplete: true))
                {
                    var postcodes = await postCodeRepository.Find(search);
                    return postcodes.Select(p => new PostCodeLookup
                    {
                        Id = p.Id,
                        Postcode = p.PostCode,
                        Locality = p.Locality,
                        Latitude = p.Lat,
                        Longitude = p.Long,
                        Region = p.Region,
                        Electorate = p.Electorate,
                        ElectorateRating = p.ElectorateRating
                    });
                }
            }
            return Enumerable.Empty<PostCodeLookup>();
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<PostCodeLookup>> FindByLGA(string search)
        {
            if (search?.Length > 2)
            {
                using (ScopeProvider.CreateScope(autoComplete: true))
                {
                    var postcodes = await postCodeRepository.FindByLGA(search);
                    return postcodes.Select(p => new PostCodeLookup
                    {
                        Id = p.Id,
                        Postcode = p.PostCode,
                        Locality = p.Locality,
                        Latitude = p.Lat,
                        Longitude = p.Long,
                        Region = p.Region,
                        Electorate = p.Electorate,
                        ElectorateRating = p.ElectorateRating
                    });
                }
            }
            return Enumerable.Empty<PostCodeLookup>();
        }

        public async Task<bool> PostCodesInitialised()
        {
            using (ScopeProvider.CreateScope(autoComplete: true))
            {
                return await postCodeRepository.ContainsData();
            }
        }

        public async Task<long> RefreshPostCodes()
        {
            try
            {
                var postcodes = await gitHubFileService.RetrieveData<IEnumerable<AustralianPostCodeImport>?>();
                if (postcodes == null)
                {
                    logger.LogWarning("Could not retrieve postcode data");
                    return 0;
                }
                logger.LogInformation("Retrieved {count} postcodes", postcodes.LongCount());
                using (var scope = ScopeProvider.CreateScope())
                {
                    postCodeRepository.ClearTable();
                    foreach (var batch in postcodes.InGroupsOf(100))
                    {
                        await postCodeRepository.InsertBatch(batch.Select(p =>
                        {
                            var lat = p.LatPrecise;
                            if (lat is null || lat == 0)
                            {
                                lat = p.Lat;
                            }
                            var lng = p.LongPrecise;
                            if (lng is null || lng == 0)
                            {
                                lng = p.Long;
                            }
                            return new PostCodes
                            {
                                Id = p.Id,
                                PostCode = p.PostCode,
                                Locality = p.Locality,
                                Region = p.LgaRegion,
                                Electorate = p.Electorate,
                                ElectorateRating = p.ElectorateRating,
                                Lat = lat,
                                Long = lng,
                                Country = "Australia"
                            };
                        }
                        ));
                    }
                    scope.Complete();
                }
                return postcodes.LongCount();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not retrieve postcode data");
            }
            return 0;
        }

    }
}
