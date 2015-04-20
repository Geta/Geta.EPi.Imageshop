namespace Geta.EPi.ImageShop
{
    public class ImageShopImage
    {
        public virtual string Url { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
    }
}