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

using System.Windows.Threading;

namespace pacman
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer();

        bool GoLeft, GoRight, GoDown, GoUp;
        bool NoLeft, NoRight, NoDown, NoUp;

        int speed = 8;

        Rect pacmanhitBox;

        int GhostSpeed = 10;
        int GhostMoveStep = 130;
        int CurrentghostStep;
        int Score = 0;

        public GameWindow()
        {
            InitializeComponent();

            GameSetup();
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GameSetup()
        {

            MyCanvas.Focus();

            gametimer.Tick += Gameloop;
            gametimer.Interval = TimeSpan.FromMilliseconds(20);
            gametimer.Start();
            CurrentghostStep = GhostMoveStep;

            //makes it so that the canvas of pacman and the ghosts are filled and so appear as the image

            ImageBrush pacmanimage = new ImageBrush();
            pacmanimage.ImageSource = new BitmapImage(new Uri("C:\\school\\jaar1.2\\periode_1\\programmeren\\FlappyBird\\pacman\\images\\pacman.jpg"));
            pacman.Fill = pacmanimage;

            ImageBrush redghost = new ImageBrush();
            redghost.ImageSource = new BitmapImage(new Uri("C:\\school\\jaar1.2\\periode_1\\programmeren\\FlappyBird\\pacman\\images\\red.jpg"));
            redguy.Fill = redghost;

            ImageBrush orangeghost = new ImageBrush();
            orangeghost.ImageSource = new BitmapImage(new Uri("C:\\school\\jaar1.2\\periode_1\\programmeren\\FlappyBird\\pacman\\images\\orange.jpg"));
            orangeguy.Fill = orangeghost;

            ImageBrush pinkghost = new ImageBrush();
            pinkghost.ImageSource = new BitmapImage(new Uri("C:\\school\\jaar1.2\\periode_1\\programmeren\\FlappyBird\\pacman\\images\\pink.jpg"));
            pinkguy.Fill = pinkghost;
        }

        private void Gameloop(object? sender, EventArgs e)
        {
            
        }

        private void GameOver()
        { 
        
        }
    }
}
