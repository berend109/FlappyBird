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

namespace pacman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HighscoresButtonClick(object sender, RoutedEventArgs e)
        {
            Highscores gw = new Highscores();
            gw.Visibility = Visibility.Visible;
            this.Close();

        }

        private void StartGameButtonClick(object sender, RoutedEventArgs e)
        {
            GameWindow gw = new GameWindow();
            gw.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
