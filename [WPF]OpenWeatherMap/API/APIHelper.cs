using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _WPF_OpenWeatherMap
{
    public class APIHelper
    {
        public static HttpClient APIClient { get; set; }

        //Inicializacija našega HTTP klienta - nastavimo da pridobi podatke tipa JSON
        public static void InitializeClient()
        {
            APIClient = new HttpClient();
            APIClient.DefaultRequestHeaders.Accept.Clear();
            APIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


    }
}
