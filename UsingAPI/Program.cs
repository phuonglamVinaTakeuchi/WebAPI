using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace UsingAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            RunAsync().Wait();
            Console.ReadKey();
        }

        static async Task RunAsync()
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("/weatherforecast");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<WeatherForecast[]>();
                Console.WriteLine($"Records retrieved: {data.Length}");
                foreach (var d in data)
                {
                    Console.WriteLine($"{d.Date}\t{d.TemperatureC}\t\t{d.Summary}");
                }
            }
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string Summary { get; set; }
    }
}