using System;

namespace Geta.EPi.Imageshop
{
    public class ImageshopImage : ImageshopFile
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual string CropName { get; set; }

        public virtual string GetCroppedUrl(string cropName)
        {
            if (!string.IsNullOrWhiteSpace(cropName))
            {
                return string.Format("{0}-{1}", Url, cropName);
            }

            return Url;
        }
    }
}
