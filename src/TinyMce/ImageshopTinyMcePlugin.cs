using EPiServer.Editor.TinyMCE;

namespace Geta.EPi.Imageshop.TinyMce
{
    [TinyMCEPluginButton(
        PlugInName = "getaepiimageshop",
        ButtonName = "getaepiimageshopbutton", 
        GroupName = "media",
        LanguagePath = "/admin/tinymce/plugins/getaepiimageshop/getaepiimageshop",
        IconUrl = "images/icon.gif"
    )]
    public class ImageshopTinyMcePlugin
    {
    }
}
