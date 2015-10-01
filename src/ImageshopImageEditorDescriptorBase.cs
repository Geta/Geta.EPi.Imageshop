using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Geta.EPi.Imageshop.Configuration;

namespace Geta.EPi.Imageshop
{
    public abstract class ImageshopImageEditorDescriptorBase : EditorDescriptor
    {
        private string _imageShopUrl;

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            ImageshopSettingsAttribute configurationAttribute = GetConfigurationAttribute(metadata, attributes);
            IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes = GetSizePresetAttributes(metadata, attributes);

            string dialogUrl = BuildDialogUrl(configurationAttribute, sizePresetAttributes);
            metadata.EditorConfiguration.Add("baseUrl", dialogUrl);

            if (configurationAttribute != null)
            {
                metadata.EditorConfiguration.Add("cropName", configurationAttribute.CropName);
                metadata.EditorConfiguration.Add("previewCropName", configurationAttribute.PreviewCropName);
            }
        }

        protected virtual string BuildDialogUrl(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes)
        {
            string token = ImageshopSettings.Instance.Token;
            string baseUrl = ImageshopSettings.Instance.BaseUrl;
            string interfaceName = ImageshopSettings.Instance.InterfaceName;
            string documentPrefix = ImageshopSettings.Instance.DocumentPrefix;
            string culture = ImageshopSettings.Instance.Culture;
            string profileId = ImageshopSettings.Instance.ProfileID;
            bool showSizeDialog = ImageshopSettings.Instance.ShowSizeDialog;
            bool showCropDialog = ImageshopSettings.Instance.ShowCropDialog;
            string sizePresets = ImageshopConfigurationSection.Instance.FormattedSizePresets;

            _imageShopUrl = string.Format("{0}&IMAGESHOPTOKEN={1}", baseUrl, token);

            // Read settings from attribute.
            if (configurationAttribute != null)
            {
                if (!string.IsNullOrWhiteSpace(configurationAttribute.InterfaceName))
                {
                    interfaceName = configurationAttribute.InterfaceName;
                }

                if (!string.IsNullOrWhiteSpace(configurationAttribute.DocumentPrefix))
                {
                    documentPrefix = configurationAttribute.DocumentPrefix;
                }

                if (!string.IsNullOrWhiteSpace(configurationAttribute.Culture))
                {
                    culture = configurationAttribute.Culture;
                }

                if (!string.IsNullOrWhiteSpace(configurationAttribute.ProfileID))
                {
                    profileId = configurationAttribute.ProfileID;
                }

                showSizeDialog = configurationAttribute.ShowSizeDialog;
                showCropDialog = configurationAttribute.ShowCropDialog;
            }

            _imageShopUrl += string.Format("&SHOWSIZEDIALOGUE={0}&SHOWCROPDIALOGUE={1}", showSizeDialog.ToString().ToLowerInvariant(), showCropDialog.ToString().ToLowerInvariant());

            if (interfaceName != null)
            {
                _imageShopUrl += string.Format("&IMAGESHOPINTERFACENAME={0}", HttpUtility.UrlEncode(interfaceName));
            }

            if (documentPrefix != null)
            {
                _imageShopUrl += string.Format("&IMAGESHOPDOCUMENTPREFIX={0}", HttpUtility.UrlEncode(documentPrefix));
            }

            if (profileId != null)
            {
                _imageShopUrl += string.Format("&PROFILEID={0}", HttpUtility.UrlEncode(profileId));
            }

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

            if (!string.IsNullOrWhiteSpace(sizePresets))
            {
                _imageShopUrl += "&IMAGESHOPSIZES=" + HttpUtility.UrlEncode(sizePresets);
            }

            if (culture != null)
            {
                _imageShopUrl += string.Format("&CULTURE={0}", culture);
            }

            return _imageShopUrl;
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
