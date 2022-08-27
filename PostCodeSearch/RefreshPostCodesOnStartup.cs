using PostCodeSearch.Services;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace PostCodeSearch
{
    internal class RefreshPostCodesOnStartup : INotificationHandler<UmbracoApplicationStartedNotification>
    {
        private readonly IPostCodeService postCodeService;

        public RefreshPostCodesOnStartup(
            IPostCodeService postCodeService)
        {
            this.postCodeService = postCodeService;
        }

        public void Handle(UmbracoApplicationStartedNotification notification)
        {
            if (!postCodeService.PostCodesInitialised().Result)
            {
                postCodeService.RefreshPostCodes();
            }
        }
    }
}