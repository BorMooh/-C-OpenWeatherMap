using _WPF_OpenWeatherMap.Model;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _WPF_OpenWeatherMap.FiveDayForecast
{
    public class DataProcessorFiveDays
    {
        public static WeatherModelFiveDay.Root GetWeatherFiveDay(string city = "Zelezniki")
        {
            //Z web-clientom pridobimo vse podatke iz določenega URL naslova, ki nam vrne JSON izpis. 
            using (WebClient client = new WebClient())
            {
                //URL 
                string url = string.Format($"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid=16f364f0a3b530faec39488f8a34aab3");


                //Z WebClientom prenesemo podatke iz našega URL-ja
                var json = client.DownloadString(url);

                //Rezultat deserializiramo z JsonConverter, in to shranimo kot tip WeatherModelFiveDay
                var result = JsonConvert.DeserializeObject<WeatherModelFiveDay.Root>(json);
                WeatherModelFiveDay.Root output = result;


                //Metoda vrne tip WeatherModel z vsemi podatki
                return output;
            }


        }
    }
}
