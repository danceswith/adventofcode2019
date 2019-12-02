using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019.Days.Day2
{
    public class Day2
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day2\day2input.txt"))
            {
                var line = streamReader.ReadLine();

                var lineSplit = line.Split(",");

                var opCodePosition = 0;

                while (opCodePosition < lineSplit.Length)
                {
                    var opCode = lineSplit[opCodePosition];

                    switch (opCode)
                    {
                        case "99":
                            Console.WriteLine(lineSplit[0]);
                            return;

                        case "1":

                            var firstPosition = int.Parse(lineSplit[opCodePosition + 1]);
                            var secondPosition = int.Parse(lineSplit[opCodePosition + 2]);
                            var saveLocation = int.Parse(lineSplit[opCodePosition + 3]);

                            lineSplit[saveLocation] = (int.Parse(lineSplit[firstPosition]) + int.Parse(lineSplit[secondPosition])).ToString();

                            break;

                        case "2":

                            var firstPosition2 = int.Parse(lineSplit[opCodePosition + 1]);
                            var secondPosition2 = int.Parse(lineSplit[opCodePosition + 2]);
                            var saveLocation2 = int.Parse(lineSplit[opCodePosition + 3]);

                            lineSplit[saveLocation2] = (int.Parse(lineSplit[firstPosition2]) * int.Parse(lineSplit[secondPosition2])).ToString();


                            break;
                    }

                    opCodePosition += 4;
                }

                
            }


        }

        public void Part2()
        {
            Console.WriteLine("Part 2");

            using (var streamReader = new StreamReader(@"C:\Users\Alexander\Documents\adventofcode2019\AdventOfCode2019\AdventOfCode2019\Days\Day2\day2input.txt"))
            {
                var line = streamReader.ReadLine();

                for (var i = 0; i <= 99; i++)
                {
                    for(var u = 0; u <= 99; u++)
                    {
                        var lineSplit = line.Split(",");

                        lineSplit[1] = i.ToString();
                        lineSplit[2] = u.ToString();

                        var opCodePosition = 0;

                        while (opCodePosition < lineSplit.Length)
                        {
                            var opCode = lineSplit[opCodePosition];

                            switch (opCode)
                            {
                                case "99":

                                    if (int.Parse(lineSplit[0]) == 19690720)
                                    {
                                        Console.WriteLine(lineSplit[0]);

                                        Console.WriteLine($"{100 * i + u}");
                                        return;
                                    }
                                    
                                    break;

                                case "1":

                                    var firstPosition = int.Parse(lineSplit[opCodePosition + 1]);
                                    var secondPosition = int.Parse(lineSplit[opCodePosition + 2]);
                                    var saveLocation = int.Parse(lineSplit[opCodePosition + 3]);

                                    lineSplit[saveLocation] = (int.Parse(lineSplit[firstPosition]) + int.Parse(lineSplit[secondPosition])).ToString();

                                    break;

                                case "2":

                                    var firstPosition2 = int.Parse(lineSplit[opCodePosition + 1]);
                                    var secondPosition2 = int.Parse(lineSplit[opCodePosition + 2]);
                                    var saveLocation2 = int.Parse(lineSplit[opCodePosition + 3]);

                                    lineSplit[saveLocation2] = (int.Parse(lineSplit[firstPosition2]) * int.Parse(lineSplit[secondPosition2])).ToString();


                                    break;
                            }

                            opCodePosition += 4;
                        }
                    }
                }

             


            }

        }
    }
}
