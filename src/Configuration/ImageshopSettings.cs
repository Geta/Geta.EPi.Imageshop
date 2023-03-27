using System.Configuration;

namespace Screentek.EPi.Imageshop.Configuration
{
    public class ImageshopSettings : ConfigurationElement
    {
        private static ImageshopSettings _instance;
        private static readonly object Lock = new object();

        public static ImageshopSettings Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = ImageshopConfigurationSection.Instance.Settings);
                }
            }
        }

        [ConfigurationProperty("baseUrl", DefaultValue = "https://client.imageshop.no/InsertImage2.aspx", IsRequired = false)]
        public string BaseUrl
        {
            get
            {
                return this["baseUrl"] as string ?? "https://client.imageshop.no/InsertImage2.aspx?IFRAMEINSERT=true";
            }
            set
            {
                this["baseUrl"] = value;
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

        [ConfigurationProperty("freeCrop", DefaultValue = true, IsRequired = false)]
        public bool FreeCrop
        {
            get
            {
                return (bool)this["freeCrop"];
            }
            set
            {
                this["freeCrop"] = value;
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

        [ConfigurationProperty("webServiceUrl", DefaultValue = "https://webservices.imageshop.no/v4.asmx", IsRequired = false)]
        public string WebServiceUrl
        {
            get
            {
                return this["webServiceUrl"] as string ?? "https://webservices.imageshop.no/v4.asmx";
            }
            set
            {
                this["webServiceUrl"] = value;
            }
        }
        [ConfigurationProperty("initializeTinyMCEPlugin", DefaultValue = true, IsRequired = false)]
        public bool InitializeTinyMCEPlugin
        {
            get
            {
                return (bool)this["initializeTinyMCEPlugin"];
            }
            set
            {
                this["initializeTinyMCEPlugin"] = value;
            }
        }

        public static class UIHint
        {
            public const string ImageshopImage = "ImageshopImage";
            public const string ImageshopImageCollection = "ImageshopImageCollection";
            public const string ImageshopVideo = "ImageshopVideo";
            public const string ImageshopVideoCollection = "ImageshopVideoCollection";
        }

        public static class TinyMCEButtons
        {
            public const string ImageshopImage = "screentekepiimageshop";
        }
    }
}
