using System;
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
    /// <summary>
    /// Interaction logic for PetDniNapovedF.xaml
    /// </summary>
    public partial class PetDniNapovedF : Window
    {
        public PetDniNapovedF()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataProcessorFiveDays.GetWeatherFiveDay();
        }
    }
}
