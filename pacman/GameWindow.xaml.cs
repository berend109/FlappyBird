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
            if (e.Key == Key.Left && NoLeft == false)
            {
                GoRight = GoUp = GoDown = false;
                NoRight = NoUp = NoDown = false;

                GoLeft = true;

                pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Right && NoRight == false)
            {
                GoLeft = GoUp = GoDown = false;
                NoLeft = NoUp = NoDown = false;

                GoRight = true;

                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Up && NoUp == false)
            {
                GoRight = GoLeft = GoDown = false;
                NoRight = NoLeft = NoDown = false;

                GoUp = true;

                pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Down && NoDown == false)
            {
                GoRight = GoUp = GoLeft= false;
                NoRight = NoUp = NoLeft = false;

                GoDown = true;

                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
            }
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
            txtScore.Content = "score: " + Score;

            if (GoRight)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + speed);
            }
            if (GoLeft)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - speed);
            }
            if (GoUp)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
            }
            if (GoDown)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) + speed);
            }


            if (GoDown && Canvas.GetTop(pacman) + 80 > Application.Current.MainWindow.Height)
            {
                NoDown = true;
                GoDown = false;
            }
            if (GoUp && Canvas.GetTop(pacman) < 1)
            {
                NoUp = true;
                GoUp = false;
            }
            if (GoLeft && Canvas.GetLeft(pacman) - 10 < 1)
            {
                NoLeft = true;
                GoLeft = false;
            }
            if (GoRight && Canvas.GetLeft(pacman) + 70 > Application.Current.MainWindow.Width)
            {
                NoRight = true;
                GoRight = false;
            }

            pacmanhitBox = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height);

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                Rect hitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                if ((string)x.Tag == "wall")
                {
                    if (GoLeft == true && pacmanhitBox.IntersectsWith(hitbox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 10);
                        NoLeft = true;
                        GoLeft = false;
                    }
                    if (GoRight == true && pacmanhitBox.IntersectsWith(hitbox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 10);
                        NoRight = true;
                        GoRight = false;
                    }
                    if (GoDown == true && pacmanhitBox.IntersectsWith(hitbox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                        NoDown = true;
                        GoDown = false;
                    }
                    if (GoUp == true && pacmanhitBox.IntersectsWith(hitbox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) + 10);
                        NoUp= true;
                        GoUp = false;
                    }

                }
                if ((string)x.Tag == "coin")
                {
                    if (pacmanhitBox.IntersectsWith(hitbox) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        Score++;
                    }
                }

                if ((string) x.Tag == "ghost")
                {
                    if (pacmanhitBox.IntersectsWith(hitbox))
                    {
                        GameOver("ghost got you, click ok to play again");
                    }

                    if (x.Name.ToString() == "orangeguy")
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - GhostSpeed);
                    }
                    else
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + GhostSpeed);
                    }

                    CurrentghostStep--;

                    if (CurrentghostStep < 1)
                    {
                        CurrentghostStep = GhostMoveStep;
                        GhostSpeed = -GhostSpeed;
                    }

                }
            }

            //85 coins

            if (Score == 85)
            {
                GameOver("you win you collected all the coins");
            }

        }

        private void GameOver(string message)
        {
            gametimer.Stop();
            MessageBox.Show(message, "game over");

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
