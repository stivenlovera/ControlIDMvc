using System.Text;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ControlIDMvc.Services
{
    public class HttpClientService
    {
        static readonly HttpClient client = new HttpClient();
        public HttpClientService()
        {
        }
        public async Task Run(string host, string url, object str)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(str), UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                HttpResponseMessage response = await client.PostAsync($"http://{host}/{url}", stringContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody + "1" + response.StatusCode.ToString());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}