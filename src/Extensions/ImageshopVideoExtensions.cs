using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Framework.Serialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace Screentek.EPi.Imageshop.Extensions
{
    public static class ImageshopVideoExtensions
    {
        public static string GetVideosJson(this ImageshopVideo video)
        {
            if (video.Videos != null)
            {
                var objectSerializer = ServiceLocator.Current.GetInstance<IObjectSerializer>();

                return objectSerializer.Serialize(video.Videos.Select(x => new
                {
                    file = x.File,
                    label = x.Label,
                    type = x.Type,
                    fullFile = $"{x.File}/{video.Code}.{x.Type}"
                }));
            }

            return null;
        }

        public static ImageshopVideoData GetFirstVideo(this ImageshopVideo video)
        {
            return video.Videos?.FirstOrDefault();
        }

        public static ImageshopVideoData GetFirstVideo(this ImageshopVideo video, string type)
        {
            return video.Videos?.FirstOrDefault(x => x.Type.Equals(type, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<ImageshopVideoData> GetVideosOfType(this ImageshopVideo video, string type)
        {
            if (video.Videos != null)
            {
                return video.Videos.Where(x => x.Type.Equals(type, StringComparison.InvariantCultureIgnoreCase));
            }

            return Enumerable.Empty<ImageshopVideoData>();
        }

        public static string GetUrlFriendlyCode(this ImageshopVideo video)
        {
            return ServiceLocator.Current.GetInstance<IUrlSegmentGenerator>().Create(video.Code);
        }
    }
}