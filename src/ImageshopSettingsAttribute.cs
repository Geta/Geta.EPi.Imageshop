using System;
using System.ComponentModel;

namespace Screentek.EPi.Imageshop
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class ImageshopSettingsAttribute : Attribute
    {
        public ImageshopSettingsAttribute()
        {
            ShowSizeDialog = true;
            ShowCropDialog = true;
            FreeCrop = true;
            PreviewCropName = string.Empty;
            CropName = string.Empty;
        }

        [Description("Name of the default interface (if any).")]
        public string InterfaceName { get; set; }

        [Description("Standard document code prefix used when uploading images.")]
        public string DocumentPrefix { get; set; }

        [Description("Culture (en-US and nb-NO supported). Default is nb-NO.")]
        public string Culture { get; set; }

        [Description("The id of the Imageshop profile to use.")]
        public string ProfileID { get; set; }

        [Description("Show size dialog. Default is true.")]
        public bool ShowSizeDialog { get; set; }

        [Description("Show crop dialog. Default is true.")]
        public bool ShowCropDialog { get; set; }

        [Description("Free cropping is enabled when selecting an image. Default is true.")]
        public bool FreeCrop { get; set; }

        [Description("Name of the crop to use for preview when editing. Default is empty string.")]
        public string PreviewCropName { get; set; }

        [Description("Name of the crop to use for the image. Default is empty string.")]
        public string CropName { get; set; }
    }
}
