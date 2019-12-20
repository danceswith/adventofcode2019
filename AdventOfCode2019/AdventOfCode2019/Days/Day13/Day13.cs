using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode2019.Days.Day13
{
    class Day13
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day13\day13input.txt"))
            {
                var test = streamReader.ReadLine();

                var intComputer = new Common.IntComputer(test);

                intComputer.ExecuteIntComputer();

                var listOfTiles = new List<string>();

                if (!intComputer.executing)
                {
                    while (intComputer.outputSignals.Any())
                    {
                        listOfTiles.Add(intComputer.outputSignals.Dequeue());
                    }
                }

                var screen = new Dictionary<Point, string>();

                for (var i = 0; i < listOfTiles.Count; i = i + 3)
                {
                    var point = new Point(int.Parse(listOfTiles[i]), int.Parse(listOfTiles[i + 1]));

                    screen.Add(point, listOfTiles[i + 2]);
                }

                var blockCount = screen.Where(s => s.Value == "2").Count();

                Console.WriteLine(blockCount);
            }
        }

        public string Score;
        public int ball;
        public int pad;
        public int blocksLeft;

        public void Part2()
        {
            Console.WriteLine("Part 2");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day13\day13input.txt"))
            {
                var test = streamReader.ReadLine();

                var intComputer = new Common.IntComputer(test);

                intComputer.memory[0] = 2; // achtung, hack hack hack

                intComputer.ExecuteIntComputer();

                var listOfTiles = new List<string>();

                var screen = new Dictionary<Point, string>();

                var left = false;
                var rigth = false;

                while (intComputer.executing)
                {
                    if (intComputer.outputSignals.Any())
                    {
                        ProcessScreen(intComputer.outputSignals, screen);

                        if (ball > pad)
                        {
                            intComputer.inputSignals.Enqueue("1");
                            rigth = true;
                            left = false;
                        }
                        else if (ball < pad)
                        {
                            intComputer.inputSignals.Enqueue("-1");
                            left = true;
                            rigth = false;
                        }
                        else
                        {
                            intComputer.inputSignals.Enqueue("0");
                            //if (left)
                            //{
                            //    intComputer.inputSignals.Enqueue("1");
                            //}
                            //else if (rigth)
                            //{
                            //    intComputer.inputSignals.Enqueue("-1");
                            //}

                        }

                        DrawTheScreen(screen);

                        if (blocksLeft == 0)
                        {
                            Console.WriteLine("you win");
                            break;
                        }

                        intComputer.ExecuteIntComputer();
                    }
                }
            }
        }

        public void DrawTheScreen(Dictionary<Point, string> screen)
        {
            var minX = screen.Keys.Select(x => x.X).Min();
            var maxX = screen.Keys.Select(x => x.X).Max();

            var maxY = screen.Keys.Select(y => y.Y).Max();
            var minY = screen.Keys.Select(y => y.Y).Min();

            for (var y = minY; y <= maxY; y++)
            {
                StringBuilder sb = new StringBuilder();

                for (var x = minX; x <= maxX; x++)
                {
                    if (screen.TryGetValue(new Point(x, y), out var output))
                    {
                        sb.Append(output);
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }

                Console.WriteLine(sb.ToString());
            }
        }

        private void ProcessScreen(Queue<string> outputs, Dictionary<Point, string> screen)
        {
            while(outputs.Any())
            {
                var x = outputs.Dequeue();
                var y = outputs.Dequeue();

                var point = new Point(int.Parse(x), int.Parse(y));

                var tile = outputs.Dequeue();

                if (x == "-1" && y == "0")
                {
                    Score = tile;
                    continue;
                }

                switch (tile)
                {
                    case "0":
                        if (screen.TryGetValue(point, out var _))
                        {
                            screen[point] = " ";
                        }
                        else
                        {
                            screen.Add(point, " ");
                        }
                        break;
                    case "1":
                        if (screen.TryGetValue(point, out var _))
                        {
                            screen[point] = "X";
                        }
                        else
                        {
                            screen.Add(point, "X");
                        }

                        break;
                    case "2":
                        if (screen.TryGetValue(point, out var _))
                        {
                            screen[point] = "B";
                        }
                        else
                        {
                            screen.Add(point, "B");
                        }

                        blocksLeft++;

                        break;
                    case "3":
                        if (screen.TryGetValue(point, out var _))
                        {
                            screen[point] = "-";
                        }
                        else
                        {
                            screen.Add(point, "-");
                        }

                        pad = point.X;
                        break;
                    case "4":
                        if (screen.TryGetValue(point, out var _))
                        {
                            screen[point] = "O";
                        }
                        else
                        {
                            screen.Add(point, "O");
                        }
                        ball = point.X;
                        break;
                }
            }
        }
    }
}
