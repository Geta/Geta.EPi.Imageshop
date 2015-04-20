using System.Configuration;

namespace Geta.EPi.Imageshop.Configuration
{
    public class ImageshopSizePresetElement : ConfigurationElementBase
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("width", IsRequired = true)]
        public int Width
        {
            get
            {
                return (int)this["width"];
            }
            set
            {
                this["width"] = value;
            }
        }

        [ConfigurationProperty("height", IsRequired = true)]
        public int Height
        {
            get
            {
                return (int)this["height"];
            }
            set
            {
                this["height"] = value;
            }
        }

        public override string GetElementKey()
        {
            return Name;
        }
    }
}