using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Geta.EPi.Imageshop.Configuration;

namespace Geta.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageshopImage), UIHint = ImageshopSettings.UIHint.ImageshopImage)]
    public class ImageshopImageEditorDescriptor : EditorDescriptor
    {
        private string _imageShopUrl;

        public ImageshopImageEditorDescriptor()
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

            ImageshopSettingsAttribute configurationAttribute = GetConfigurationAttribute(attributes);
            IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes = GetSizePresetAttributes(attributes);

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

        protected virtual ImageshopSettingsAttribute GetConfigurationAttribute(IEnumerable<Attribute> attributes)
        {
            return attributes.OfType<ImageshopSettingsAttribute>().FirstOrDefault();
        }

        protected virtual IEnumerable<ImageshopSizePresetAttribute> GetSizePresetAttributes(IEnumerable<Attribute> attributes)
        {
            return attributes.OfType<ImageshopSizePresetAttribute>();
        }
    }
}