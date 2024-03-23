using System.Numerics;

namespace Harjoitustyö
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            Game newGame = new Game();
            newGame.Run();
        }
        public static void ColorLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void ColorNoLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void MenuText(string text1, string text2, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(text1);
            Console.ForegroundColor = color;
            Console.Write(text2);
        }
        public static void WaitForSeconds(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
        public static void ClearLines(int lines)
        {
            Console.SetCursorPosition(0, Console.CursorTop - lines);
            for (int i = 1; i <= lines; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
            Console.SetCursorPosition(0, Console.CursorTop - lines);
        }
        public static void EscapeText()
        {
            int currentCursorleft = Console.CursorLeft;
            int currentCursortop = Console.CursorTop;
            Console.SetCursorPosition(70, 0);
            Program.ColorLine("[Escape] peruuta", ConsoleColor.DarkGray);
            Console.SetCursorPosition(currentCursorleft, currentCursortop);
        }
        public static void RemoveEscapeText()
        {
            int currentCursorleft = Console.CursorLeft;
            int currentCursortop = Console.CursorTop;
            Console.SetCursorPosition(70, 0);
            Program.ColorLine("                 ", ConsoleColor.DarkGray);
            Console.SetCursorPosition(currentCursorleft, currentCursortop);
        }
        public static void GameStart()
        {
            Console.WriteLine("Pelaatko uudestaan? (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                Console.Clear();
                Game newGame = new Game();
                newGame.Run();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
