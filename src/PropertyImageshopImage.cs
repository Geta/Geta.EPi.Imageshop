using EPiServer.PlugIn;

namespace Geta.EPi.Imageshop
{
    [PropertyDefinitionTypePlugIn(DisplayName = "Imageshop Image")]
    public class PropertyImageshopImage : PropertyJsonSerializedObject<ImageshopImage>
    {
    }
}
