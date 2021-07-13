using _WPF_OpenWeatherMap.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        #region Pridobivanje podatkov za vsak dan ob 12ih - PridobiPodatke
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dan">
        /// Prvi parameter je podatki, drugi pa kateri dan hočemo (0 - danes, 1 - jutri, 2 - pojutrišnem,...)
        /// </param>
        /// <returns>
        /// OUTPUT MORA BITI INT - ŠTEVILKA List[i], ki je za ta DAN
        /// </returns>
        public static WeatherModelFiveDay.List PridobiPodatke(WeatherModelFiveDay.Root podatki, int dan)
        {
            //List v katerem so podatki za vsak dan ob 12ih
            List<WeatherModelFiveDay.List> zbirkaDni = new List<WeatherModelFiveDay.List>();
            zbirkaDni.Add(podatki.list[0]); //V zbirko dodamo prvi podatek, saj to je trenutni dan 

            //Prvi datum v podatkih je trenutni datum 
            int trenutniDan = Convert.ToInt32(podatki.list[0].dt_txt.Substring(8, 2));

            //Če je parameter večji od 5 naj se metoda zaključi
            if (dan > 5)
                return null;

            //Vsakem elementu preverimo uro, ki mora biti 12:00:00 - ker bo program prikazoval vreme za ob 12ih za naslednje dni
            for(int i = 0; i < podatki.list.Count; i++)
            {
                //Preverimo uro - ta mora biti za vse podatke ob 12:00:00
                if(podatki.list[i].dt_txt.Remove(0,11) == "12:00:00")
                {
                    //Preverimo če se datum NE ujema z prvim dnem, saj tega imamo že dodane notri
                    if(Convert.ToInt32(podatki.list[i].dt_txt.Substring(8,2)) != trenutniDan)
                    {
                        zbirkaDni.Add(podatki.list[i]);
                    }
                }
            }

            return zbirkaDni[dan];
        }

        #endregion
        /// <summary>
        /// Metoda, ki iz dt_txt pridobi datum v pravilni Evropski obliki; DD-MM-YYYY
        /// </summary>
        /// <param name="dt_txt">Paramter je dt_txt iz API podatkov</param>
        /// <returns>
        /// DD-MM
        /// </returns>
        public static string PridobiDatum(string dt_txt)
        {
            //Najprej odstranimo čas od celega napisa, tako da ostane samo dan, mesec in leto
            dt_txt = dt_txt.Substring(0,10);
            
            //Vsak element(D,M,Y) dodamo v array, vsakega v svojo celico
            //[0] - Year | [1] - Month | [2] - Day
            string[] datum = dt_txt.Split('-');

            return $"{datum[2]}. {datum[1]}";
        }
    }
}
