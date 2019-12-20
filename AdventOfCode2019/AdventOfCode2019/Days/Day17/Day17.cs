using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day17
{
    class Day17
    {
        public void Part1()
        {

            var intComputerProgram = string.Empty;
            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day17\day17input.txt"))
            {
                intComputerProgram = streamReader.ReadToEnd();
            }

            var intComputer = new Common.IntComputer(intComputerProgram);

            var stringBuilder = new StringBuilder();

            intComputer.ExecuteIntComputer();

            var map = new Dictionary<Point, char>();

            var x = 0;
            var y = 0;

            while (intComputer.outputSignals.Any())
            {
                var output = int.Parse(intComputer.outputSignals.Dequeue());

                var outputChar = Convert.ToChar(output);

                stringBuilder.Append(outputChar);

                var point = new Point(x, y);

                map.Add(point, outputChar);

                if (output == 10)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }
            }

            Console.WriteLine(stringBuilder.ToString());

            var listOfAlignments = new List<int>();

            foreach (var mapPoint in map)
            {
                if (mapPoint.Value == '#')
                {
                    var point = mapPoint.Key;

                    var left = new Point(point.X - 1, point.Y);
                    var right = new Point(point.X + 1, point.Y);
                    var up = new Point(point.X, point.Y - 1);
                    var down = new Point(point.X, point.Y + 1);

                    if (left.X < 0)
                    {
                        continue;
                    }

                    if (up.Y < 0)
                    {
                        continue;
                    }

                    if (map[left] == '#'
                        && map[right] == '#'
                        && map[up] == '#'
                        && map[down] == '#')
                    {
                        listOfAlignments.Add(point.X * point.Y);
                    }
                }
            }

            Console.WriteLine(listOfAlignments.Sum());   
        }

        public void Part2()
        {

            var intComputerProgram = string.Empty;
            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day17\day17input.txt"))
            {
                intComputerProgram = streamReader.ReadToEnd();
            }

            var intComputer = new Common.IntComputer(intComputerProgram);

            var stringBuilder = new StringBuilder();

            intComputer.ExecuteIntComputer();

            var map = new Dictionary<Point, char>();

            var x = 0;
            var y = 0;

            while (intComputer.outputSignals.Any())
            {
                var output = int.Parse(intComputer.outputSignals.Dequeue());

                var outputChar = Convert.ToChar(output);

                stringBuilder.Append(outputChar);

                var point = new Point(x, y);

                map.Add(point, outputChar);

                if (output == 10)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }
            }

            Console.WriteLine(stringBuilder.ToString());

            var listOfAlignments = new List<int>();

            foreach (var mapPoint in map)
            {
                if (mapPoint.Value == '#')
                {
                    var point = mapPoint.Key;

                    var left = new Point(point.X - 1, point.Y);
                    var right = new Point(point.X + 1, point.Y);
                    var up = new Point(point.X, point.Y - 1);
                    var down = new Point(point.X, point.Y + 1);

                    if (left.X < 0)
                    {
                        continue;
                    }

                    if (up.Y < 0)
                    {
                        continue;
                    }

                    if (map[left] == '#'
                        && map[right] == '#'
                        && map[up] == '#'
                        && map[down] == '#')
                    {
                        listOfAlignments.Add(point.X * point.Y);
                    }
                }
            }

            Console.WriteLine(listOfAlignments.Sum());

            var scafold = new Dictionary<Point, char>();
            var start = new Point();

            foreach (var mapPoint in map)
            {
                if (mapPoint.Value == '^')
                {
                    start = mapPoint.Key;
                }

                if (mapPoint.Value == '#')
                {
                    scafold.Add(mapPoint.Key, mapPoint.Value);
                }
            }

            var scafoldToCheck = new Dictionary<Point, char>();

            //ran through this manually rob, shoot me. plus its midnight so mercy plz

            //L,8,R,10,L,10,R,10,L,8,L,8,L,10,L,8,R,10,L,10,L,4,L,6,L,8,L,8,R,10,L,8,L,8,L,10,L,4,L,6,L,8,L,8,L,8,R,10,L,10,L,4,L,6,L,8,L,8,R,10,L,8,L,8,L,10,L,4,L,6,L,8,L,8


            //L,4,L,6,L,8,L,8               A
            //R,10,L,8,L,8,L,10             B
            //L,8,R,10,L,10                 C

            var groupA = "L,4,L,6,L,8,L,8\n";
            var groupB = "R,10,L,8,L,8,L,10\n";
            var groupC = "L,8,R,10,L,10\n";
            var videoFeed = "n\n";

            var mainSequence = "C,B,C,A,B,A,C,A,B,A\n";

            var intComputer2 = new Common.IntComputer(intComputerProgram);

            var mainSequenceList = new List<string>();
            foreach(var character in mainSequence)
            {
                mainSequenceList.Add(((int)character).ToString());
            }

            var groupAList = new List<string>();
            foreach (var character in groupA)
            {
                groupAList.Add(((int)character).ToString());
            }

            var groupBList = new List<string>();
            foreach (var character in groupB)
            {
                groupBList.Add(((int)character).ToString());
            }

            var groupCList = new List<string>();
            foreach (var character in groupC)
            {
                groupCList.Add(((int)character).ToString());
            }

            var videoList = new List<string>();
            foreach (var character in videoFeed)
            {
                videoList.Add(((int)character).ToString());
            }

            var listOfInputs = new List<string>();

            listOfInputs.AddRange(mainSequenceList);
            listOfInputs.AddRange(groupAList);
            listOfInputs.AddRange(groupBList);
            listOfInputs.AddRange(groupCList);
            listOfInputs.AddRange(videoList);

            intComputer2.memory[0] = 2;

            foreach(var input in listOfInputs)
            {
                intComputer2.inputSignals.Enqueue(input);
            }
           
            intComputer2.ExecuteIntComputer();

            var lastOutput = intComputer2.outputSignals.Last();

            Console.WriteLine(lastOutput);
        }
    }
}
