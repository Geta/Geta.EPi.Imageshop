# Geta.EPi.ImageShop
Imageshop plugin for EPiServer

## How to get started

Start by installing NuGet package (use [EPiServer Nuget](http://nuget.episerver.com))

    Install-Package Geta.EPi.ImageShop

# Basics

Add a Imageshop property to your model:

    [BackingType(typeof(PropertyImageShopImage))]
    [UIHint(ImageShopSettings.UIHint.ImageShopImage)]
    [ImageShopSettings(InterfaceName = "", DocumentPrefix = "", ProfileID = "", Culture = "nb-NO")]
    [ImageShopSizePreset("Main image (1280x720)", 1280, 720)]
    [ImageShopSizePreset("Thumbnail image (400x300)", 400, 300)]
    public virtual ImageShopImage MainImage { get; set; }
    
Add a display template called ImageShopImage.cshtml:

    @model Geta.EPi.ImageShop.ImageShopImage
    @if (Model != null &&!string.IsNullOrWhiteSpace(Model.Url))
    {
        <img src="@Model.Url" alt="@ViewData["ImageAltText"]">
    }

Render the property in a view:

    @Html.PropertyFor(m => m.CurrentPage.MainImage)

# Configuration

See configuration section <geta.epi.imageshop> in web.config for examples. It is important to also add your access token there.
