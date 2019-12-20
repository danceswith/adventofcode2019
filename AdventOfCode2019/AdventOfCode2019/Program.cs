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

                case 8:
                    

                    var day8 = new Days.Day8.Day8();

                    day8.Part1();
                    day8.Part2();

                    break;

                case 9:

                    var day9 = new Days.Day9.Day9();

                    day9.Part1();
                    day9.Part2();

                    break;

                case 10:

                    var day10 = new Days.Day10.Day10();

                    day10.Part1();
                    day10.Part2();

                    break;

                case 11:

                    var Day11 = new Days.Day11.Day11();

                    Day11.Part1();
                    break;

                case 12:

                    var day12 = new Days.Day12.Day12();

                    day12.Part1();
                    day12.Part2();
         
                    break;

                case 13:
                    var Day13 = new Days.Day13.Day13();

                    Day13.Part1();
                    Day13.Part2();
                    break;

                case 14:

                    var Day14 = new Days.Day14.Day14();

                    Day14.Part1();
                    Day14.Part2();
                    break;

                case 15:

                    break;

                case 17:

                    var day17 = new Days.Day17.Day17();

                    day17.Part1();
                    day17.Part2();

                    break;

                case 19:
                    var day19 = new Days.Day19.Day19();

                    day19.Part2();

                    break;
            }

            return true;
        }
    }
}
