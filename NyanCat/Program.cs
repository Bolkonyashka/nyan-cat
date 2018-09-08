using System;

namespace NyanCat
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var game = new Game1();
            game.Run();
        }
    }
}
