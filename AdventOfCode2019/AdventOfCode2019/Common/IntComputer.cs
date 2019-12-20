using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Common
{
    public class IntComputer
    {
        public int PhaseSetting = 0;
        public string IntComputerProgram = string.Empty;
        public Queue<string> inputSignals = new Queue<string>();
        public Queue<string> outputSignals = new Queue<string>();
        public string LastOutput = string.Empty;
        public string[] lineSplit;
        public long[] memory;
        public bool executing = false;
        public bool awaitingInput = false;
        public long opCodePosition = 0;
        public bool phaseSettingSet = false;
        public long relativeBase = 0;

        public IntComputer Copy()
        {
            var newIntComputer = new IntComputer();
            newIntComputer.inputSignals = this.inputSignals;
            newIntComputer.outputSignals = this.outputSignals;
            newIntComputer.LastOutput = this.LastOutput;
            newIntComputer.lineSplit = this.lineSplit;
            newIntComputer.memory = this.memory;
            newIntComputer.executing = this.executing;
            newIntComputer.awaitingInput = this.awaitingInput;
            newIntComputer.opCodePosition = this.opCodePosition;
            newIntComputer.phaseSettingSet = this.phaseSettingSet;
            newIntComputer.relativeBase = this.relativeBase;

            return newIntComputer;
        }

        public IntComputer(int phaseSetting, string intComputerProgram)
        {
            this.PhaseSetting = phaseSetting;
            this.inputSignals.Enqueue(phaseSetting.ToString());

            this.IntComputerProgram = intComputerProgram;

            lineSplit = new string[999999];

            lineSplit = intComputerProgram.Split(",");

            ParseIntoMemory();

            executing = true;
        }

        public IntComputer(string intComputerProgram)
        {
            this.IntComputerProgram = intComputerProgram;

            lineSplit = new string[999999];

            lineSplit = intComputerProgram.Split(",");

            ParseIntoMemory();

            executing = true;
        }

        public IntComputer()
        {

        }

        private void ParseIntoMemory()
        {
            this.memory = new long[999999999];

            var counter = 0;

            foreach(var str in lineSplit)
            {
                memory[counter] = long.Parse(str);

                counter++;
            }
        }

        public void ExecuteIntComputer()
        {
            while (executing)
            {
                var opCode = memory[opCodePosition].ToString();

                var parameterModes = new int[3];

                opCode = opCode.PadLeft(5, '0');

                if (opCode.Length > 2)
                {
                    opCode = opCode.PadLeft(5, '0');

                    var hi = opCode.Substring(0, 3).ToCharArray();

                    var count = 0;

                    foreach (var c in hi)
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

                        executing = false;

                        return;
                        break;

                    case "01":

                        OpCode1(ref memory, opCodePosition, parameterModes);

                        opCodePosition += 4;
                        break;

                    case "02":

                        OpCode2(ref memory, opCodePosition, parameterModes);

                        opCodePosition += 4;
                        break;

                    case "03":
                        //Console.WriteLine("Enter your intput ... ");

                        if (inputSignals.Any())
                        {
                            var input = int.Parse(inputSignals.Dequeue());

                            long saveLocation3 = 0;

                            if (parameterModes[2] == 2)
                            {
                                var saveValue3 = memory[(opCodePosition + 1)];
                                saveLocation3 = relativeBase + saveValue3;
                            }
                            else
                            {
                                saveLocation3 = memory[opCodePosition + 1];
                            }

                            memory[saveLocation3] = input;

                            awaitingInput = false;

                            opCodePosition += 2;

                            break;
                        }
                        else
                        {
                            awaitingInput = true;

                            return;
                            //return false;
                        }

                        break;


                    case "04":

                        if (parameterModes[2] == 0)
                        {
                            var saveLocation4 = memory[opCodePosition + 1];

                            LastOutput = memory[saveLocation4].ToString();

                            outputSignals.Enqueue(LastOutput);
                        }
                        else if (parameterModes[2] == 1)
                        {
                            var saveLocation4 = memory[opCodePosition + 1];

                            LastOutput = saveLocation4.ToString();

                            outputSignals.Enqueue(LastOutput);
                        }
                        else
                        {
                            var saveValue4 = memory[(opCodePosition + 1)];
                            var saveLocation4 = relativeBase + saveValue4;

                            LastOutput = memory[saveLocation4].ToString();

                            outputSignals.Enqueue(LastOutput);
                        }

                        //Console.WriteLine(LastOutput);

                        opCodePosition += 2;
                        break;

                    case "05":

                        long firstValue = 0;

                        if (parameterModes[2] == 0)
                        {
                            var firstParameter = memory[opCodePosition + 1];

                            firstValue = memory[firstParameter];
                        }
                        else if (parameterModes[2] == 1)
                        {
                            firstValue = memory[opCodePosition + 1];
                        }
                        else
                        {
                            firstValue = memory[(opCodePosition + 1)];

                            firstValue = memory[relativeBase + firstValue];
                        }

                        if (firstValue > 0)
                        {
                            if (parameterModes[1] == 0)
                            {
                                var secondParameter = memory[opCodePosition + 2];

                                opCodePosition = memory[secondParameter];
                            }
                            else if (parameterModes[1] == 1)
                            {
                                opCodePosition = memory[opCodePosition + 2];
                            }
                            else
                            {
                                var secondValue5 = memory[opCodePosition + 2];
                                opCodePosition = memory[relativeBase + secondValue5];
                            }
                        }
                        else
                        {
                            opCodePosition += 3;
                        }

                        break;

                    case "06":

                        long firstValue1 = 0;

                        if (parameterModes[2] == 0)
                        {
                            var firstParameter1 = memory[opCodePosition + 1];

                            firstValue1 = memory[firstParameter1];
                        }
                        else if (parameterModes[2] == 1)
                        {
                            firstValue1 = memory[opCodePosition + 1];
                        }
                        else
                        {
                            firstValue1 = memory[opCodePosition + 1];
                            firstValue1 = memory[relativeBase + firstValue1];
                        }


                        if (firstValue1 == 0)
                        {
                            if (parameterModes[1] == 0)
                            {
                                var secondParameter1 = memory[opCodePosition + 2];

                                opCodePosition = memory[secondParameter1];
                            }
                            else if (parameterModes[1] == 1)
                            {
                                opCodePosition = memory[opCodePosition + 2];
                            }
                            else
                            {
                                var secondValue6 = memory[opCodePosition + 2];
                                opCodePosition = memory[relativeBase + secondValue6];
                            }

                        }
                        else
                        {
                            opCodePosition += 3;
                        }


                        break;

                    case "07":

                        long firstValue2 = 0;

                        if (parameterModes[2] == 0)
                        {
                            var firstParameter2 = memory[opCodePosition + 1];

                            firstValue2 = memory[firstParameter2];
                        }
                        else if (parameterModes[2] == 1)
                        {
                            firstValue2 = memory[opCodePosition + 1];
                        }
                        else
                        {
                            firstValue2 = memory[opCodePosition + 1];
                            firstValue2 = memory[relativeBase + firstValue2];
                        }

                        long secondValue = 0;
                        if (parameterModes[1] == 0)
                        {
                            var secondParameter2 = memory[opCodePosition + 2];

                            secondValue = memory[secondParameter2];
                        }
                        else if (parameterModes[1] == 1)
                        {
                            secondValue = memory[opCodePosition + 2];
                        }
                        else
                        {
                            secondValue = memory[opCodePosition + 2];
                            secondValue = memory[relativeBase + secondValue];
                        }

                        var thirdParameter = memory[opCodePosition + 3];

                        if (firstValue2 < secondValue)
                        {
                            if (parameterModes[0] == 2)
                            {
                                memory[relativeBase + thirdParameter] = 1;
                            }
                            else
                            {
                                memory[thirdParameter] = 1;
                            }
                            
                        }
                        else
                        {
                            if (parameterModes[0] == 2)
                            {
                                memory[relativeBase + thirdParameter] = 0;
                            }
                            else
                            {
                                memory[thirdParameter] = 0;
                            }
                        }

                        opCodePosition += 4;

                        break;

                    case "08":

                        long firstValue3 = 0;

                        if (parameterModes[2] == 0)
                        {
                            var firstParameter3 = memory[opCodePosition + 1];

                            firstValue3 = memory[firstParameter3];
                        }
                        else if (parameterModes[2] == 1)
                        {
                            firstValue3 = memory[opCodePosition + 1];
                        }
                        else
                        {
                            firstValue3 = memory[opCodePosition + 1];
                            firstValue3 = memory[relativeBase + firstValue3];
                        }

                        long secondValue3 = 0;

                        if (parameterModes[1] == 0)
                        {
                            var secondParameter3 = memory[opCodePosition + 2];
                            secondValue3 = memory[secondParameter3];
                        }
                        else if (parameterModes[1] == 1)
                        {
                            secondValue3 = memory[opCodePosition + 2];
                        }
                        else
                        {
                            secondValue3 = memory[opCodePosition + 2];
                            secondValue3 = memory[relativeBase + secondValue3];
                        }


                        var thirdParameter2 = memory[opCodePosition + 3];

                        if (firstValue3 == secondValue3)
                        {
                            if (parameterModes[0] == 2)
                            {
                                memory[relativeBase + thirdParameter2] = 1;
                            }
                            else
                            {
                                memory[thirdParameter2] = 1;
                            }
                            
                        }
                        else
                        {
                            if (parameterModes[0] == 2)
                            {
                                memory[relativeBase + thirdParameter2] = 0;
                            }
                            else
                            {
                                memory[thirdParameter2] = 0;
                            }
                        }


                        opCodePosition += 4;

                        break;

                    case "09":

                        long firstValue9 = 0;

                        var param = parameterModes[2];

                        //relativeBase += memory[opCodePosition + 1];

                        if (parameterModes[2] == 0)
                        {
                            var firstParameter9 = memory[opCodePosition + 1];

                            firstValue9 = memory[firstParameter9];

                            relativeBase += firstValue9;
                        }
                        else if (parameterModes[2] == 1)
                        {
                            relativeBase += memory[opCodePosition + 1];
                        }
                        else
                        {
                            var firstParameter9 = memory[opCodePosition + 1];

                            var offsetAddress = memory[relativeBase + firstParameter9];

                            relativeBase += offsetAddress;
                        }

                        opCodePosition += 2;

                        break;
                }
            }

            //return executing;
        }

        private void OpCode1(ref long[] memory, long opCodePosition, int[] parameterModes)
        {

            long firstValue = 0;

            if (parameterModes[2] == 0)
            {
                var firstPosition = memory[opCodePosition + 1];
                firstValue = memory[firstPosition];
            }
            else if(parameterModes[2] == 1)
            {
                firstValue = memory[opCodePosition + 1];
            }
            else
            {
                firstValue = memory[(opCodePosition + 1)];

                firstValue = memory[relativeBase + firstValue];
            }

            long secondValue = 0;
            if (parameterModes[1] == 0)
            {
                var secondPosition = memory[opCodePosition + 2];
                secondValue = memory[secondPosition];
            }
            else if (parameterModes[1] == 1)
            {
                secondValue = memory[opCodePosition + 2];
            }
            else
            {
                secondValue = memory[opCodePosition + 2];
                secondValue = memory[relativeBase + secondValue];
            }

            //var thirdValue = 0;
            //if (parameterModes[0] == 0)
            //{
            //    var thirdPosition = lineSplit[opCodePosition + 3];
            //    thirdValue = lineSplit[thirdPosition];
            //}
            //else
            //{
            //    thirdValue = lineSplit[opCodePosition + 3];
            //}

            long saveLocation = 0;

            if (parameterModes[0] == 2)
            {
                var saveValue = memory[(opCodePosition + 3)];

                saveLocation = relativeBase + saveValue;
            }
            else
            {
                saveLocation = memory[opCodePosition + 3];
            }


            memory[saveLocation] = (firstValue + secondValue);
        }

        private void OpCode2(ref long[] memory, long opCodePosition, int[] parameterModes)
        {
            long firstValue = 0;

            if (parameterModes[2] == 0)
            {
                var firstPosition = memory[opCodePosition + 1];
                firstValue = memory[firstPosition];
            }
            else if (parameterModes[2] == 1)
            {
                firstValue = memory[opCodePosition + 1];
            }
            else
            {
                firstValue = memory[(opCodePosition + 1)];
                firstValue = memory[relativeBase + firstValue];
            }

            long secondValue = 0;
            if (parameterModes[1] == 0)
            {
                var secondPosition = memory[opCodePosition + 2];
                secondValue = memory[secondPosition];
            }
            else if (parameterModes[1] == 1)
            {
                secondValue = memory[opCodePosition + 2];
            }
            else
            {
                secondValue = memory[(opCodePosition + 2)];
                secondValue = memory[relativeBase + secondValue];
            }

            long saveLocation = 0;

            if (parameterModes[0] == 2)
            {
                var saveValue = memory[(opCodePosition + 3)];
                saveLocation = relativeBase + saveValue;
            }
            else
            {
                saveLocation = memory[opCodePosition + 3];
            }

            memory[saveLocation] = (firstValue * secondValue);
        }


    }
}
