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
    #region README
    /// <summary>
    ///  TODO
    ///     UTF-8 PODPORA ZA ŠUMNIKE
    ///     OPTIMIZACIJA KODE ZA DODAJANJE SLIKE/IKONE
    ///     COMBOBOX TEXT PLACEMENT - TEKST NI PORAVNAN - OK
    ///     CITY LIST - DA IMAMO VSA MESTA NOTRI V COMBOBOXU
    ///     SLIKE SO LOWRES???
    ///     VEČ POLJ
    ///     FAVOURITES - SHRANJEVANJE MEST 
    ///     ODPRE DOLOČENO MESTO TUDI Z GOOGLE MAPSOV - NuGet?
    ///     5 DAY napoved? 
    ///     ANG/SLO
    /// </summary>
    /// 

    //API ZA 5 DNI: http://api.openweathermap.org/data/2.5/forecast?q=kranj&appid=16f364f0a3b530faec39488f8a34aab3
    //API: http://api.openweathermap.org/data/2.5/weather?q=zelezniki&appid=16f364f0a3b530faec39488f8a34aab3;
    //ikona: http://openweathermap.org/img/wn/10d@2x.png
    #endregion

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        //javni model WeatherModel, s katerim pridobimo podatke
        //Podatki se notri vnesejo ob kliku na "Search" gumb
        static WeatherModel.Root podatki;


        #region Search gumb
        //Ob kliku na gumb "Search"
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Podatke probamo pridobiti - če to ne deluje, nam program izpiše da je prišlo do napake
            try
            {
                podatki = new WeatherModel.Root();
                podatki = DataProcessor.GetWeather(inputField.Text); // DODAJ PARAMETER - MESTO

                InsertData();
            }
            catch
            {
                MessageBox.Show("Prišlo je do napake!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region Vnos podatkov v polja
        /// <summary>
        /// Metoda s katero se vsi podatki vnesejo v pravilna polja
        /// </summary>
        private void InsertData()
        {
            //Testno ime
            labelMesto.Content = "Prikaz podaktov za: " + podatki.name;

            #region Vreme
            //Za dodajanje slike
            Uri weatherUri = new Uri(DataProcessor.GetIcon(podatki.weather[0].icon.ToString()), UriKind.Absolute);
            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.UriSource = weatherUri;
            bmi.EndInit();
            weatherIcon.Source = bmi;

            weatherDescription.Content = podatki.weather[0].description;
            #endregion
            #region Temperatura
            //Temperatura
            temperatureLabel.Content = Math.Floor(podatki.main.temp - 273.15) + "°c";

            #endregion Temperatura
            #region Veter
            windDegrees.Content = "Hitrost: "+ podatki.wind.speed + "km/h";
            windGust.Content = "Sunek: " + podatki.wind.gust + "km/h";
            windDegrees.Content = "Smer: " + podatki.wind.deg + "°";
            #endregion
            #region Vlaznost
            labelVlaznost.Content = podatki.main.humidity;
            #endregion
            #region Latitude/Longitude
            labelLongitude.Content = podatki.coord.lon;
            labelLatitude.Content = podatki.coord.lat;

            labelCoords.Content = DataProcessor.GetLink(podatki.coord.lat, podatki.coord.lon);
            #endregion
        }
        #endregion
        #region Get Location gumb
        /// <summary>
        /// Ob kliku na gumb "Get Location" se izvede ta metoda.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelGoogleMaps_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Work in Progress", "WIP", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
        #region Shranjene lokacije gumb
        private void shranjeniButton_Click(object sender, RoutedEventArgs e)
        {
            SavedLocations.SaveCity(podatki.name);
        }
        #endregion
    }
}
