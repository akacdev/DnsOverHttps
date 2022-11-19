using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DnsOverHttps
{
    public static class Extensions
    {
        public static string UrlEncode(this string value) => WebUtility.UrlEncode(value);

        public static async Task<string> GetPreview(this HttpResponseMessage res)
        {
            string text = await res.Content.ReadAsStringAsync();
            return text[..Math.Min(text.Length, Constants.MaxPreviewLength)];
        }

        public static async Task<T> Deseralize<T>(this HttpResponseMessage res, JsonSerializerOptions options = null)
        {
            Stream stream = await res.Content.ReadAsStreamAsync();
            if (stream.Length == 0) throw new DnsOverHttpsException("Response content is empty, can't parse as JSON.");

            try
            {
                return await JsonSerializer.DeserializeAsync<T>(stream, options);
            }
            catch (Exception ex)
            {
                throw new DnsOverHttpsException(
                    $"Exception while parsing JSON: {ex.GetType().Name} => {ex.Message}\n" +
                    $"Preview: {await res.GetPreview()}");
            }
        }
    }
}
