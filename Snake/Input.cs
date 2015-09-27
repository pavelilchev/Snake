﻿using System.Collections;
using System.Windows.Forms;

namespace Snake
{
    public class Input
    {
        private static Hashtable keyTable = new Hashtable();

        public static bool Keypressed(Keys key)
        {
            if (keyTable[key] == null)
            {
                return false;
            }

            return (bool)keyTable[key];
        }

        public static void ChangeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }
    }
}
