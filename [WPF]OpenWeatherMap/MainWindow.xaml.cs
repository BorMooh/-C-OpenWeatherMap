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
    ///     COMBOBOX TEXT PLACEMENT - TEKST NI PORAVNAN
    ///     CITY LIST - DA IMAMO VSA MESTA NOTRI V COMBOBOXU
    ///     SLIKE SO LOWRES???
    ///     VEČ POLJ
    /// </summary>
    /// 

    //API: `http://api.openweathermap.org/data/2.5/weather?q=zelezniki&appid=16f364f0a3b530faec39488f8a34aab3`;
    //ikona: http://openweathermap.org/img/wn/10d@2x.png
    #endregion

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        //Ob kliku na gumb "Search"
        static WeatherModel.Root podatki;
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

        #region Vnos podatkov v polja
        /// <summary>
        /// Metoda s katero se vsi podatki vnesejo v pravilna polja
        /// </summary>
        private void InsertData()
        {
            //Testno ime
            labelTest.Content = podatki.name;

            //Za dodajanje slike
            Uri weatherUri = new Uri(DataProcessor.GetIcon(podatki.weather[0].icon.ToString()), UriKind.Absolute);
            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.UriSource = weatherUri;
            bmi.EndInit();
            weatherIcon.Source = bmi;

            weatherDescription.Content = podatki.weather[0].description;


            //Temperatura
            temperatureLabel.Content = Math.Floor(podatki.main.temp - 273.15) + "c";
        }
        #endregion
    }
}
