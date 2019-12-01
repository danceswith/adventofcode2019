using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Days.Day1
{
    class Day1
    {
        public void ExecuteDay1Part1()
        {
            Console.WriteLine("Part 1");
            var results = new List<int>();

            using (var streamReader = new StreamReader(@"C:\Users\Alexander\Documents\adventofcode2019\AdventOfCode2019\AdventOfCode2019\Days\Day1\day1input.txt"))
            {
                while(!streamReader.EndOfStream)
                {
                    var input = decimal.Parse(streamReader.ReadLine());

                    results.Add((int)CalculateFuel(input));

                    //Console.WriteLine(input);
                }
            }

            Console.WriteLine(results.Sum());
        }

        public void ExecuteDay1Part2()
        {
            Console.WriteLine("Part 2");

            var results = new List<int>();

            using (var streamReader = new StreamReader(@"C:\Users\Alexander\Documents\adventofcode2019\AdventOfCode2019\AdventOfCode2019\Days\Day1\day1input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var fuelResults = new List<int>();
                    var input = decimal.Parse(streamReader.ReadLine());

                    fuelResults.Add((int)CalculateFuel(input));

                    var hasFuelRemaining = true;
                    var currentFuelValue = input;

                    while (hasFuelRemaining)
                    {
                        currentFuelValue = CalculateFuel(currentFuelValue);

                        if (currentFuelValue > 0)
                        {
                            fuelResults.Add((int)currentFuelValue);

                            continue;
                        }

                        hasFuelRemaining = false;
                    }

                    var totalFuel = fuelResults.Sum();

                    results.Add(totalFuel);

                    //Console.WriteLine(totalFuel);
                }
            }

            Console.WriteLine(results.Sum());
        }

        private decimal CalculateFuel(decimal fuel)
        {
            var result = Math.Floor(fuel / 3);

            result -= 2;

            return result;
        }
    }
}
