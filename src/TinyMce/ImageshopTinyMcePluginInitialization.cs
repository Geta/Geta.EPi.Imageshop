using System;
using System.Linq;
using EPiServer.Cms.TinyMce.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Geta.EPi.Imageshop.Configuration;

namespace Geta.EPi.Imageshop.TinyMce
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ImageshopTinyMcePluginInitialization : IInitializableModule, IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            bool initializeTinyMCEPlugin = ImageshopSettings.Instance.InitializeTinyMCEPlugin;
            if (initializeTinyMCEPlugin)
                context.Services.Configure<TinyMceConfiguration>(config =>
                {
                    config.Default()
                    .AddEpiserverSupport()
                        .AddExternalPlugin("getaepiimageshop", "/Util/Editor/tinymce/plugins/getaepiimageshop/editor_plugin.js")
                        .AppendToolbar("getaepiimageshop");
                });
        }

        public void Initialize(InitializationEngine context)
        {
            //Add initialization logic, this method is called once after CMS has been initialized
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }
}