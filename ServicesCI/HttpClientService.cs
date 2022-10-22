using System.Text;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ControlIDMvc.ServicesCI
{
    public class HttpClientService
    {
        static readonly HttpClient client = new HttpClient();
        public async Task<Response> Run(string host, int port, string url, object str, string session)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(str), UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                System.Console.WriteLine($"http://{host}:{port}/{url}?session={session}");
                Console.WriteLine(JsonConvert.SerializeObject(str, Formatting.Indented));
                HttpResponseMessage response = await client.PostAsync($"http://{host}:{port}/{url}?session={session}", stringContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody + "1" + response.StatusCode.ToString());

                return new Response()
                {
                    estado = true,
                    data = responseBody
                };
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new Response()
                {
                    estado = false,
                    data = "error"
                };
            }
        }
        public async Task<Response> RunBlank(string host, int port, string url,string session)
        {
            try
            {
                System.Console.WriteLine($"http://{host}/{url}?session={session}");
                HttpResponseMessage response = await client.PostAsync($"http://{host}:{port}/{url}?session={session}", null);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody + "1" + response.StatusCode.ToString());

                return new Response()
                {
                    estado = true,
                    data = responseBody
                };
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new Response()
                {
                    estado = false,
                    data = "error"
                };
            }
        }
        public async Task<Response> LoginRun(string host, int port, string url, object str,string session)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(str, Formatting.Indented), UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                System.Console.WriteLine($"http://{host}:{port}/{url}?session={session}");
                Console.WriteLine(JsonConvert.SerializeObject(str, Formatting.Indented));
                HttpResponseMessage response = await client.PostAsync($"http://{host}:{port}/{url}?session={session}", stringContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);
                Console.WriteLine(responseBody + "1" + response.StatusCode.ToString());
                return new Response()
                {
                    estado = true,
                    data = json.GetValue("session").ToString()
                };
            }
            catch (HttpRequestException e)
            {
                System.Console.WriteLine(e);
                return new Response()
                {
                    estado = false,
                    data = "error"
                };
            }
        }
    }
    public class Response
    {
        public bool estado { get; set; }
        public string data { get; set; }
    }
    public class responseApi
    {
        public List<int> ids { get; set; }
    }
}