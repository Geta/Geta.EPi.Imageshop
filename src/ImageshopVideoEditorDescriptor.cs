using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Geta.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageshopVideo))]
    public class ImageshopVideoEditorDescriptor : ImageshopEditorDescriptorBase
    {
        public ImageshopVideoEditorDescriptor()
        {
            base.ClientEditingClass = "geta-epi-imageshop/widgets/ImageSelector";
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);
            metadata.EditorConfiguration.Add("isVideo", true);
        }

        protected override NameValueCollection BuildDialogQuery(ImageshopSettingsAttribute configurationAttribute, IEnumerable<ImageshopSizePresetAttribute> sizePresetAttributes)
        {
            NameValueCollection query = base.BuildDialogQuery(configurationAttribute, sizePresetAttributes);
            query.Add("SHOWVIDEO", "true");
            return query;
        }
    }
}
