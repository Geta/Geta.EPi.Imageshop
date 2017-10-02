using System.Collections.Generic;
using EPiServer.PlugIn;

namespace Geta.EPi.Imageshop
{
    [PropertyDefinitionTypePlugIn(DisplayName = "Imageshop Video Collection")]
    public class PropertyImageshopVideoCollection : PropertyJsonSerializedObject<IEnumerable<ImageshopVideo>>
    {
    }
}
