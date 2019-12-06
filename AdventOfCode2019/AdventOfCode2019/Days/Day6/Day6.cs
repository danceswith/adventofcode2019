using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day6
{
    class Day6
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            var list = new List<Tuple<string, string>>();

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day6\day6input.txt"))
            {
                while(!streamReader.EndOfStream)
                {
                    list.Add(ParseOrbit(streamReader.ReadLine()));
                }
            }

            var listOfLinks = new List<Tuple<string, string>>();

            var countOfLinks = 0;

            foreach(var orbit in list)
            {
                countOfLinks++;

                if (orbit.Item1 == "COM")
                {
                    continue;
                }

                var cont = true;
                var currentOrbit = orbit.Item1;

                while (cont)
                {
                    var indirectOrbit = list.Where(o => o.Item2 == currentOrbit).FirstOrDefault();

                    if (indirectOrbit != null)
                    {
                        countOfLinks++;

                        if (indirectOrbit.Item1 == "COM")
                        {
                            break;
                        }

                        currentOrbit = indirectOrbit.Item1;
                    }
                }

            }

            Console.WriteLine(countOfLinks);
        }

        private Tuple<string, string> ParseOrbit(string input)
        {
            var inputSplit = input.Split(')');

            return new Tuple<string, string>(inputSplit[0], inputSplit[1]);
        }

        public void Part2()
        {
            Console.WriteLine("Part 1");

            var list = new List<Tuple<string, string>>();

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day6\day6input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    list.Add(ParseOrbit(streamReader.ReadLine()));
                }
            }

            var you = list.FirstOrDefault(o => o.Item2 == "YOU");
            var san = list.FirstOrDefault(o => o.Item2 == "SAN");

            var cont = true;
            var currentOrbit = you.Item1;

            var youStepsToTake = new Dictionary<string, int>();

            var youCounter = 0;

            while (true)
            {
                var indirectOrbit = list.Where(o => o.Item2 == currentOrbit).FirstOrDefault();

                if (indirectOrbit != null)
                {
                    youCounter++;

                    youStepsToTake.Add(indirectOrbit.Item1, youCounter);

                    if (indirectOrbit.Item1 == "COM")
                    {
                        break;
                    }

                    currentOrbit = indirectOrbit.Item1;
                }
            }

            var sanStepsToTake = new Dictionary<string, int>();
            var sanCounter = 0;

            currentOrbit = san.Item1;

            while (true)
            {
                var indirectOrbit = list.Where(o => o.Item2 == currentOrbit).FirstOrDefault();

                if (indirectOrbit != null)
                {
                    sanCounter++;

                    sanStepsToTake.Add(indirectOrbit.Item1, sanCounter);

                    if (indirectOrbit.Item1 == "COM")
                    {
                        break;
                    }

                    currentOrbit = indirectOrbit.Item1;
                }
            }

            var results = new List<int>();

            foreach(var steps in youStepsToTake)
            {
                if (sanStepsToTake.TryGetValue(steps.Key, out var sanSteps))
                {
                    results.Add(steps.Value + sanSteps);
                }
            }

            Console.WriteLine(results.Min());
        }
    }
}
