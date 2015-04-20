using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Geta.EPi.ImageShop.Configuration;

namespace Geta.EPi.ImageShop
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageShopImage), UIHint = ImageShopSettings.UIHint.ImageShopImage)]
    public class ImageShopImageEditorDescriptor : EditorDescriptor
    {
        private string _imageShopUrl;

        public ImageShopImageEditorDescriptor()
        {
            base.ClientEditingClass = "geta-epi-imageshop.widgets.imageSelector";
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);
            string dialogUrl = BuildDialogUrl(attributes);
            metadata.EditorConfiguration.Add("baseUrl", dialogUrl);
        }

        protected virtual string BuildDialogUrl(IEnumerable<Attribute> attributes)
        {
            string token = ImageShopSettings.Instance.Token;
            string baseUrl = ImageShopSettings.Instance.BaseUrl;
            string interfaceName = ImageShopSettings.Instance.InterfaceName;
            string documentPrefix = ImageShopSettings.Instance.DocumentPrefix;
            string culture = ImageShopSettings.Instance.Culture;
            string profileId = ImageShopSettings.Instance.ProfileID;
            bool showSizeDialog = ImageShopSettings.Instance.ShowSizeDialog;
            bool showCropDialog = ImageShopSettings.Instance.ShowCropDialog;
            string sizePresets = ImageShopConfigurationSection.Instance.FormattedSizePresets;

            _imageShopUrl = string.Format("{0}&IMAGESHOPTOKEN={1}", baseUrl, token);

            ImageShopSettingsAttribute configurationAttribute = GetConfigurationAttribute(attributes);
            IEnumerable<ImageShopSizePresetAttribute> sizePresetAttributes = GetSizePresetAttributes(attributes);

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

        protected virtual ImageShopSettingsAttribute GetConfigurationAttribute(IEnumerable<Attribute> attributes)
        {
            return attributes.OfType<ImageShopSettingsAttribute>().FirstOrDefault();
        }

        protected virtual IEnumerable<ImageShopSizePresetAttribute> GetSizePresetAttributes(IEnumerable<Attribute> attributes)
        {
            return attributes.OfType<ImageShopSizePresetAttribute>();
        }
    }
}