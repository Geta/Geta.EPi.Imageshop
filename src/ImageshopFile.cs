namespace Geta.EPi.Imageshop
{
    public class ImageshopFile
    {
        public virtual int DocumentID { get; set; }
        public virtual string Code { get; set; }
        public virtual string Url { get; set; }
        public virtual string Name { get; set; }
        public virtual string Credits { get; set; }
        public virtual string Description { get; set; }
        public virtual string Comment { get; set; }
        public virtual string Rights { get; set; }
        public virtual string Tags { get; set; }
        public virtual string AuthorName { get; set; }
    }
}
