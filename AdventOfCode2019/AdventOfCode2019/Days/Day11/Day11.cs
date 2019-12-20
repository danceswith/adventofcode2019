using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day11
{
    class Day11
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day11\day11input.txt"))
            {
                var test = streamReader.ReadLine();

                var robot = new PaintingRobot(test);

                robot.ExecuteRobot();

                Console.WriteLine(robot.CountOfPanelsPaintedOnce);

                robot.DrawTheHull();
            }
        }
    }

    public class PaintingRobot
    {
        public Common.IntComputer IntComputer;
        public Point Location;
        public Direction Facing;
        public Dictionary<Point, int> Map;
        public RobotMode CurrentMode;
        public int CountOfPanelsPaintedOnce;

        public PaintingRobot(string intComputerProgram)
        {
            this.IntComputer = new Common.IntComputer(intComputerProgram);
            this.Map = new Dictionary<Point, int>();
        }

        public enum Direction { Up, Down, Left, Right}
        public enum RobotMode { Painting, Moving }

        public void ExecuteRobot()
        {
            Location = new Point(0, 0);

            this.CurrentMode = RobotMode.Painting;

            //this.IntComputer.inputSignals.Enqueue("0"); // over black at start

            this.IntComputer.inputSignals.Enqueue("1");

            this.IntComputer.ExecuteIntComputer();

            while (this.IntComputer.executing)
            {
                if (this.IntComputer.outputSignals.Any())
                {
                    if (this.CurrentMode == RobotMode.Painting)
                    {
                        var output = this.IntComputer.outputSignals.Dequeue();

                        if (Map.TryGetValue(Location, out var colour))
                        {
                            Map[Location] = int.Parse(output) == 0 ? 0 : 1;
                        }
                        else
                        {
                            var paintedColour = int.Parse(output) == 0 ? 0 : 1;

                            Map.Add(Location, paintedColour);
                            
                            this.CountOfPanelsPaintedOnce++;
                        }

                        this.CurrentMode = RobotMode.Moving;
                    }

                    if (this.CurrentMode == RobotMode.Moving)
                    {
                        var output = this.IntComputer.outputSignals.Dequeue();

                        TurnRobot(int.Parse(output));

                        if (Map.TryGetValue(Location, out var colour))
                        {
                            this.IntComputer.inputSignals.Enqueue(colour.ToString());
                        }
                        else
                        {
                            Map.Add(Location, 0);
                            this.CountOfPanelsPaintedOnce++;
                            this.IntComputer.inputSignals.Enqueue("0");
                        }

                        this.CurrentMode = RobotMode.Painting;
                    }
                }

                this.IntComputer.ExecuteIntComputer();
            }

        }

        private void TurnRobot(int turnMode)
        {
            if (turnMode == 0)
            {
                switch(this.Facing)
                {
                    case Direction.Up:
                        this.Facing = Direction.Left;
                        break;
                    case Direction.Left:
                        this.Facing = Direction.Down;
                        break;
                    case Direction.Down:
                        this.Facing = Direction.Right;
                        break;
                    case Direction.Right:
                        this.Facing = Direction.Up;
                        break;
                }
            }
            else if (turnMode == 1)
            {
                switch (this.Facing)
                {
                    case Direction.Up:
                        this.Facing = Direction.Right;
                        break;
                    case Direction.Right:
                        this.Facing = Direction.Down;
                        break;
                    case Direction.Down:
                        this.Facing = Direction.Left;
                        break;
                    case Direction.Left:
                        this.Facing = Direction.Up;
                        break;
                }
            }

            switch(this.Facing)
            {
                case Direction.Up:
                    Location = new Point(Location.X, Location.Y + 1);
                    break;
                case Direction.Down:
                    Location = new Point(Location.X, Location.Y - 1);
                    break;
                case Direction.Left:
                    Location = new Point(Location.X - 1, Location.Y);
                    break;
                case Direction.Right:
                    Location = new Point(Location.X + 1, Location.Y);
                    break;
            }
        }

        public void DrawTheHull()
        {
            var minX = Map.Keys.Select(x => x.X).Min();
            var maxX = Map.Keys.Select(x => x.X).Max();

            var maxY = Map.Keys.Select(y => y.Y).Max();
            var minY = Map.Keys.Select(y => y.Y).Min();

            for (var x = minX; x <= maxX; x++)
            {
                StringBuilder sb = new StringBuilder();

                for (var y = minY; y <= maxY; y++)
                {
                    if (Map.TryGetValue(new Point(x, y), out var output))
                    {
                        var value = output == 0 ? "." : "#";
                        sb.Append(value);
                    }
                    else
                    {
                        sb.Append("#");
                    }
                }

                Console.WriteLine(sb.ToString());
            }
        }
    }

}
