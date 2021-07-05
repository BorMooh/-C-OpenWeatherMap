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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            APIHelper.InitializeClient();
        }
    }
}
