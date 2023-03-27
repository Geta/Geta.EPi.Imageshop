using System.Collections.Generic;
using EPiServer.PlugIn;

namespace Screentek.EPi.Imageshop
{
    [PropertyDefinitionTypePlugIn(DisplayName = "Imageshop Image Collection")]
    public class PropertyImageshopImageCollection : PropertyJsonSerializedObject<IEnumerable<ImageshopImage>>
    {
    }
}
