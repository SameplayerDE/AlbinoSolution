using System;

namespace AlbinoOffline
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new AlbinoOffline())
                game.Run();
        }
    }
}