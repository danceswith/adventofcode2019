using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day19
{
    class Day19
    {
        public void Part1()
        {

            var intComputerProgram = string.Empty;
            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day19\day19inputtest.txt"))
            {
                intComputerProgram = streamReader.ReadToEnd();
            }
            
            var beam = 0;

            for(var x = 0; x < 50; x++)
            {
                for(var y = 0; y < 50; y++)
                {
                    var intComputer = new Common.IntComputer(intComputerProgram);

                    intComputer.inputSignals.Enqueue(x.ToString());
                    intComputer.inputSignals.Enqueue(y.ToString());

                    intComputer.ExecuteIntComputer();

                    if (intComputer.outputSignals.Any())
                    {
                        var output = intComputer.outputSignals.Dequeue();

                        beam += int.Parse(output);
                    }
                }
            }

            Console.WriteLine(beam);
        }

        public void Part2()
        {

            var intComputerProgram = string.Empty;
            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day19\day19inputtest.txt"))
            {
                intComputerProgram = streamReader.ReadToEnd();
            }

            int leftOff = 0;

            for (var y = 0; y < 10000; y++)
            {
                for (var x = leftOff; x < 10000; x++)
                {
                    var intComputer = new Common.IntComputer(intComputerProgram);

                    intComputer.inputSignals.Enqueue(x.ToString());
                    intComputer.inputSignals.Enqueue(y.ToString());

                    intComputer.ExecuteIntComputer();

                    var output = intComputer.outputSignals.Dequeue();

                    if (output == "1")
                    {
                        leftOff = x;

                        var intComputer2 = new Common.IntComputer(intComputerProgram);

                        intComputer2.inputSignals.Enqueue((x + 99).ToString());
                        intComputer2.inputSignals.Enqueue((y - 99).ToString());

                        intComputer2.ExecuteIntComputer();

                        var output2 = intComputer2.outputSignals.Dequeue();

                        if (output2 == "1")
                        {
                            Console.WriteLine(x * 10000 + y - 99);
                        }
                        break;
                    }
                }
            }
        }
    }
}
