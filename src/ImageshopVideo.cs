using System.Collections.Generic;

namespace Geta.EPi.Imageshop
{
    public class ImageshopVideo : ImageshopFile
    {
        public virtual IEnumerable<ImageshopVideoData> Videos { get; set; }
    }
}