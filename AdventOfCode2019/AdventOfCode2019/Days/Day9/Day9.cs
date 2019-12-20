using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019.Days.Day9
{
    class Day9
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day9\day9input.txt"))
            {
                var test = new Common.IntComputer(streamReader.ReadLine());

                test.inputSignals.Enqueue("1");

                test.ExecuteIntComputer();
            }
        }

        public void Part2()
        {
            Console.WriteLine("Part 2");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day9\day9input.txt"))
            {
                var test = new Common.IntComputer(streamReader.ReadLine());

                test.inputSignals.Enqueue("2");

                test.ExecuteIntComputer();
            }
        }
    }
}
