using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Geta.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageshopVideo))]
    public class ImageshopVideoEditorDescriptor : ImageshopEditorDescriptorBase
    {
        protected override bool IsVideoDescriptor { get { return true; } }

        public ImageshopVideoEditorDescriptor()
        {
            base.ClientEditingClass = "geta-epi-imageshop/widgets/ImageSelector";
        }
    }
}
