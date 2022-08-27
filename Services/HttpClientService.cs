using System.Text;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ControlIDMvc.Services
{
    public class HttpClientService
    {
        public string session { get; set; }
        static readonly HttpClient client = new HttpClient();
        public HttpClientService()
        {
        }
        public async Task<string> Run(string host, string url, object str)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(str), UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                System.Console.WriteLine($"http://{host}/{url}?session={this.session}");
                Console.WriteLine(JsonConvert.SerializeObject(str, Formatting.Indented));
                HttpResponseMessage response = await client.PostAsync($"http://{host}/{url}?session={this.session}", stringContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody + "1" + response.StatusCode.ToString());
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "error";
            }
        }


        public async Task<string> LoginRun(string host, string url, object str)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(str, Formatting.Indented), UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                System.Console.WriteLine($"http://{host}/{url}?session={this.session}");
                Console.WriteLine(JsonConvert.SerializeObject(str, Formatting.Indented));
                HttpResponseMessage response = await client.PostAsync($"http://{host}/{url}?session={this.session}", stringContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);
                Console.WriteLine(responseBody + "1" + response.StatusCode.ToString());
                return json.GetValue("session").ToString();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "error";
            }
        }
    }
    public class LoginToken
    {
        public string session { get; set; }
    }
}