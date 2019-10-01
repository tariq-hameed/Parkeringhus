using System;
using static System.Console;

namespace Parkeringhus
{
    class Program
    {
        static void Main(string[] args)
        {


            bool shouldNotExit = true;
            while (shouldNotExit)

            {

                Console.WriteLine("1. Register arrivals");
                Console.WriteLine("2. Register departure");
                Console.WriteLine("3. Show Parking registery");
                Console.WriteLine("4. Exit");

                ConsoleKeyInfo keypressed = ReadKey(true);
                Clear();

                switch (keypressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        shouldNotExit = false;
                        break;



                }


                Clear();

            }
        }
    }
}
