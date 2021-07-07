using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using _WPF_OpenWeatherMap.Model;
using Newtonsoft.Json;

namespace _WPF_OpenWeatherMap
{
    public class DataProcessor
    {
        /// <summary>
        /// Metoda, s katero pridobimo podatke o vnešenem mestu. Kot parameter vnesemo določeno mesto 
        /// </summary>
        /// <param name="city"></param>
        public static WeatherModel.Root GetWeather(string city = "Zelezniki")
        {
            //Z web-clientom pridobimo vse podatke iz določenega URL naslova, ki nam vrne JSON izpis. 
            using (WebClient client = new WebClient())
            {
                //URL 
                string url = string.Format($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=16f364f0a3b530faec39488f8a34aab3");


                //Z WebClientom prenesemo podatke iz našega URL-ja
                var json = client.DownloadString(url);

                //Rezultat deserializiramo z JsonConverter, in to shranimo kot tip WeatherModel
                var result = JsonConvert.DeserializeObject<WeatherModel.Root>(json);
                WeatherModel.Root output = result;


                //Metoda vrne tip WeatherModel z vsemi podatki
                return output;
            }


        }

        //Metoda za pridobivanje ikone 
        public static string GetIcon(string id)
        {
            string fullpath = "http://openweathermap.org/img/wn/";
            fullpath += id + ".png";

            return fullpath;
        }

        //Metoda za pridobivanje Google Maps linka iz latitude in longitude
        public static string GetLink(double lat, double lon)
        {
            return $"https://www.google.com/maps/place/{lat},{lon}";
        }
    }



}
