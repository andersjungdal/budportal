using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ApiGateway.BusinessLogic.Extensions
{
    public static class HttpRequestDeserializingExtension
    {
        public static async Task<T> JasonDeserialize<T>(this HttpRequest httpRequest) where T : class
        {
            string requestBody = await new StreamReader(httpRequest.Body).ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }
}