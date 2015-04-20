using System.Configuration;

namespace Geta.EPi.ImageShop.Configuration
{
    public class ImageShopSettings : ConfigurationElement
    {
        private static ImageShopSettings _instance;
        private static readonly object Lock = new object();

        public static ImageShopSettings Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = ImageShopConfigurationSection.Instance.Settings);
                }
            }
        }

        [ConfigurationProperty("token", IsRequired = true)]
        public string Token
        {
            get
            {
                return this["token"] as string;
            }
            set
            {
                this["token"] = value;
            }
        }

        [ConfigurationProperty("baseUrl", DefaultValue = "http://client.imageshop.no/InsertImage.aspx?IFRAMEINSERT=true", IsRequired = false)]
        public string BaseUrl
        {
            get
            {
                return this["baseUrl"] as string ?? "http://client.imageshop.no/InsertImage.aspx?IFRAMEINSERT=true";
            }
            set
            {
                this["baseUrl"] = value;
            }
        }

        [ConfigurationProperty("interfaceName", DefaultValue = null, IsRequired = false)]
        public string InterfaceName
        {
            get
            {
                return this["interfaceName"] as string;
            }
            set
            {
                this["interfaceName"] = value;
            }
        }

        [ConfigurationProperty("documentPrefix", DefaultValue = null, IsRequired = false)]
        public string DocumentPrefix
        {
            get
            {
                return this["documentPrefix"] as string;
            }
            set
            {
                this["documentPrefix"] = value;
            }
        }

        [ConfigurationProperty("culture", DefaultValue = null, IsRequired = false)]
        public string Culture
        {
            get
            {
                return this["culture"] as string;
            }
            set
            {
                this["culture"] = value;
            }
        }

        [ConfigurationProperty("profileId", DefaultValue = null, IsRequired = false)]
        public string ProfileID
        {
            get
            {
                return this["profileId"] as string;
            }
            set
            {
                this["profileId"] = value;
            }
        }

        [ConfigurationProperty("showSizeDialog", DefaultValue = true, IsRequired = false)]
        public bool ShowSizeDialog
        {
            get
            {
                return (bool)this["showSizeDialog"];
            }
            set
            {
                this["showSizeDialog"] = value;
            }
        }

        [ConfigurationProperty("showCropDialog", DefaultValue = true, IsRequired = false)]
        public bool ShowCropDialog
        {
            get
            {
                return (bool)this["showCropDialog"];
            }
            set
            {
                this["showCropDialog"] = value;
            }
        }

        public static class UIHint
        {
            public const string ImageShopImage = "ImageShopImage";
        }
    }
}