namespace Snake
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Snake : Form
    {
        private readonly List<Circle> snake = new List<Circle>();
        private Circle food = new Circle();

        public Snake()
        {
            this.InitializeComponent();
            var settings = new Settings();

            this.gameTimer.Interval = 1000 / Settings.Speed;
            this.gameTimer.Tick += this.UpdateScreen;
            this.gameTimer.Start();

            this.StartGame();
        }

        private void StartGame()
        {
            var settings = new Settings();
            this.lblGameOver.Visible = false;
          
            this.snake.Clear();

            Circle head = new Circle(10, 5);
            this.snake.Add(head);

            this.lblScore.Text = Settings.Score.ToString();

            this.GenerateFood();
        }

        private void GenerateFood()
        {
            int maxXPosition = this.pbCanvas.Size.Width / Settings.Width;
            int maxYposition = this.pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            int x = random.Next(0, maxXPosition);
            int y = random.Next(0, maxYposition);
            this.food = new Circle(x, y);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Settings.GameOver)
            {
                if (Input.Keypressed(Keys.Enter))
                {
                    this.StartGame();
                }
            }
            else
            {
                if (Input.Keypressed(Keys.Right) && Settings.Direction != Direction.Left)
                {
                    Settings.Direction = Direction.Right;
                }
                if (Input.Keypressed(Keys.Left) && Settings.Direction != Direction.Right)
                {
                    Settings.Direction = Direction.Left;
                }
                if (Input.Keypressed(Keys.Up) && Settings.Direction != Direction.Down)
                {
                    Settings.Direction = Direction.Up;
                }
                if (Input.Keypressed(Keys.Down) && Settings.Direction != Direction.Up)
                {
                    Settings.Direction = Direction.Down;
                }

                this.MovePlayer();
            }

            this.pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                for (int i = 0; i < this.snake.Count; i++)
                {
                    var snakeColor = i != 0 ? Brushes.Black : Brushes.Green;

                    canvas.FillEllipse(snakeColor, new Rectangle(this.snake[i].X * Settings.Width,
                                                                 this.snake[i].Y * Settings.Height,
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
                this.lblGameOver.Text = gameOver;
                this.lblGameOver.Visible = true;
            }
        }

        private void MovePlayer()
        {
            for (int i = this.snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.Direction)
                    {
                        case Direction.Up:
                            this.snake[i].Y--;
                            break;
                        case Direction.Down:
                            this.snake[i].Y++;
                            break;
                        case Direction.Left:
                            this.snake[i].X--;
                            break;
                        case Direction.Right:
                            this.snake[i].X++;
                            break;
                    }

                    int maxXPosition = this.pbCanvas.Size.Width / Settings.Width;
                    int maxYposition = this.pbCanvas.Size.Height / Settings.Height;

                    if (this.snake[i].X < 0
                     || this.snake[i].Y < 0
                     || this.snake[i].X >= maxXPosition
                     || this.snake[i].Y >= maxYposition)
                    {
                        Die();
                    }

                    for (int j = 0; j < this.snake.Count; j++)
                    {
                        if (this.snake[i].X == this.snake[j].X
                         && this.snake[i].Y == this.snake[j].Y
                         && i != j)
                        {
                            Die();
                        }
                    }

                    if (this.snake[0].X == this.food.X && this.snake[0].Y == this.food.Y)
                    {
                        this.Eat();
                    }
                }
                else
                {
                    this.snake[i].X = this.snake[i - 1].X;
                    this.snake[i].Y = this.snake[i - 1].Y;
                }
            }
        }

        private void Eat()
        {
            int x = this.snake[this.snake.Count - 1].X;
            int y = this.snake[this.snake.Count - 1].Y;
            Circle eatedFood = new Circle(x, y);

            this.snake.Add(eatedFood);

            Settings.Score += Settings.Points;
            this.lblScore.Text = Settings.Score.ToString();

            this.GenerateFood();
        }

        private static void Die()
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
