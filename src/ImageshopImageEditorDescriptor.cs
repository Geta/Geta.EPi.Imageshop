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
    }
}
