using System.Collections.Generic;

namespace Screentek.EPi.Imageshop
{
    public class ImageshopVideo : ImageshopFile
    {
        public virtual IEnumerable<ImageshopVideoData> Videos { get; set; }
    }
}