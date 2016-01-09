namespace Snake
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Settings
    {
        public Settings()
        {
            Width = 16;
            Height = 16;
            Speed = 16;
            Score = 0;
            Points = 100;
            GameOver = false;
            Direction = Direction.Down;
        }

        public static int Width { get; private set; }

        public static int Height { get; private set; }

        public static int Speed { get; private set; }

        public static int Score { get; set; }

        public static int Points { get; private set; }

        public static bool GameOver { get; set; }

        public static Direction Direction { get; set; }
    }
}
