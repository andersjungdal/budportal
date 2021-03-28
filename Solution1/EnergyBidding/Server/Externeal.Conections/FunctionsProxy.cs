using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EnergyBidding.Server.Externeal.Conections
{
    public class FunctionsProxy
    {  
        public string BaseAdresse { get; set; }
        public string FunctionKey { get; set; }
        public HttpClient HttpClient { get; set; }

        public FunctionsProxy(HttpClient httpClient, IConfiguration configuration)
        {
            HttpClient = httpClient;
            FunctionKey = configuration.GetValue<string>("FunctionKey");
        }
        //get
        //put
        //post
        //Deleate
        /// <summary>
        /// </summary>
        /// <typeparam name="TBody">
        /// method defult is GET
        /// </typeparam>
        /// <param name="path"></param>
        /// <param name="method">Get</param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CallFunction<TBody>(string path, TBody body, HttpMethod method = null)
            where TBody: class
        {
            if (path.Contains('?'))
            {
                path += $"&code={FunctionKey}";
            }
            else
            {
                path += $"?code={FunctionKey}";
            }
            string Jsonbody = "";
            Jsonbody = JsonSerializer.Serialize(body);
            using (StringContent content = new StringContent(Jsonbody, Encoding.UTF8, "application/json"))
            {
                HttpRequestMessage message = new HttpRequestMessage(method ?? HttpMethod.Get, BaseAdresse + path);
                message.Content = content;
                return await HttpClient.SendAsync(message).ConfigureAwait(false);
            }
        }
        public async Task<HttpResponseMessage> CallFunctionXml(string path, string bodyXml, HttpMethod method = null)
        {
            if (path.Contains('?'))
            {
                path += $"&code={FunctionKey}";
            }
            else
            {
                path += $"?code={FunctionKey}";
            }
            using (StringContent content = new StringContent(bodyXml, Encoding.UTF8, "application/xml"))
            {
                HttpRequestMessage message = new HttpRequestMessage(method ?? HttpMethod.Get, BaseAdresse + path);
                message.Content = content;
                return await HttpClient.SendAsync(message).ConfigureAwait(false);
            }
        }
        public async Task<HttpResponseMessage> CallFunction(string path, HttpMethod method = null)
        {
            if (path.Contains('?'))
            {
                path += $"&code={FunctionKey}";
            }
            else
            {
                path += $"?code={FunctionKey}";
            }
            string Jsonbody = "";
            using (StringContent content = new StringContent(Jsonbody, Encoding.UTF8, "application/json"))
            {
                HttpRequestMessage message = new HttpRequestMessage(method ?? HttpMethod.Get, BaseAdresse + path);
                message.Content = content;
                return await HttpClient.SendAsync(message).ConfigureAwait(false);
            }
        }
    }
}