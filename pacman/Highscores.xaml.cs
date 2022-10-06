using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

namespace pacman
{
    /// <summary>
    /// Interaction logic for Highscores.xaml
    /// </summary>
    public partial class Highscores : Window
    {
        public Highscores()
        {
            InitializeComponent();
        }

        private void ScoresNaarMain(object sender, RoutedEventArgs e)
        {
            MainWindow gw = new MainWindow();
            gw.Visibility = Visibility.Visible;
            this.Close();
        }

        private void AddHighscoresToDatabase(int highscore)
        {
            string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\school\\jaar1.2\\periode_1\\programmeren\\FlappyBird\\pacman\\Data\\" +
                "database_score.mdf;Integrated Security=True";
            string query = "insert into [Highscores] (name) (score) values () (txtscore)";

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = query;
                command.CommandText = Convert.ToString(CommandType.Text);
                command.Connection = connection;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
            }
        }
    }
}
