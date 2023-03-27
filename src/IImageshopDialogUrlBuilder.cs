using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Screentek.EPi.Imageshop
{
    public interface IImageshopDialogUrlBuilder
    {
        UriBuilder BuildDialogUrl(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes, bool addVideoParameter);
        NameValueCollection BuildDialogQuery(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes, bool addVideoParameter);
    }
}