using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();

            new Settings();

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            StartGame();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;
            new Settings();

            Snake.Clear();

            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);

            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
        }

        private void GenerateFood()
        {
            int maxXPosition = pbCanvas.Size.Width / Settings.Width;
            int maxYposition = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPosition);
            food.Y = random.Next(0, maxYposition);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Settings.GameOver == true)
            {
                if (Input.Keypressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.Keypressed(Keys.Right) && Settings.direction != Direction.Left)
                {
                    Settings.direction = Direction.Right;
                }
                if (Input.Keypressed(Keys.Left) && Settings.direction != Direction.Right)
                {
                    Settings.direction = Direction.Left;
                }
                if (Input.Keypressed(Keys.Up) && Settings.direction != Direction.Down)
                {
                    Settings.direction = Direction.Up;
                }
                if (Input.Keypressed(Keys.Down) && Settings.direction != Direction.Up)
                {
                    Settings.direction = Direction.Down;
                }

                MovePlayer();
            }

            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                Brush snakeColor;

                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i != 0)
                    {
                        snakeColor = Brushes.Black;
                    }
                    else
                    {
                        snakeColor = Brushes.Green;
                    }

                    canvas.FillEllipse(snakeColor, new Rectangle(Snake[i].X * Settings.Width,
                                                                 Snake[i].Y * Settings.Height,
                                                                 Settings.Width, Settings.Height));

                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * Settings.Width,
                                                                  food.Y * Settings.Height,
                                                                  Settings.Width,
                                                                  Settings.Height));
                }

            }
            else
            {
                string gameOver = "game Over \nYour score is" + Settings.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                    }

                    int maxXPosition = pbCanvas.Size.Width / Settings.Width;
                    int maxYposition = pbCanvas.Size.Height / Settings.Height;

                    if (Snake[i].X < 0
                     || Snake[i].Y < 0
                     || Snake[i].X >= maxXPosition
                     || Snake[i].Y >= maxYposition)
                    {
                        Die();
                    }

                    for (int j = 0; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X
                         && Snake[i].Y == Snake[j].Y
                         && i != j)
                        {
                            Die();
                        }
                    }

                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void Eat()
        {
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;

            Snake.Add(food);

            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
        }

        private void Die()
        {
            Settings.GameOver = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
    }
}
