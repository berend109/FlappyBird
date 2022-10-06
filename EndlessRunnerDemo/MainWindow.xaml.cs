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

using System.Windows.Threading;

namespace EndlessRunnerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitbox;
        Rect groundHitbox;
        Rect obstacleHitbox;

        bool isJumping;

        int force = 20;
        int speed = 5;

        Random rnd = new Random();

        bool gameOver;

        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int[] obstaclePosition = { 320, 320, 300, 305, 315 };

        int score = 0;

        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/megamanBackground.gif"));

            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            StartGame();
            
        }

        private void GameEngine(object sender, EventArgs e)
        {

            Canvas.SetLeft(background, Canvas.GetLeft(background) - 5);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 5);

            if (Canvas.GetLeft(background) < -1262)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }

            if (Canvas.GetLeft(background2) < -1262)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }

            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);

            scoreText.Content = "Score: " + score;

            playerHitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 30, player.Height);
            obstacleHitbox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            groundHitbox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            if (playerHitbox.IntersectsWith(groundHitbox))
            {
                speed = 0;

                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);

                isJumping = false;

                spriteIndex += .25;

                if (spriteIndex > 4)
                {
                    spriteIndex = 1;
                }

                RunSprite(spriteIndex);
            }

            //jumping
            if(isJumping == true) 
            {
                speed = -9;

                force -= 1;
            }
            else
            {
                speed = 12;
            }

            if(force < 0)
            {
                isJumping = false;
            }


            //respawn obstacle and increase score
            if(Canvas.GetLeft(obstacle) < -50)
            {
                Canvas.SetLeft(obstacle, 950);

                Canvas.SetTop(obstacle, obstaclePosition[rnd.Next(0, obstaclePosition.Length)]);

                score += 1;
            }


            //player death
            if (playerHitbox.IntersectsWith(obstacleHitbox))
            {
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/megamanHurt.gif"));

                gameOver = true;
                gameTimer.Stop();
            }

            if(gameOver == true)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;

                scoreText.Content = "Score: " + score + " Press Enter to play again.";
            }
            else
            {
                obstacle.StrokeThickness = 0;
                player.StrokeThickness = 0;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }

            if (e.Key == Key.Space && isJumping == false && Canvas.GetTop(player) > 250)
            {
                isJumping = true;
                force = 15;
                speed = -12;

                //jump sprite
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/megamanJump.gif"));
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            
        }

        private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);
            

            //default player location
            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 240);


            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/megamanObstacle.png"));
            obstacle.Fill = obstacleSprite;

            isJumping = false;
            gameOver = false;
            score = 0;

            scoreText.Content = "Score: " + score;

            gameTimer.Start();
        }


        //running sprite animation
        private void RunSprite(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/megamanRun_01.gif"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/megamanRun_02.gif"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/megamanRun_03.gif"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/megamanRun_04.gif"));
                    break;
            }

            player.Fill = playerSprite;
        }
    }
}
