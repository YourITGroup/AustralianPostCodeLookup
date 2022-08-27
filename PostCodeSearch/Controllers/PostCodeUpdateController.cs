using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostCodeSearch.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace PostCodeSearch.Controllers
{
    [PluginController("PostCodes")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/postcodes")]
    public class PostCodeUpdateController : UmbracoApiController
    {
        private readonly ILogger<PostCodeUpdateController> logger;
        private readonly IPostCodeService postCodeService;

        public PostCodeUpdateController(ILogger<PostCodeUpdateController> logger, IPostCodeService postCodeService)
        {
            this.logger = logger;
            this.postCodeService = postCodeService;
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Refresh()
        {
            logger.LogInformation("Received PostCode Refresh event");
            var stopwatch = Stopwatch.StartNew();
            var count = await postCodeService.RefreshPostCodes();
            stopwatch.Stop();
            logger.LogInformation("Refreshed {count} PostCodes in {duration}", count, stopwatch.Elapsed);
            return Ok();
        }
    }
}
