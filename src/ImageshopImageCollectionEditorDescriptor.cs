using System;
using System.Collections.Generic;
using EPiServer.Shell;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Screentek.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(IEnumerable<ImageshopImage>))]
    public class ImageshopImageCollectionEditorDescriptor : ImageshopEditorDescriptorBase
    {
        public ImageshopImageCollectionEditorDescriptor()
        {
            base.ClientEditingClass = "screentek-epi-imageshop/widgets/ImageCollection";
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
