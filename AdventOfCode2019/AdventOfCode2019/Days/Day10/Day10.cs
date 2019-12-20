using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day10
{
    class Day10
    {
        public List<Point> AsteroidField;

        public void Part1()
        {
            Console.WriteLine("Part 1");

            AsteroidField = new List<Point>();

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day10\day10input.txt"))
            {
                var input = streamReader.ReadToEnd();

                ParseAsteroids(input);

                var listOfTargetsPerAsteroid = new List<int>();

                foreach(var asteroidBase in AsteroidField)
                {
                    var numberOfAsteroidVisible = 0;

                    foreach(var asteroidToView in AsteroidField)
                    {
                        if (asteroidBase == asteroidToView)
                        {
                            continue; // You cant view yourself
                        }

                        var viewBlocked = false;

                        foreach(var asteroidInTheWay in AsteroidField)
                        {
                            if (asteroidBase == asteroidInTheWay)
                            {
                                continue; // Cant block yourself either
                            }

                            if (asteroidToView == asteroidInTheWay)
                            {
                                continue; // Cant block your own view
                            }

                        }

                        if (!viewBlocked)
                        {
                            numberOfAsteroidVisible++;
                        }
                    }

                    listOfTargetsPerAsteroid.Add(numberOfAsteroidVisible);
                }

                Console.WriteLine(listOfTargetsPerAsteroid.Max());
            }
        }

        private void ParseAsteroids(string input)
        {
            var inputSplit = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var y = 0;
            

            foreach (var line in inputSplit)
            {
                var x = 0;

                foreach (var character in line)
                {
                    if (character.Equals('#'))
                    {
                        var asteroid = new Point(x, y);
                        AsteroidField.Add(asteroid);
                    }

                    x++;
                }

                y++;
            }
        }

        public void Part2()
        {
            Console.WriteLine("Part 2");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day10\day10input.txt"))
            {

            }
        }
    }
}
