namespace Snake
{
    using System.Collections;
    using System.Windows.Forms;

    public static class Input
    {
        private static readonly Hashtable KeyTable = new Hashtable();

        public static bool Keypressed(Keys key)
        {
            if (KeyTable[key] == null)
            {
                return false;
            }

            return (bool)KeyTable[key];
        }

        public static void ChangeState(Keys key, bool state)
        {
            KeyTable[key] = state;
        }
    }
}
