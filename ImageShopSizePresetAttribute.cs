using System;

namespace Geta.EPi.ImageShop
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ImageShopSizePresetAttribute : Attribute
    {
        private string _name;

        public ImageShopSizePresetAttribute(int width, int height) : this(null, width, height)
        {
        }

        public ImageShopSizePresetAttribute(string name, int width, int height)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_name))
                {
                    _name = string.Format("{0}x{1}", Width, Height);
                }

                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}