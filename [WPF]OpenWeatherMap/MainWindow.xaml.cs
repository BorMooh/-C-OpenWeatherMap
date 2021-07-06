using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _WPF_OpenWeatherMap.Model;
using Newtonsoft.Json;

namespace _WPF_OpenWeatherMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //API: `http://api.openweathermap.org/data/2.5/weather?q=${inputLocation}&appid=16f364f0a3b530faec39488f8a34aab3`;
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getWeather();
            
        }


        /// <summary>
        /// Metoda, s katero pridobimo podatke o vnešenem mestu. Kot parameter vnesemo določeno mesto 
        /// </summary>
        /// <param name="city"></param>
        public  void getWeather(string city = "Zelezniki")
        {

            //Z web-clientom pridobimo vse podatke iz določenega URL naslova, ki nam vrne JSON izpis. 
            using(WebClient client = new WebClient())
            {
                //URL 
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q=zelezniki&appid=16f364f0a3b530faec39488f8a34aab3");

                //Z WebClientom prenesemo podatke iz našega URL-ja
                var json = client.DownloadString(url);
                
                //Rezultat deserializiramo z JsonConverter, in to shranimo kot tip WeatherModel
                var result = JsonConvert.DeserializeObject<WeatherModel.Root>(json);
                WeatherModel.Root output = result;

                //IZPIS
                labelTest.Content = output.weather[0].description;
            }
            
        }
    }
}
