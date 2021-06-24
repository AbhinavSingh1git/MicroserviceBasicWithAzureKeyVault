
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace BasicMicroservice
{
/*
 public class Weather
 {
     public string description {get;set;}
 }
 public class Main
 {
     public decimal temp {get;set;}
 }
  
  public class Forecast
 {
     public Weather[] weather {get;set;}
     public Main main {get;set;}
     public long dt {get;set;}
 }
*/
    public class WeatherClient
    {
        private readonly HttpClient httpClient;
        private readonly ServiceSettings settings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this.httpClient = httpClient;
            settings = options.Value;
        }
        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] weather, Main main, long dt);
        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            var forecast = await httpClient.GetFromJsonAsync<Forecast>
            ($"https://{settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={settings.ApiKey}&units=metric");
            return forecast;
        }
    }


}
//WeatherClient