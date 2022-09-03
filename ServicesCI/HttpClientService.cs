using System.Text;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ControlIDMvc.ServicesCI
{
    public class HttpClientService
    {
        public string session { get; set; }
        static readonly HttpClient client = new HttpClient();
        public async Task<Respose> Run(string host, string url, object str)
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

                 return new Respose()
                {
                    estado = true,
                    data = responseBody
                };
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new Respose()
                {
                    estado = false,
                    data = "error"
                };
            }
        }
        public async Task<Respose> LoginRun(string host, string url, object str)
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
                return new Respose()
                {
                    estado = true,
                    data = json.GetValue("session").ToString()
                };
            }
            catch (HttpRequestException e)
            {
                System.Console.WriteLine(e);
                return new Respose()
                {
                    estado = false,
                    data = "error"
                };
            }
        }
    }
    public class Respose
    {
        public bool estado { get; set; }
        public string data { get; set; }
    }
}