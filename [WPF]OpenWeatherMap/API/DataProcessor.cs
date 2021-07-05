﻿using _WPF_OpenWeatherMap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _WPF_OpenWeatherMap
{
    public class DataProcessor
    {
        //Metoda, s katero pridobimo podatke. Metoda sprejme mesto, default vrednost je pa Železniki. 
        public static async Task<WeatherModel> LoadData(string city = "Zelezniki")
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=16f364f0a3b530faec39488f8a34aab3";


            using(HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    WeatherModel weather = await response.Content.ReadAsAsync<WeatherModel>();

                    return weather;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
