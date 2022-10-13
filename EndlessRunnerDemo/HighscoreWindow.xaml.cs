using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
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

namespace EndlessRunnerDemo
{
    /// <summary>
    /// Interaction logic for HighscoreWindow.xaml
    /// </summary>
    public partial class HighscoreWindow : Window
    {


        //might need to download and reference the connector. Download: https://dev.mysql.com/downloads/connector/net/ 
        string connectionSting = "SERVER=localhost;DATABASE=highscores;UID=root;PASSWORD=;";

        public HighscoreWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            //Connect to DB
            MySqlConnection connection = new MySqlConnection(connectionSting);

            //open connection
            connection.Open();

            //Retreive scores from DB
            MySqlCommand cmd = new MySqlCommand("SELECT score FROM scores ORDER BY score DESC LIMIT 10", connection);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            dt.Columns.Add("rank", typeof(String));

            foreach (DataRow row in dt.Rows)
            {
                row.SetField("rank", dt.Rows.IndexOf(row) + 1 + ".");
            }

            highscoreList.DataContext = dt;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        { 
        
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
        
        }
        }
}
