using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;

namespace Geta.EPi.Imageshop.Configuration
{
    public class ImageshopConfigurationSection : ConfigurationSection
    {
        private static ImageshopConfigurationSection _instance;
        private static readonly object Lock = new object();

        public static ImageshopConfigurationSection Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = GetSection());
                }
            }
        }

        public static ImageshopConfigurationSection GetSection()
        {
            var section = WebConfigurationManager.GetSection("Geta.EPi.Imageshop") as ImageshopConfigurationSection;

            if (section == null)
            {
                throw new ConfigurationErrorsException("The <Geta.EPi.Imageshop> configuration section could not be found in web.config.");
            }

            return section;
        }

        [ConfigurationProperty("settings", IsRequired = true)]
        public ImageshopSettings Settings
        {
            get
            {
                return (ImageshopSettings)base["settings"];
            }
        }

        [ConfigurationProperty("sizePresets", IsDefaultCollection = false, IsRequired = false)]
        [ConfigurationCollection(typeof(ConfigurationElementCollection<ImageshopSizePresetElement>), AddItemName = "add", RemoveItemName = "remove", ClearItemsName = "clear", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
        public ConfigurationElementCollection<ImageshopSizePresetElement> SizePresets
        {
            get
            {
                return (ConfigurationElementCollection<ImageshopSizePresetElement>)base["sizePresets"];
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

                foreach (ImageshopSizePresetElement sizePreset in SizePresets)
                {
                    sizePresets.Add(string.Format("{0};{1}x{2}", sizePreset.Name, sizePreset.Width, sizePreset.Height));
                }

                return string.Join(":", sizePresets);
            }
        }
    }
}
