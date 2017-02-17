using System;
using System.Collections.Generic;
using EPiServer.Shell;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Geta.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(IEnumerable<ImageshopImage>))]
    public class ImageshopImageCollectionEditorDescriptor : ImageshopEditorDescriptorBase
    {
        public ImageshopImageCollectionEditorDescriptor()
        {
            base.ClientEditingClass = "geta-epi-imageshop/widgets/ImageCollection";
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);
            metadata.CustomEditorSettings["uiType"] = metadata.ClientEditingClass;
            metadata.CustomEditorSettings["uiWrapperType"] = UiWrapperType.Flyout;
            metadata.EditorConfiguration["uiWrapperType"] = UiWrapperType.Flyout;
        }
    }
}
