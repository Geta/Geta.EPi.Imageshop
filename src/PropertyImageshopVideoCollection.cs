using System.Collections.Generic;
using EPiServer.PlugIn;

namespace Screentek.EPi.Imageshop
{
    [PropertyDefinitionTypePlugIn(DisplayName = "Imageshop Video Collection")]
    public class PropertyImageshopVideoCollection : PropertyJsonSerializedObject<IEnumerable<ImageshopVideo>>
    {
    }
}
