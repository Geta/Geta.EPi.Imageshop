using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using EPiServer.ServiceLocation;
using Geta.EPi.Imageshop.Configuration;
using Geta.EPi.Imageshop.Extensions;

namespace Geta.EPi.Imageshop
{
    [ServiceConfiguration(typeof(IImageshopDialogUrlBuilder), Lifecycle = ServiceInstanceScope.Singleton)]
    public class DefaulImageshopDialogUrlBuilder : IImageshopDialogUrlBuilder
    {
        public virtual UriBuilder BuildDialogUrl(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes, bool addVideoParameter)
        {
            string baseUrl = ImageshopSettings.Instance.BaseUrl;
            NameValueCollection query = BuildDialogQuery(configurationAttribute, sizePresetAttributes, addVideoParameter);

            var uri = new UriBuilder(baseUrl);
            uri.Query = ConstructQueryString(query);

            return uri;
        }

        public string ConstructQueryString(NameValueCollection parameters)
        {
            var items = new List<string>();

            foreach (string name in parameters)
            {
                items.Add(string.Concat(name, "=", HttpUtility.UrlEncode(parameters[name])));
            }

            return string.Join("&", items.ToArray());
        }

        public virtual NameValueCollection BuildDialogQuery(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes, bool addVideoParameter)
        {
            string token = ImageshopSettings.Instance.Token;
            string interfaceName = ImageshopSettings.Instance.InterfaceName;
            string documentPrefix = ImageshopSettings.Instance.DocumentPrefix;
            string culture = ImageshopSettings.Instance.Culture;
            string profileId = ImageshopSettings.Instance.ProfileID;
            bool showSizeDialog = ImageshopSettings.Instance.ShowSizeDialog;
            bool showCropDialog = ImageshopSettings.Instance.ShowCropDialog;
            bool freeCropDialog = ImageshopSettings.Instance.FreeCrop;
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
                freeCropDialog = configurationAttribute.FreeCrop;
            }

            query.Add("SHOWSIZEDIALOGUE", showSizeDialog.ToString().ToLowerInvariant());
            query.Add("SHOWCROPDIALOGUE", showCropDialog.ToString().ToLowerInvariant());
            query.Add("FREECROP", freeCropDialog.ToString().ToLowerInvariant());
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

            query.AddIfNotNull("IMAGESHOPSIZES", sizePresets);
            query.AddIfNotNull("CULTURE", culture);
            query.Add("FORMAT", "json");

            if (addVideoParameter)
            {
                query.Add("SHOWVIDEO", "true");
            }

            return query;
        }
    }
}