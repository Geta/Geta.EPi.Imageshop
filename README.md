# Geta.EPi.Imageshop

![](http://tc.geta.no/app/rest/builds/buildType:(id:TeamFrederik_EpiImageshop_Debug)/statusIcon)
[![Platform](https://img.shields.io/badge/Platform-.NET%204.6.1-blue.svg?style=flat)](https://msdn.microsoft.com/en-us/library/w0x726c2%28v=vs.110%29.aspx)
[![Platform](https://img.shields.io/badge/Episerver-%2011-orange.svg?style=flat)](http://world.episerver.com/cms/)

[Imageshop](http://www.imageshop.org) is an online-based Digital Asset Management (DAM) software. This module integrates Imageshop in the [Episerver](http://www.episerver.com) CMS User Interface. It contains a custom property and a TinyMCE plugin that launches the Imageshop image selection interface in a dialogue.

## How to get started

Start by installing NuGet package (use [Episerver Nuget](http://nuget.episerver.com))

    Install-Package Geta.EPi.Imageshop

### Configure access token

After the package is successfully installed you need to add your access token to configuration section &lt;geta.epi.imageshop&gt; in web.config.

## Basics

Add an Imageshop image property to your content model:

    [BackingType(typeof(PropertyImageshopImage))]
    [UIHint(ImageshopSettings.UIHint.ImageshopImage)]
    [ImageshopSettings(InterfaceName = "", DocumentPrefix = "", ProfileID = "", Culture = "nb-NO")]
    [ImageshopSizePreset("Main image (1280x720)", 1280, 720)]
    [ImageshopSizePreset("Thumbnail image (400x300)", 400, 300)]
    public virtual ImageshopImage MainImage { get; set; }

Minimal Imageshop image property example:

    [BackingType(typeof(PropertyImageshopImage))]
    public virtual ImageshopImage MainImage { get; set; }

Render the image property in a view:

    @Html.PropertyFor(m => m.CurrentPage.MainImage)
    
Image collection property:

    [Display(Name = "Bilder")]
    [BackingType(typeof(PropertyImageshopImageCollection))]
    [UIHint(ImageshopSettings.UIHint.ImageshopImageCollection)]
    [ImageshopSettings(ProfileID = "CAROUSEL", ShowCropDialog = false, ShowSizeDialog = false)]
    public virtual IEnumerable<ImageshopImage> Images { get; set; }

Imageshop video property:

	[BackingType(typeof(PropertyImageshopVideo))]
	public virtual ImageshopVideo MainVideo { get; set; }

Render the video property in a view:

	@Html.PropertyFor(m => m.CurrentPage.MainVideo)

Imageshop video collection property:

	[BackingType(typeof(PropertyImageshopVideoCollection))]
    [UIHint(ImageshopSettings.UIHint.ImageshopVideoCollection)]
	public virtual IEnumerable<ImageshopVideo> Videos { get; set; }

## TinyMCE

A TinyMCE plugin is included for browsing Imageshop images to add to your XhtmlString properties. It's located in the "media" group.

## Configuration

| Parameter		      		| Type       | Description                                                                      	|
| ------------------------- | ---------- | ------------------------------------------------------------------------------------ |
| baseUrl        			| string     | Base URL to Imageshop client. Default is http://client.imageshop.no/InsertImage.aspx |
| token          			| string     | Token identifying the user.                                                      	|
| interfaceName  			| string     | Standard interface used when searching images.                                  		|
| documentPrefix 			| string     | Standard document code prefix used when uploading images.                        	|
| culture        			| string     | Language for the client. Supports en-US and nb-NO. Norwegian is default (nb-NO). 	|
| showSizeDialog 			| true/false | Indicates whether the size dialogue should be shown. Default is true.            	|
| showCropDialog		 	| true/false | Indicates whether the crop dialogue should be show. Default is true.             	|
| initializeTinyMCEPlugin	| true/false | Version 1.7.0, enables plug in for TinyMCE v > 2.0						            |

See configuration section &lt;geta.epi.imageshop&gt; in web.config for examples.

## Screenshots

![ScreenShot](/docs/epi-dialogue.jpg)

![ScreenShot](/docs/imageshop-selection.jpg)

![ScreenShot](/docs/tinymce-plugin.jpg)

## Changelog
2.1. Support for Episerver 11
2.2. Version 1.7.0 Support for TinyMCE > 2.0. Added new configuration parameter for TinyMCE plugin initialization.
