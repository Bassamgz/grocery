namespace Grocery.Core.Extension
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented, // Just for humans
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(
                json,
                settings);
            return value;
        }
    }
}
