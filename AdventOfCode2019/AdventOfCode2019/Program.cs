using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            while (!Execute())
            {

            }
        }

        private static bool Execute()
        {
            Console.WriteLine("Enter which day to execute ...");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out var intInput))
            {
                Console.WriteLine("Enter a number");
                return false;
            }

            switch (intInput)
            {
                case 1:
                    var day1 = new Days.Day1.Day1();

                    day1.ExecuteDay1Part1();
                    day1.ExecuteDay1Part2();
                    break;

                case 2:
                    var day2 = new Days.Day2.Day2();

                    day2.Part1();
                    day2.Part2();

                    break;

                case 3:

                    var day3 = new Days.Day3.Day3();

                    day3.Part1();
                    day3.Part2();

                    break;
            }

            return true;
        }
    }
}
