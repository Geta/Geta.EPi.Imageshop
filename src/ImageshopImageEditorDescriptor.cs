using System;
using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Geta.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageshopImage))]
    public class ImageshopImageEditorDescriptor : ImageshopEditorDescriptorBase
    {
        public ImageshopImageEditorDescriptor()
        {
            base.ClientEditingClass = "geta-epi-imageshop/widgets/ImageSelector";
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);
            metadata.EditorConfiguration.Add("isVideo", false);
        }
    }
}
