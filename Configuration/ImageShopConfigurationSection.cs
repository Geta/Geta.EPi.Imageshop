using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace Geta.EPi.ImageShop.Configuration
{
    public class ImageShopConfigurationSection : ConfigurationSection
    {
        private static ImageShopConfigurationSection _instance;
        private static readonly object Lock = new object();

        public static ImageShopConfigurationSection Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = GetSection());
                }
            }
        }

        public static ImageShopConfigurationSection GetSection()
        {
            var section = WebConfigurationManager.GetSection("geta.epi.imageshop") as ImageShopConfigurationSection;

            if (section == null)
            {
                throw new ConfigurationErrorsException("The <geta.epi.imageshop> configuration section could not be found in web.config.");
            }

            return section;
        }

        [ConfigurationProperty("settings", IsRequired = true)]
        public ImageShopSettings Settings
        {
            get
            {
                return (ImageShopSettings)base["settings"];
            }
        }

        [ConfigurationProperty("sizePresets", IsDefaultCollection = false, IsRequired = false)]
        [ConfigurationCollection(typeof(ConfigurationElementCollection<ImageShopSizePresetElement>), AddItemName = "add", RemoveItemName = "remove", ClearItemsName = "clear", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
        public ConfigurationElementCollection<ImageShopSizePresetElement> SizePresets
        {
            get
            {
                return (ConfigurationElementCollection<ImageShopSizePresetElement>)base["sizePresets"];
            }
            set { base["sizePresets"] = value; }
        }

        public string FormattedSizePresets
        {
            get
            {
                if (SizePresets == null || SizePresets.Count == 0)
                {
                    return string.Empty;
                }

                var sizePresets = new List<string>();

                foreach (ImageShopSizePresetElement sizePreset in SizePresets)
                {
                    sizePresets.Add(string.Format("{0};{1}x{2}", sizePreset.Name, sizePreset.Width, sizePreset.Height));
                }

                return string.Join(":", sizePresets);
            }
        }
    }
}