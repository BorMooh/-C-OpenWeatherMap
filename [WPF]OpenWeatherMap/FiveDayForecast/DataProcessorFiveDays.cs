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

        #region Metoda za pridobivanje podatkov - GetWeatherFiveDay
        /// <summary>
        /// Metoda za pridobivanje podatkov iz API-ja za določeno mesto - to mesto dobimo kot parameter
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
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
        #endregion
        #region Pridobivanje podatkov za vsak dan ob 12ih - 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dan"></param>
        /// <returns>
        /// OUTPUT MORA BITI INT - ŠTEVILKA List[i], ki je za ta DAN
        /// </returns>
        public static int PridobiPodatke(WeatherModelFiveDay.Root podatki, string dan)
        {
            string output = "";

            //Vsakem elementu preverimo uro, ki mora biti 12:00:00 - ker bo program prikazoval vreme za ob 12ih 
            for(int i = 0; i < podatki.list.Count; i++)
            {
                if(podatki.list[i].dt_txt.Remove(0,11) == "12:00:00")
                {
                    output += podatki.list[i].dt_txt + ", ";
                }

            }



            return 1;
        }

        #endregion
    }
}
