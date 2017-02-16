using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using EPiServer.Shell;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Geta.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(IEnumerable<ImageshopVideo>))]
    public class ImageshopVideoCollectionEditorDescriptor : ImageshopEditorDescriptorBase
    {
        public ImageshopVideoCollectionEditorDescriptor()
        {
            base.ClientEditingClass = "geta-epi-imageshop/widgets/ImageCollection";
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);
            metadata.EditorConfiguration.Add("isVideo", true);
            metadata.CustomEditorSettings["uiType"] = metadata.ClientEditingClass;
            metadata.CustomEditorSettings["uiWrapperType"] = UiWrapperType.Flyout;
            metadata.EditorConfiguration["uiWrapperType"] = UiWrapperType.Flyout;
        }

        protected override NameValueCollection BuildDialogQuery(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes)
        {
            NameValueCollection query = base.BuildDialogQuery(configurationAttribute, sizePresetAttributes);
            query.Add("SHOWVIDEO", "true");
            return query;
        }
    }
}
