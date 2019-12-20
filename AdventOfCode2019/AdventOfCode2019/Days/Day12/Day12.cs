using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day12
{
    class Day12
    {
        public void Part1()
        {
            var listOfMoons = new List<Moon>();

            var pairsOfMoons = new List<Tuple<Moon, Moon>>();

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day12\day12inputtest.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine().Replace("<", "").Replace(">", "");

                    var linePositions = line.Split(',');
                    var x = long.Parse(linePositions[0].Trim().Substring(2, linePositions[0].Trim().Length - 2).Trim());
                    var y = long.Parse(linePositions[1].Trim().Substring(2, linePositions[1].Trim().Length - 2).Trim());
                    var z = long.Parse(linePositions[2].Trim().Substring(2, linePositions[2].Trim().Length - 2).Trim());

                    var moon = new Moon(x, y, z);

                    listOfMoons.Add(moon);
                }
            }

            foreach (var moon in listOfMoons)
            {
                foreach (var sister in listOfMoons.Where(m => m != moon))
                {
                    moon.sisterMoons.Add(sister);
                }
            }

            for (var timeStep = 0; timeStep < 1000; timeStep++)
            {
                foreach (var moon in listOfMoons)
                {
                    moon.CalculateGravity();
                }

                foreach (var moon in listOfMoons)
                {
                    moon.CalculatePosition();
                }
            }

            foreach(var moon in listOfMoons)
            {
                moon.CalculateEnergys();
            }

            var total = listOfMoons.Sum(m => m.TotalEnergy);

            Console.WriteLine(total);
        }

        public void Part2()
        {
            var listOfMoons = new List<Moon>();

            var pairsOfMoons = new List<Tuple<Moon, Moon>>();

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day12\day12inputtest.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine().Replace("<", "").Replace(">", "");
                    var linePositions = line.Split(',');
                    var x = long.Parse(linePositions[0].Trim().Substring(2, linePositions[0].Trim().Length - 2).Trim());
                    var y = long.Parse(linePositions[1].Trim().Substring(2, linePositions[1].Trim().Length - 2).Trim());
                    var z = long.Parse(linePositions[2].Trim().Substring(2, linePositions[2].Trim().Length - 2).Trim());
                    var moon = new Moon(x, y, z);
                    listOfMoons.Add(moon);
                }
            }

            foreach (var moon in listOfMoons)
            {
                foreach(var sister in listOfMoons.Where(m => m != moon))
                {
                    moon.sisterMoons.Add(sister);
                }
            }

            var tuples = new HashSet<Tuple<long, long, long, long, long, long, long, Tuple<long>>>(); // list was so slow

            var xCounter = 0;

            while(true)
            {
                var xTuple = new Tuple<long, long, long, long, long, long, long, Tuple<long>>
                    (listOfMoons[0].XPosition, listOfMoons[1].XPosition, 
                    listOfMoons[2].XPosition, listOfMoons[3].XPosition, 
                    listOfMoons[0].XVelocity, listOfMoons[1].XVelocity, 
                    listOfMoons[2].XVelocity, new Tuple<long>(listOfMoons[3].XVelocity));
                
                if (!tuples.Contains(xTuple))
                {
                    tuples.Add(xTuple);

                    foreach (var moon in listOfMoons)
                    {
                        moon.CalculateGravity("x");
                    }

                    foreach (var moon in listOfMoons)
                    {
                        moon.CalculatePosition("x");
                    }

                    xCounter++;

                    continue;
                }

                break; 
            }

            var yCounter = 0;

            while (true)
            {
                var yTuple = new Tuple<long, long, long, long, long, long, long, Tuple<long>>
                    (listOfMoons[0].YPosition, listOfMoons[1].YPosition, listOfMoons[2].YPosition, 
                    listOfMoons[3].YPosition, listOfMoons[0].YVelocity, listOfMoons[1].YVelocity, 
                    listOfMoons[2].YVelocity, new Tuple<long>(listOfMoons[3].YVelocity));

                if (!tuples.Contains(yTuple))
                {
                    tuples.Add(yTuple);

                    foreach (var moon in listOfMoons)
                    {
                        moon.CalculateGravity("y");
                    }

                    foreach (var moon in listOfMoons)
                    {
                        moon.CalculatePosition("y");
                    }

                    yCounter++;

                    continue;
                }

                break;
            }

            var zCounter = 0;

            while (true)
            {
                var zTuple = new Tuple<long, long, long, long, long, long, long, Tuple<long>>
                    (listOfMoons[0].ZPosition, listOfMoons[1].ZPosition, listOfMoons[2].ZPosition,
                    listOfMoons[3].ZPosition, listOfMoons[0].ZVelocity, listOfMoons[1].ZVelocity,
                    listOfMoons[2].ZVelocity, new Tuple<long>(listOfMoons[3].ZVelocity));

                if (!tuples.Contains(zTuple))
                {
                    tuples.Add(zTuple);

                    foreach (var moon in listOfMoons)
                    {
                        moon.CalculateGravity("z");
                    }

                    foreach (var moon in listOfMoons)
                    {
                        moon.CalculatePosition("z");
                    }

                    zCounter++;

                    continue;
                }

                break;

            }

            var yzGcf = GreatestCommonFactor(yCounter, zCounter);

            var answer = GreatestCommonFactor(xCounter, yzGcf);

            Console.WriteLine(answer);
        }

        static long GreatestCommonFactor(long first, long second)
        {
            return (first * second) / GreatestCommonDenominator(first, second);
        }

        static long GreatestCommonDenominator(long first, long second)
        {
            while (first != second)
            {
                if (first < second)
                {
                    second -= first;
                }
                else
                {
                    first -= second;
                }
            }

            return (first);
        }

    }

    class Moon
    {
        public long XPosition { get; set; }
        public long YPosition { get; set; }
        public long ZPosition { get; set; }
        public long XVelocity { get; set; }
        public long YVelocity { get; set; }
        public long ZVelocity { get; set; }
        public long PotentialEngery { get; set; }
        public long KineticEnergy { get; set; }
        public long TotalEnergy { get; set; }
        public List<Moon> sisterMoons = new List<Moon>();

        public Moon(long x, long y, long z)
        {
            XPosition = x;
            YPosition = y;
            ZPosition = z;
        }

        public void CalculateGravity()
        {
            foreach(var sister in sisterMoons)
            {
                if (this.XPosition < sister.XPosition)
                {
                    this.XVelocity++;
                    //sister.XVelocity--;
                }
                else if (this.XPosition == sister.XPosition)
                {
                }
                else
                {
                    this.XVelocity--;
                    //sister.XVelocity++;
                }

                if (this.YPosition < sister.YPosition)
                {
                    this.YVelocity++;
                    //sister.YVelocity--;
                }
                else if (this.YPosition == sister.YPosition)
                {
                }
                else
                {
                    this.YVelocity--;
                    //sister.YVelocity++;
                }

                if (this.ZPosition < sister.ZPosition)
                {
                    this.ZVelocity++;
                    //sister.ZVelocity--;
                }

                else if (this.ZPosition == sister.ZPosition)

                {
                }
                else
                {
                    this.ZVelocity--;

                    //sister.ZVelocity++;
                }
            }
        }

        public void CalculateGravity(string axis)
        {
            foreach (var sister in sisterMoons)
            {
                switch(axis)
                {
                    case "x":

                        if (this.XPosition < sister.XPosition)
                        {
                            this.XVelocity++;
                            //sister.XVelocity--;
                        }
                        else if (this.XPosition == sister.XPosition)
                        {
                        }
                        else
                        {
                            this.XVelocity--;
                            //sister.XVelocity++;
                        }
                        break;
                    case "y":

                        if (this.YPosition < sister.YPosition)
                        {
                            this.YVelocity++;
                            //sister.YVelocity--;
                        }
                        else if (this.YPosition == sister.YPosition)
                        {
                        }
                        else
                        {
                            this.YVelocity--;
                            //sister.YVelocity++;
                        }
                        break;
                    case "z":

                        if (this.ZPosition < sister.ZPosition)
                        {
                            this.ZVelocity++;
                            //sister.ZVelocity--;
                        }

                        else if (this.ZPosition == sister.ZPosition)

                        {
                        }
                        else
                        {
                            this.ZVelocity--;

                            //sister.ZVelocity++;
                        }
                        break;
                }              
            }
        }



        public void CalculatePosition()
        {
            this.XPosition += this.XVelocity;
            this.YPosition += this.YVelocity;
            this.ZPosition += this.ZVelocity;
        }

        public void CalculatePosition(string axis)
        {
            switch(axis)
            {
                case "x":
                    this.XPosition += this.XVelocity;
                    break;
                case "y":
                    this.YPosition += this.YVelocity;
                    break;
                case "z":
                    this.ZPosition += this.ZVelocity;
                    break;
            }          
        }

        public void CalculateEnergys()
        {
            this.PotentialEngery = Math.Abs(XPosition) + Math.Abs(YPosition) + Math.Abs(ZPosition);
            this.KineticEnergy = Math.Abs(XVelocity) + Math.Abs(YVelocity) + Math.Abs(ZVelocity);

            this.TotalEnergy = PotentialEngery * KineticEnergy;
        }
    }
}