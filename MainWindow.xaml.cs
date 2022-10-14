using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FlappyBird
{
	public partial class MainWindow : Window
	{
		private double gravity;
		private int score;
		private bool gameOver;

		private bool spaceIsHeld;

		private readonly DispatcherTimer timer = new();
		private readonly Random random = new();

		public MainWindow()
		{
			InitializeComponent();

			timer.Tick += GameEngine;
			timer.Interval = TimeSpan.FromMilliseconds(20);

			StartGame();
		}

		private void KeyIsDown(object? sender, KeyEventArgs e)
		{
			if(e.Key == Key.Space)
			{
				if(!spaceIsHeld)
				{
					spaceIsHeld = true;
					gravity = -4;
				}
			}

			if(e.Key == Key.R && gameOver)
			{
				StartGame();
			}
		}

		private void KeyIsUp(object? sender, KeyEventArgs e)
		{
			if(spaceIsHeld)
			{
				spaceIsHeld = false;
			}
		}
		
		private void StartGame()
		{
			score = 0;
			gravity = -4;
			gameOver = false;

			Canvas.SetTop(flappyBird, 114);

			var obstacles = mainCanvas.Children.OfType<Canvas>().Where(o => o.Name.ToLower().StartsWith("obstacle")).ToArray();

			for(int i = 0; i < obstacles.Length; i++)
			{
				var obstacle = obstacles[i];
				var indexPlusOne = i + 1;

				Canvas.SetTop(obstacle, -random.Next(141));
				if(obstacle.Name == "obstacle" + indexPlusOne)
				{
					Canvas.SetLeft(obstacle, 100 * indexPlusOne);
				}
			}

			var clouds = mainCanvas.Children.OfType<Image>().Where(o => o.Name.ToLower().StartsWith("cloud")).ToArray();

			for(int i = 0; i < clouds.Length; i++)
			{
				var cloud = clouds[i];
				var indexPlusOne = i + 1;

				if(cloud.Name == "cloud" + indexPlusOne)
				{
					Canvas.SetLeft(cloud, random.Next(50, 223));
				}
			}

			timer.Start();
		}

		private void StopGame()
		{
			timer.Stop();
			gameOver = true;
			scoreLabel.Content += "   Game over, press R to try again.";
		}

		private void GameEngine(object? sender, EventArgs e)
		{
			if(gravity >= 8)
			{
				gravity = 8;
			}
			else
			{
				gravity += 0.4;
			}

			flappyBird.RenderTransform = new RotateTransform(gravity * 15, flappyBird.Width * 0.5, flappyBird.Height * 0.5);

			scoreLabel.Content = "Score: " + score;
			Rect flappyRect = new(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width, flappyBird.Height);
			Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);

			if(Canvas.GetTop(flappyBird) <= -flappyBird.Height || Canvas.GetTop(flappyBird) >= mainCanvas.Height)
			{
				StopGame();
			}

			foreach(var pipes in mainCanvas.Children.OfType<Canvas>().Where(o => o.Name.ToLower().StartsWith("obstacle")))
			{
				foreach(var pipe in pipes.Children.OfType<Image>())
				{
					Rect pillars = new(Canvas.GetLeft(pipes), Canvas.GetTop(pipes) + Canvas.GetTop(pipe), 26, 160);

					if(flappyRect.IntersectsWith(pillars))
					{
						StopGame();
					}
				}

				Canvas.SetLeft(pipes, Canvas.GetLeft(pipes) - 2);

				if((string)pipes.Tag == "up")
				{
					Canvas.SetTop(pipes, Canvas.GetTop(pipes) - 1);
					if(Canvas.GetTop(pipes) <= -140)
					{
						pipes.Tag = "down";
					}
				}
				else if((string)pipes.Tag == "down")
				{
					Canvas.SetTop(pipes, Canvas.GetTop(pipes) + 1);
					if(Canvas.GetTop(pipes) >= 0)
					{
						pipes.Tag = "up";
					}
				}

				if(Canvas.GetLeft(pipes) < -26)
				{
					Canvas.SetLeft(pipes, Canvas.GetLeft(pipes) + 400);
					Canvas.SetTop(pipes, -random.Next(141));
					score += 1;
				}
			}

			foreach(var image in mainCanvas.Children.OfType<Image>().Where(o => o.Name.ToLower().StartsWith("cloud")))
			{
				Canvas.SetLeft(image, Canvas.GetLeft(image) - 1);

				if(Canvas.GetLeft(image) < -image.Width)
				{
					Canvas.SetLeft(image, random.Next(320, 421));
				}
			}
		}
	}
}
