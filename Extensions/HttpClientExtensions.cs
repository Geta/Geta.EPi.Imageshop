using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Geta.EPi.Imageshop.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ExecuteAsync<T>(this HttpClient client, HttpRequestMessage request)
        {
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var resultData = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var serializer = new XmlSerializer(typeof(T));
            var data = (T)serializer.Deserialize(resultData);
            return data;
        }
    }
}
