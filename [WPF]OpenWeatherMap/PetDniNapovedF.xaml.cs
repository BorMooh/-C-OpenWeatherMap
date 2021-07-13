﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _WPF_OpenWeatherMap.FiveDayForecast;

namespace _WPF_OpenWeatherMap
{
    #region README
    /*  TODO
     *      GUMBE, S KATERIM SE POMIKAMO LEVO/DESNO? DA NE PRIKAZUJEMO PODATKOV ZA VSE DNI HKRATI
     *      
     * 
     */

    #endregion

    /// <summary>
    /// Interaction logic for PetDniNapovedF.xaml
    /// </summary>
    public partial class PetDniNapovedF : Window
    {
        //Javne spremenljivke
        public static WeatherModelFiveDay.Root podatkiTeden;

        public static WeatherModelFiveDay.List podatkiDanes;
        public static WeatherModelFiveDay.List podatkiJutri;


        public PetDniNapovedF()
        {
            InitializeComponent();
            
            try
            {
                //Prenos podatkov iz API-ja
                podatkiTeden = DataProcessorFiveDays.GetWeatherFiveDay(MainWindow.podatki.name);
            }
            catch
            {
                MessageBox.Show("Podatki niso prišli iz API");
            }
        }


        #region Window_Load metoda
        //Vsi podatki se avtomatsko vnesejo v form ko se ta naloži.
        //Ker dobimo ime mesta iz prvotnega forma ni potreben noben input za mesto. 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataProcessorFiveDays.PridobiPodatke(podatkiTeden, 0);
            podatkiDanes = DataProcessorFiveDays.PridobiPodatke(podatkiTeden, 0);
            podatkiJutri = DataProcessorFiveDays.PridobiPodatke(podatkiTeden, 1);

            danesTemp.Content = podatkiDanes.main.temp;
            danesVreme.Content = podatkiDanes.weather[0].description; 

            //SKRAJŠAJ KODO!!!
            Uri weatherUri = new Uri(DataProcessor.GetIcon(podatkiDanes.weather[0].icon.ToString()), UriKind.Absolute);
            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.UriSource = weatherUri;
            bmi.EndInit();
            danesIkona.Source = bmi;
            ///
        }
        #endregion


    }
}
