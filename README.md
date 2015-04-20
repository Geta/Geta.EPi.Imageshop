# Geta.EPi.Imageshop
Imageshop plugin for EPiServer

## How to get started

Start by installing NuGet package (use [EPiServer Nuget](http://nuget.episerver.com))

    Install-Package Geta.EPi.Imageshop

### Configure access token

After the package is successfully installed you need to add your access token to configuration section &lt;geta.epi.imageshop&gt; in web.config.

## Basics

Add a Imageshop property to your model:

    [BackingType(typeof(PropertyImageshopImage))]
    [UIHint(ImageshopSettings.UIHint.ImageshopImage)]
    [ImageshopSettings(InterfaceName = "", DocumentPrefix = "", ProfileID = "", Culture = "nb-NO")]
    [ImageshopSizePreset("Main image (1280x720)", 1280, 720)]
    [ImageshopSizePreset("Thumbnail image (400x300)", 400, 300)]
    public virtual ImageshopImage MainImage { get; set; }
    
Add a display template, ImageshopImage.cshtml:

    @model Geta.EPi.Imageshop.ImageshopImage
    @if (Model != null && !string.IsNullOrWhiteSpace(Model.Url))
    {
        <img src="@Model.Url" alt="@ViewData["ImageAltText"]">
    }

Render the property in a view:

    @Html.PropertyFor(m => m.CurrentPage.MainImage)

## TinyMCE

A TinyMCE plugin is included and can be added to your XhtmlString properties. It's located in the "media" group.

## Configuration

See configuration section &lt;geta.epi.imageshop&gt; in web.config for examples.
