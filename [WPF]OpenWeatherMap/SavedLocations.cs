using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace _WPF_OpenWeatherMap
{
    public class SavedLocations
    {
        //Metoda, ki pridobi kot parameter ime mesta in ga shrani v beležko
        //PRED SHRANJEVANJEM MORA TUDI METODA PREGLEDATI ALI JE MESTO ŽE SHRANJENO BILO
        public static void SaveCity(string cityName)
        {
            StreamWriter pisi = File.AppendText("podatki.txt");
            pisi.WriteLine(cityName);
            pisi.Close();
            MessageBox.Show("Podakti so bili uspešno shranjeni!");
        }
    }
}
