using System.Collections.Specialized;
using System.Web;

namespace Geta.EPi.Imageshop.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static void AddIfNotNull(this NameValueCollection nvc, string name, string value)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                nvc.Add(name, value);
            }
        }
    }
}