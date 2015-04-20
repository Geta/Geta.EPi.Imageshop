using System;
using System.ComponentModel;

namespace Geta.EPi.ImageShop
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ImageShopSettingsAttribute : Attribute
    {
        public ImageShopSettingsAttribute()
        {
            ShowSizeDialog = true;
            ShowCropDialog = true;
        }

        [Description("Name of the default interface (if any).")]
        public string InterfaceName { get; set; }

        [Description("Standard document code prefix used when uploading images.")]
        public string DocumentPrefix { get; set; }

        [Description("Culture (en-US and nb-NO supported). Default is nb-NO.")]
        public string Culture { get; set; }

        [Description("The id of the ImageShop profile to use.")]
        public string ProfileID { get; set; }

        [Description("Show size dialog. Default is true.")]
        public bool ShowSizeDialog { get; set; }

        [Description("Show crop dialog. Default is true.")]
        public bool ShowCropDialog { get; set; }
    }
}