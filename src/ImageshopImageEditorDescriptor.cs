using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Geta.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageshopImage))]
    public class ImageshopImageEditorDescriptor : ImageshopImageEditorDescriptorBase
    {
        public ImageshopImageEditorDescriptor()
        {
            base.ClientEditingClass = "geta-epi-imageshop.widgets.imageSelector";
        }
    }
}
