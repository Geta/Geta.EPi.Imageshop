using EPiServer.Editor.TinyMCE;

namespace Geta.EPi.ImageShop.TinyMce
{
    [TinyMCEPluginButton(
        PlugInName = "getaepiimageshop",
        ButtonName = "getaepiimageshopbutton", 
        GroupName = "media",
        LanguagePath = "/admin/tinymce/plugins/getaepiimageshop/getaepiimageshop",
        IconUrl = "images/icon.gif"
    )]
    public class ImageShopTinyMcePlugin
    {
    }
}