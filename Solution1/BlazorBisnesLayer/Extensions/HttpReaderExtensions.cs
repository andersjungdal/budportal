using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorBusinessLogic.Extensions
{
    static class HttpReaderExtensions
    {
        public static async Task<T> ReadFromJasonAsync<T>(this HttpResponseMessage message)
        {
            return JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
        }
    }
}
