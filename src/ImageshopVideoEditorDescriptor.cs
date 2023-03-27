using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Screentek.EPi.Imageshop
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageshopVideo))]
    public class ImageshopVideoEditorDescriptor : ImageshopEditorDescriptorBase
    {
        protected override bool IsVideoDescriptor { get { return true; } }

        public ImageshopVideoEditorDescriptor()
        {
            base.ClientEditingClass = "screentek-epi-imageshop/widgets/ImageSelector";
        }
    }
}
