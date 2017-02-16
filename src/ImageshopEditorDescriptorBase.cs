using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using EPiServer.Globalization;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Geta.EPi.Imageshop.Configuration;
using Geta.EPi.Imageshop.Extensions;

namespace Geta.EPi.Imageshop
{
    public abstract class ImageshopEditorDescriptorBase : EditorDescriptor
    {
        protected readonly NameValueCollection ImageshopDialogQuery;
        protected UriBuilder ImageShopDialogUrl;

        protected ImageshopEditorDescriptorBase()
        {
            ImageshopDialogQuery = new NameValueCollection();
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            ImageshopSettingsAttribute configurationAttribute = GetConfigurationAttribute(metadata, attributes);
            IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes = GetSizePresetAttributes(metadata, attributes);

            UriBuilder dialogUrl = BuildDialogUrl(configurationAttribute, sizePresetAttributes);
            metadata.EditorConfiguration.Add("baseUrl", dialogUrl.ToString());
            metadata.EditorConfiguration.Add("preferredLanguage", MapToImageshopLanguage(ContentLanguage.PreferredCulture));

            if (configurationAttribute != null)
            {
                metadata.EditorConfiguration.Add("cropName", configurationAttribute.CropName);
                metadata.EditorConfiguration.Add("previewCropName", configurationAttribute.PreviewCropName);
            }
        }

        protected virtual string MapToImageshopLanguage(CultureInfo cultureInfo)
        {
            CultureInfo neutralCulture = cultureInfo.IsNeutralCulture == false ? cultureInfo.Parent : cultureInfo;

            return neutralCulture.Name.ToLowerInvariant();
        }

        protected virtual NameValueCollection BuildDialogQuery(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes)
        {
            string token = ImageshopSettings.Instance.Token;
            string interfaceName = ImageshopSettings.Instance.InterfaceName;
            string documentPrefix = ImageshopSettings.Instance.DocumentPrefix;
            string culture = ImageshopSettings.Instance.Culture;
            string profileId = ImageshopSettings.Instance.ProfileID;
            bool showSizeDialog = ImageshopSettings.Instance.ShowSizeDialog;
            bool showCropDialog = ImageshopSettings.Instance.ShowCropDialog;
            string sizePresets = ImageshopConfigurationSection.Instance.FormattedSizePresets;

            var query = HttpUtility.ParseQueryString(string.Empty);

            query.Add("IFRAMEINSERT", "true");
            query.Add("IMAGESHOPTOKEN", token);

            // Read settings from attribute.
            if (configurationAttribute != null)
            {
                if (string.IsNullOrWhiteSpace(configurationAttribute.InterfaceName) == false)
                {
                    interfaceName = configurationAttribute.InterfaceName;
                }

                if (string.IsNullOrWhiteSpace(configurationAttribute.DocumentPrefix) == false)
                {
                    documentPrefix = configurationAttribute.DocumentPrefix;
                }

                if (string.IsNullOrWhiteSpace(configurationAttribute.Culture) == false)
                {
                    culture = configurationAttribute.Culture;
                }

                if (string.IsNullOrWhiteSpace(configurationAttribute.ProfileID) == false)
                {
                    profileId = configurationAttribute.ProfileID;
                }

                showSizeDialog = configurationAttribute.ShowSizeDialog;
                showCropDialog = configurationAttribute.ShowCropDialog;
            }

            query.Add("SHOWSIZEDIALOGUE", showSizeDialog.ToString().ToLowerInvariant());
            query.Add("SHOWCROPDIALOGUE", showCropDialog.ToString().ToLowerInvariant());
            query.AddIfNotNull("IMAGESHOPINTERFACENAME", interfaceName);
            query.AddIfNotNull("IMAGESHOPDOCUMENTPREFIX", documentPrefix);
            query.AddIfNotNull("PROFILEID", profileId);

            // Read and apply size preset attributes
            if (sizePresetAttributes != null)
            {
                sizePresets = "";
                int index = 0;

                foreach (var preset in sizePresetAttributes)
                {
                    if (index++ > 0)
                    {
                        sizePresets += ":";
                    }

                    var presetString = string.Format("{0};{1}x{2}", preset.Name, preset.Width, preset.Height);
                    sizePresets += presetString;
                }
            }

            query.AddIfNotNull("IMAGESHOPSIZES",  sizePresets);
            query.AddIfNotNull("CULTURE", culture);
            query.Add("FORMAT", "json");

            return query;
        }

        protected virtual UriBuilder BuildDialogUrl(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes)
        {
            string baseUrl = ImageshopSettings.Instance.BaseUrl;
            NameValueCollection query = BuildDialogQuery(configurationAttribute, sizePresetAttributes);

            ImageShopDialogUrl = new UriBuilder(baseUrl);
            ImageShopDialogUrl.Query = query.ToString();

            return ImageShopDialogUrl;
        }

        protected virtual ImageshopSettingsAttribute GetConfigurationAttribute(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            return attributes.OfType<ImageshopSettingsAttribute>().FirstOrDefault() ??
                   metadata.ContainerType.GetCustomAttribute<ImageshopSettingsAttribute>(true);
        }

        protected virtual IEnumerable<ImageshopSizePresetAttribute> GetSizePresetAttributes(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            return attributes.OfType<ImageshopSizePresetAttribute>();
        }
    }
}
