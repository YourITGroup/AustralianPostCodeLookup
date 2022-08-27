using Microsoft.Extensions.DependencyInjection;
using PostCodeSearch.Models.Persistence;
using PostCodeSearch.Services;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace PostCodeSearch
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddPostCodes(this IUmbracoBuilder builder)
        {
            builder.Services
                .AddOptions()
                .Configure<Configuration.PostCodes>(builder.Config.GetSection(nameof(Configuration.PostCodes)));

            builder.Services.AddSingleton<IPostCodeRepository, PostCodeRepository>();
            builder.Services.AddSingleton<IGitHubFileService, GitHubFileService>();
            builder.Services.AddSingleton<IPostCodeService, PostCodeService>();

            builder.AddNotificationHandler<UmbracoApplicationStartingNotification, RunPostCodesMigration>()
                    .AddNotificationHandler<UmbracoApplicationStartedNotification, RefreshPostCodesOnStartup>()
                    .AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesParsingHandler>();
            return builder;
        }
    }
}
