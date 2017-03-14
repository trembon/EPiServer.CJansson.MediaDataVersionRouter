using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;
using System;
using System.Linq;
using System.Web.Routing;

namespace EPiServer.CJansson.MediaDataVersionRouter.Routing
{
    public class MediaDataVersionPartialRouter : IPartialRouter<MediaData, ContentVersion>
    {
        public Injected<IContentVersionRepository> ContentVersionRepository { get; set; }

        public PartialRouteData GetPartialVirtualPath(ContentVersion content, string language, RouteValueDictionary routeValues, RequestContext requestContext)
        {
            return new PartialRouteData()
            {
                BasePathRoot = content.ContentLink.ToReferenceWithoutVersion(),
                PartialVirtualPath = content.ContentLink.WorkID.ToString()
            };
        }

        public object RoutePartial(MediaData content, SegmentContext segmentContext)
        {
            var segment = segmentContext.GetNextValue(segmentContext.RemainingPath);
            
            int version;
            if (!int.TryParse(segment.Next, out version) || version <= 0)
                return null;

            segmentContext.RemainingPath = segment.Remaining;

            var list = ContentVersionRepository.Service.List(content.ContentLink);
            return list.FirstOrDefault(cv => cv.ContentLink.WorkID == version);
        }
    }
}