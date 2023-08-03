using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Screentek.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageshopImage))]
    public class ImageshopImageEditorDescriptor : ImageshopEditorDescriptorBase
    {
        public ImageshopImageEditorDescriptor()
        {
            base.ClientEditingClass = "screentek-epi-imageshop/widgets/ImageSelector";
        }
    }
}
