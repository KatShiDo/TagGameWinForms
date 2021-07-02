using System;
using System.Collections.Generic;
using System.Timers;
using TagGameLib;
using Timer = System.Timers.Timer;

namespace TagGameWinForms
{
    internal static class Program
    {
        private static Dictionary<ConsoleKey, MoveDirection> keys = new Dictionary<ConsoleKey, MoveDirection>
        {
            [ConsoleKey.LeftArrow] = MoveDirection.Left,
            [ConsoleKey.RightArrow] = MoveDirection.Right,
            [ConsoleKey.UpArrow] = MoveDirection.Up,
            [ConsoleKey.DownArrow] = MoveDirection.Down,
        };
        
        private static Timer _timer;
        private static bool _flag = true;
        private static ModelGame _model;
        private static readonly DateTime Start = DateTime.Now;

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Clear();
            _timer = new Timer {Interval = 100};
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();

            _model = new ModelGame();
            _model.RePaint += Print;
            _model.Init();
            
            do
            {
                var key = Console.ReadKey(true).Key;
                _model.KeyDown(keys[key]);
            } while (!_model.Win());
            Console.WriteLine("You win!!!");
        }
        
        private static void Print(object sender, int[,] map)
        {
            _flag = false;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Step: {_model.Step}");
            Console.WriteLine();
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    Console.Write(map[i, j] == 0 ? "   " : $"{map[i, j],3}");
                }
                Console.WriteLine();
            }

            _flag = true;
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_flag) return;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{(DateTime.Now - Start).TotalSeconds:F1} sec...");
        }
    }
}