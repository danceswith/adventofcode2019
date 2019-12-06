using System;
using System.IO;

namespace AdventOfCode2019.Days.Day5
{
    class Day5
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day5\day5input.txt"))
            {
                var line = streamReader.ReadLine();

                var lineSplit = line.Split(",");

                var opCodePosition = 0;

                while (opCodePosition < lineSplit.Length)
                {
                    var opCode = lineSplit[opCodePosition];

                    var parameterModes = new int[3];

                    if (opCode.Length > 2)
                    {
                        opCode = opCode.PadLeft(5, '0');

                        var hi = opCode.Substring(0, 3).ToCharArray();

                        var count = 0;

                        foreach(var c in hi)
                        {
                            parameterModes[count] = int.Parse(c.ToString());

                            count++;
                        }

                        opCode = opCode.Substring(3, 2);                     
                    }

                    if (opCode.Length == 1)
                    {
                        opCode = opCode.PadLeft(2, '0');
                    }

                    switch (opCode)
                    {
                        case "99":
                            Console.WriteLine(lineSplit[0]);
                            return;

                        case "01":

                            OpCode1(ref lineSplit, opCodePosition, parameterModes);

                            opCodePosition += 4;
                            break;

                        case "02":

                            OpCode2(ref lineSplit, opCodePosition, parameterModes);

                            opCodePosition += 4;
                            break;

                        case "03":
                            Console.WriteLine("Enter your intput ... ");
                            var input = Console.ReadLine();

                            var saveLocation3 = int.Parse(lineSplit[opCodePosition + 1]);

                            lineSplit[saveLocation3] = input;

                            opCodePosition += 2;
                            break;

                        case "04":

                            if (parameterModes[2] == 0)
                            {
                                var saveLocation4 = int.Parse(lineSplit[opCodePosition + 1]);

                                Console.WriteLine(lineSplit[saveLocation4]);
                            }
                            else
                            {
                                var saveLocation4 = int.Parse(lineSplit[opCodePosition + 1]);

                                Console.WriteLine(saveLocation4);
                            }

                            opCodePosition += 2;
                            break;

                        case "05":

                            var firstValue = 0;

                            if (parameterModes[2] == 0)
                            {
                                var firstParameter = int.Parse(lineSplit[opCodePosition + 1]);

                                firstValue = int.Parse(lineSplit[firstParameter]);
                            }
                            else
                            {
                                firstValue = int.Parse(lineSplit[opCodePosition + 1]);
                            }

                            if (firstValue > 0)
                            {
                                if (parameterModes[1] == 0)
                                {
                                    var secondParameter = int.Parse(lineSplit[opCodePosition + 2]);

                                    opCodePosition = int.Parse(lineSplit[secondParameter]);
                                }
                                else
                                {
                                    opCodePosition = int.Parse(lineSplit[opCodePosition + 2]);
                                }
                            }
                            else
                            {
                                opCodePosition += 3;
                            }




                            break;

                        case "06":

                            var firstValue1 = 0;

                            if (parameterModes[2] == 0)
                            {
                                var firstParameter1 = int.Parse(lineSplit[opCodePosition + 1]);

                                firstValue1 = int.Parse(lineSplit[firstParameter1]);
                            }
                            else
                            {
                                firstValue1 = int.Parse(lineSplit[opCodePosition + 1]);
                            }


                            if (firstValue1 == 0)
                            {
                                if (parameterModes[1] == 0)
                                {
                                    var secondParameter1 = int.Parse(lineSplit[opCodePosition + 2]);

                                    opCodePosition = int.Parse(lineSplit[secondParameter1]);
                                }
                                else
                                {
                                    opCodePosition = int.Parse(lineSplit[opCodePosition + 2]);
                                }

                            }
                            else
                            {
                                opCodePosition += 3;
                            }


                            break;

                        case "07":

                            var firstValue2 = 0;

                            if (parameterModes[2] == 0)
                            {
                                var firstParameter2 = int.Parse(lineSplit[opCodePosition + 1]);

                                firstValue2 = int.Parse(lineSplit[firstParameter2]);
                            }
                            else
                            {
                                firstValue2 = int.Parse(lineSplit[opCodePosition + 1]);
                            }

                            var secondValue = 0;
                            if (parameterModes[1] == 0)
                            {
                                var secondParameter2 = int.Parse(lineSplit[opCodePosition + 2]);

                                secondValue = int.Parse(lineSplit[secondParameter2]);
                            }
                            else
                            {
                                secondValue = int.Parse(lineSplit[opCodePosition + 2]);
                            }
                            

                            var thirdParameter = int.Parse(lineSplit[opCodePosition + 3]);

                            if (firstValue2 < secondValue)
                            {
                                lineSplit[thirdParameter] = "1";
                            }
                            else
                            {
                                lineSplit[thirdParameter] = "0";
                            }

                            opCodePosition += 4;

                            break;

                        case "08":

                            var firstValue3 = 0;

                            if (parameterModes[2] == 0)
                            {
                                var firstParameter3 = int.Parse(lineSplit[opCodePosition + 1]);

                                firstValue3 = int.Parse(lineSplit[firstParameter3]);
                            }
                            else
                            {
                                firstValue3 = int.Parse(lineSplit[opCodePosition + 1]);
                            }

                            var secondValue3 = 0;

                            if (parameterModes[1] == 0)
                            {
                                var secondParameter3 = int.Parse(lineSplit[opCodePosition + 2]);
                                secondValue3 = int.Parse(lineSplit[secondParameter3]);
                            }
                            else
                            {
                                secondValue3 = int.Parse(lineSplit[opCodePosition + 2]);
                            }
                            

                            var thirdParameter2 = int.Parse(lineSplit[opCodePosition + 3]);

                            if (firstValue3 == secondValue3)
                            {
                                lineSplit[thirdParameter2] = "1";
                            }
                            else
                            {
                                lineSplit[thirdParameter2] = "0";
                            }


                            opCodePosition += 4;

                            break;
                    }

                    
                }


            }


        }

        private void OpCode1(ref string[] lineSplit, int opCodePosition, int[] parameterModes)
        {

            var firstValue = 0;

            if (parameterModes[2] == 0)
            {
                var firstPosition = int.Parse(lineSplit[opCodePosition + 1]);
                firstValue = int.Parse(lineSplit[firstPosition]);
            }
            else
            {
                firstValue = int.Parse(lineSplit[opCodePosition + 1]);
            }

            var secondValue = 0;
            if (parameterModes[1] == 0)
            {
                var secondPosition = int.Parse(lineSplit[opCodePosition + 2]);
                secondValue = int.Parse(lineSplit[secondPosition]);
            }
            else
            {
                secondValue = int.Parse(lineSplit[opCodePosition + 2]);
            }

            //var thirdValue = 0;
            //if (parameterModes[0] == 0)
            //{
            //    var thirdPosition = int.Parse(lineSplit[opCodePosition + 3]);
            //    thirdValue = int.Parse(lineSplit[thirdPosition]);
            //}
            //else
            //{
            //    thirdValue = int.Parse(lineSplit[opCodePosition + 3]);
            //}

            var saveLocation = int.Parse(lineSplit[opCodePosition + 3]);

            lineSplit[saveLocation] = (firstValue + secondValue).ToString();
        }


        private void OpCode2(ref string[] lineSplit, int opCodePosition, int[] parameterModes)
        {
            var firstValue = 0;

            if (parameterModes[2] == 0)
            {
                var firstPosition = int.Parse(lineSplit[opCodePosition + 1]);
                firstValue = int.Parse(lineSplit[firstPosition]);
            }
            else
            {
                firstValue = int.Parse(lineSplit[opCodePosition + 1]);
            }

            var secondValue = 0;
            if (parameterModes[1] == 0)
            {
                var secondPosition = int.Parse(lineSplit[opCodePosition + 2]);
                secondValue = int.Parse(lineSplit[secondPosition]);
            }
            else
            {
                secondValue = int.Parse(lineSplit[opCodePosition + 2]);
            }

            var saveLocation = int.Parse(lineSplit[opCodePosition + 3]);

            lineSplit[saveLocation] = (firstValue * secondValue).ToString();
        }

        public void Part2()
        {
            Console.WriteLine("Part 2");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day5\day5inputtest.txt"))
            {
                var line = streamReader.ReadLine();

                for (var i = 0; i <= 99; i++)
                {
                    for (var u = 0; u <= 99; u++)
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
