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

                case 4:

                    var day4 = new Days.Day4.Day4();

                    day4.Part1();
                    day4.Part2();

                    break;

                case 5:

                    var day5 = new Days.Day5.Day5();

                    day5.Part1();
                    day5.Part2();

                    break;

                case 6:

                    var day6 = new Days.Day6.Day6();

                    day6.Part1();

                    day6.Part2();

                    break;

                case 7:

                    var day7 = new Days.Day7.Day7();

                    day7.Part1();
                    day7.Part2();

                    break;
            }

            return true;
        }
    }
}
