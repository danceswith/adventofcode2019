using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day3
{
    class Day3
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day3\day3input.txt"))
            {
                var list = new List<string>();
                var listMinManhats = new List<float>();

                while (!streamReader.EndOfStream)
                {
                    list.Add(streamReader.ReadLine());
                }

                foreach(var line in list)
                {

                }

                var wire = GetWire(list[0]);
                var wire2 = GetWire(list[1]);

                var intersecs = wire.Keys.Intersect(wire2.Keys);

                var manhats = new List<float>();

                foreach(var intersec in intersecs)
                {
                    var manhat = ManhattanDistance(intersec.Item1, intersec.Item2);

                    manhats.Add(manhat);
                }

                var minmanhat = manhats.Min();

                Console.WriteLine(minmanhat);
            }
        }

        private Dictionary<(int, int), int> GetWire(string value)
        {
            var x = 0;
            var y = 0;
            var count = 1; //0 ,0 counts as a step?????
            var wire = new Dictionary<(int, int), int>();

            foreach (var i in value.Split(','))
            {
                switch (i.Substring(0, 1))
                {
                    case "U":
                        for (int u = 0; u < int.Parse(i.Substring(1)); u++)
                        {
                            try
                            {
                                wire.Add((x, ++y), count++);
                            }
                            catch
                            {

                            }
                            
                        }
                        break;

                    case "D":
                        for (int d = 0; d < int.Parse(i.Substring(1)); d++)
                        {
                            try
                            {
                                wire.Add((x, --y), count++);
                            }
                            catch
                            {

                            }
                        }
                        break;

                    case "R":
                        for (int r = 0; r < int.Parse(i.Substring(1)); r++)
                        {
                            try
                            {
                                wire.Add((++x, y), count++);
                            }
                            catch
                            {

                            }
                        }
                        break;

                    case "L":
                        for (int l = 0; l < int.Parse(i.Substring(1)); l++)
                        {
                            try
                            {
                                wire.Add((--x, y), count++);
                            }
                            catch
                            {

                            }
                        }
                        break;

                }
            }

            return wire;
        }

        public void Part2()
        {
            Console.WriteLine("Part 2");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day3\day3input.txt"))
            {
                var list = new List<string>();
                var listMinManhats = new List<float>();

                while (!streamReader.EndOfStream)
                {
                    list.Add(streamReader.ReadLine());
                }

                var wire = GetWire(list[0]);
                var wire2 = GetWire(list[1]);

                var intersecs = wire.Keys.Intersect(wire2.Keys);

                var manhats = new List<float>();

                foreach (var intersec in intersecs)
                {
                    var manhat = ManhattanDistance(intersec.Item1, intersec.Item2);

                    manhats.Add(manhat);
                }

                var minmanhat = manhats.Min();

                var min = intersecs.Min(i => wire[i] + wire2[i]);

                //Console.WriteLine(minmanhat);

                Console.WriteLine(min);
            }
        }

        public static float ManhattanDistance(float x1, float y1)
        {
            return Math.Abs(x1) + Math.Abs(y1);
        }
    }
}