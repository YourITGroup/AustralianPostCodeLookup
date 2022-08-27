using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostCodeSearch.Models;
using PostCodeSearch.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace PostCodeSearch.Controllers
{
    [PluginController("PostCodes")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/postcodes")]
    public class PostCodeLookupController : UmbracoApiController
    {
        private readonly IPostCodeService postCodeService;

        public PostCodeLookupController(IPostCodeService postCodeService)
        {
            this.postCodeService = postCodeService;
        }

        [HttpGet("getPostCodes/{search:string}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostCodeLookup>>> GetPostCodes(string search)
        {
            var postCodes = await postCodeService.FindPostCodes(search);
            return Ok(postCodes);
        }

        [HttpGet("getByLocalGovernmentArea/{search:string}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostCodeLookup>>> GetByLocalGovernmentArea(string search)
        {
            var postCodes = await postCodeService.FindByLGA(search);
            return Ok(postCodes);
        }
    }
}
