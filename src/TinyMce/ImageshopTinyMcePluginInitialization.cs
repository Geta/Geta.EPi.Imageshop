using System;
using System.Linq;
using EPiServer.Cms.TinyMce.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Screentek.EPi.Imageshop.Configuration;

namespace Screentek.EPi.Imageshop.TinyMce
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
                        .AddExternalPlugin("screentekepiimageshop", "/Util/Editor/tinymce/plugins/screentekepiimageshop/editor_plugin.js")
                        .AppendToolbar("screentekepiimageshop");
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