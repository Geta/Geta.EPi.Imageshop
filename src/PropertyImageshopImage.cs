using EPiServer.PlugIn;

namespace Screentek.EPi.Imageshop
{
    [PropertyDefinitionTypePlugIn(DisplayName = "Imageshop Image")]
    public class PropertyImageshopImage : PropertyJsonSerializedObject<ImageshopImage>
    {
    }
}
