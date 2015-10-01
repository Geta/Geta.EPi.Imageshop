# Geta.EPi.Imageshop
[Imageshop](http://www.imageshop.org) is an online-based Digital Asset Management (DAM) software. This module integrates Imageshop in the [EPiServer](http://www.episerver.com) CMS User Interface. It contains a custom property and a TinyMCE plugin that launches the Imageshop image selection interface in a dialogue.

## How to get started

Start by installing NuGet package (use [EPiServer Nuget](http://nuget.episerver.com))

    Install-Package Geta.EPi.Imageshop

### Configure access token

After the package is successfully installed you need to add your access token to configuration section &lt;geta.epi.imageshop&gt; in web.config.

## Basics

Add a Imageshop property to your content model:

    [BackingType(typeof(PropertyImageshopImage))]
    [UIHint(ImageshopSettings.UIHint.ImageshopImage)]
    [ImageshopSettings(InterfaceName = "", DocumentPrefix = "", ProfileID = "", Culture = "nb-NO")]
    [ImageshopSizePreset("Main image (1280x720)", 1280, 720)]
    [ImageshopSizePreset("Thumbnail image (400x300)", 400, 300)]
    public virtual ImageshopImage MainImage { get; set; }

Minimal Imageshop property example:

    [BackingType(typeof(PropertyImageshopImage))]
    [UIHint(ImageshopSettings.UIHint.ImageshopImage)]
    public virtual ImageshopImage MainImage { get; set; }

Render the property in a view:

    @Html.PropertyFor(m => m.CurrentPage.MainImage)

## TinyMCE

A TinyMCE plugin is included and can be added to your XhtmlString properties. It's located in the "media" group.

## Configuration

| Parameter      | Type       | Description                                                                      |
| -------------- | ---------- | -------------------------------------------------------------------------------- |
| baseUrl        | string     | Base URL to imageshop client. Default is http://client.imageshop.no/InsertImage.aspx?IFRAMEINSERT=true |
| token          | string     | Token identifying the user.                                                      |
| interfaceName  | string     | Standard interface used when searching images.                                   |
| documentPrefix | string     | Standard document code prefix used when uploading images.                        |
| culture        | string     | Language for the client. Supports en-US and nb-NO. Norwegian is default (nb-NO). |
| showSizeDialog | true/false | Indicates whether the size dialogue should be shown. Default is true.            |
| showCropDialog | true/false | Indicates whether the crop dialogue should be show. Default is true.             |

See configuration section &lt;geta.epi.imageshop&gt; in web.config for examples.

## Screenshots

![ScreenShot](/docs/epi-dialogue.jpg)

![ScreenShot](/docs/imageshop-selection.jpg)

![ScreenShot](/docs/tinymce-plugin.jpg)
