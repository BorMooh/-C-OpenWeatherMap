using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _WPF_OpenWeatherMap.Model;


namespace _WPF_OpenWeatherMap
{
    #region README
    /// <summary>
    ///  TODO
    ///     UTF-8 PODPORA ZA ŠUMNIKE
    ///     OPTIMIZACIJA KODE ZA DODAJANJE SLIKE/IKONE
    ///     COMBOBOX TEXT PLACEMENT - TEKST NI PORAVNAN - OK
    ///     CITY LIST - DA IMAMO VSA MESTA NOTRI V COMBOBOXU - OK?
    ///     SLIKE SO LOWRES???
    ///     VEČ POLJ - OK 
    ///     FAVOURITES - SHRANJEVANJE MEST + ODSTRANJEVANJE SHRANJENIH MEST - OK 
    ///     ODPRE DOLOČENO MESTO TUDI Z GOOGLE MAPSOV - NuGet?
    ///     5 DAY napoved - Nov FORM
    ///     ANG/SLO
    ///     
    ///     GUMB SHRANI/IZBRIŠI SE MORA REFRESHATI OB VSAKEM KLIKU - TAKO DA JE NAPIS ZMERAJ PRAVILEN(potrebno je pritisniti na Search gumb da se refresha)
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
            tedenskaButton.IsEnabled = false;
            //Nastavimo ItemSource COMBOBOXA kot beležko shranjenih mest
        }
        //javni model WeatherModel, v katerem so podatki 
        //Podatki se notri vnesejo ob kliku na "Search" gumb
        public static WeatherModel.Root podatki;



        #region Window Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inputField.ItemsSource = SavedLocations.PridobiMesta();
        }
        #endregion
        #region Search gumb
        //Ob kliku na gumb "Search"
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Podatke probamo pridobiti - če to ne deluje, nam program izpiše da je prišlo do napake
            try
            {
                podatki = new WeatherModel.Root();
                podatki = DataProcessor.GetWeather(inputField.Text); 

                //Metoda, s katero se podatki vnesejo v polja
                InsertData();

                //Spremenimo napis gumba z metodo - gumb ima 2 možnosti: Shrani, Izbriši
                shranjeniButton.Content = SavedLocations.SpremeniNapis(podatki.name);

                //Nastavimo gumb za 5 dnevno napoved na VISIBLE, tako da ga uporanbik lahko pritisne
                tedenskaButton.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Prišlo je do napake!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                tedenskaButton.IsEnabled = false;
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
            weatherIcon.Source = DataProcessor.GetIcon(podatki.weather[0].icon);

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
            System.Diagnostics.Process.Start(DataProcessor.GetLink(podatki.coord.lat, podatki.coord.lon));
        }

        #endregion
        #region Shranjene lokacije gumb
        private void shranjeniButton_Click(object sender, RoutedEventArgs e)
        {
            //Shranimo mesto v beležko 
            SavedLocations.SaveCity(podatki.name);
            
            //Metodo je potrebno ponovno poklicati da se novi podatki dodajo v ComboBox
            inputField.ItemsSource = SavedLocations.PridobiMesta();
        }
        #endregion
        #region 5 dnevna napoved
        /// <summary>
        /// Klik na gumb za 5 dnevno napoved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tedenskaButton_Click(object sender, RoutedEventArgs e)
        {
            PetDniNapovedF noviForm = new PetDniNapovedF();
            noviForm.Show();
        }
        #endregion

    }
}
