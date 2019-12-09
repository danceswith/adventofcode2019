using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days.Day7
{
    class Day7
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day7\day7input.txt"))
            {
                var intComputerProgram = streamReader.ReadLine();

                var listOfPhaseSettingResults = new List<string>();

                var phaseCount = 0;

                phaseCount++;

                var phaseSettings = GetPermutations(Enumerable.Range(0, 5), 5);

                foreach (var phaseSetting in phaseSettings)
                {
                    var phaseSettingList = phaseSetting.ToList();

                    var listOfAmplifiers = new Dictionary<string, Amplifier>();

                    listOfAmplifiers.Add("A", new Amplifier(phaseSettingList[0]));
                    listOfAmplifiers.Add("B", new Amplifier(phaseSettingList[1]));
                    listOfAmplifiers.Add("C", new Amplifier(phaseSettingList[2]));
                    listOfAmplifiers.Add("D", new Amplifier(phaseSettingList[3]));
                    listOfAmplifiers.Add("E", new Amplifier(phaseSettingList[4]));

                    var input = "0";

                    foreach (var amplifier in listOfAmplifiers)
                    {
                        amplifier.Value.intComputer = new IntComputer(amplifier.Value.AmpPhaseSetting, intComputerProgram);

                        amplifier.Value.intComputer.inputSignals.Enqueue(input);

                        amplifier.Value.intComputer.ExecuteIntComputer();

                        input = amplifier.Value.intComputer.LastOutput;
                    }

                    listOfPhaseSettingResults.Add(input);
                }

                Console.WriteLine(phaseCount.ToString());

                Console.WriteLine(listOfPhaseSettingResults.Max());
            }
        }

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public void Part2()
        {
            Console.WriteLine("Part 2");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day7\day7input.txt"))
            {
                var intComputerProgram = streamReader.ReadLine();

                var listOfPhaseSettingResults = new List<int>();

                var phaseCount = 0;

                var phaseSettings = GetPermutations(Enumerable.Range(5, 5), 5);

                foreach (var phaseSetting in phaseSettings)
                {
                    phaseCount++;

                    var phaseSettingList = phaseSetting.ToList();

                    var listOfAmplifiers = new Dictionary<string, Amplifier>();

                    listOfAmplifiers.Add("A", new Amplifier(phaseSettingList[0]));
                    listOfAmplifiers.Add("B", new Amplifier(phaseSettingList[1]));
                    listOfAmplifiers.Add("C", new Amplifier(phaseSettingList[2]));
                    listOfAmplifiers.Add("D", new Amplifier(phaseSettingList[3]));
                    listOfAmplifiers.Add("E", new Amplifier(phaseSettingList[4]));

                    //listOfAmplifiers.Add("A", new Amplifier(9));
                    //listOfAmplifiers.Add("B", new Amplifier(8));
                    //listOfAmplifiers.Add("C", new Amplifier(7));
                    //listOfAmplifiers.Add("D", new Amplifier(6));
                    //listOfAmplifiers.Add("E", new Amplifier(5));

                    foreach (var amp in listOfAmplifiers)
                    {
                        amp.Value.intComputer = new IntComputer(amp.Value.AmpPhaseSetting, intComputerProgram);
                    }

                    listOfAmplifiers["A"].pairedAmp = listOfAmplifiers["B"];
                    listOfAmplifiers["B"].pairedAmp = listOfAmplifiers["C"];
                    listOfAmplifiers["C"].pairedAmp = listOfAmplifiers["D"];
                    listOfAmplifiers["D"].pairedAmp = listOfAmplifiers["E"];
                    listOfAmplifiers["E"].pairedAmp = listOfAmplifiers["A"];

                    listOfAmplifiers.First().Value.intComputer.inputSignals.Enqueue("0");

                    while(listOfAmplifiers.Values.All(a => a.intComputer.executing))
                    {
                        foreach(var amplifier in listOfAmplifiers)
                        {
                            amplifier.Value.intComputer.ExecuteIntComputer();
                       
                            if (!amplifier.Value.intComputer.executing)
                            {
                                if (amplifier.Key == "E")
                                {
                                    listOfPhaseSettingResults.Add(int.Parse(amplifier.Value.intComputer.LastOutput));
                                    break;
                                }
                                else
                                {
                                    var lastOutput = amplifier.Value.intComputer.LastOutput;

                                    amplifier.Value.pairedAmp.intComputer.inputSignals.Enqueue(lastOutput);

                                    continue;
                                }
                            }

                            if (amplifier.Value.intComputer.awaitingInput)
                            {
                                amplifier.Value.pairedAmp.intComputer.inputSignals.Enqueue(amplifier.Value.intComputer.LastOutput);

                                continue;
                            }
                        }
                    }

                    //listOfPhaseSettingResults.Add(input);
                }

                Console.WriteLine(phaseCount.ToString());

                Console.WriteLine(listOfPhaseSettingResults.Max());
            }
        }
    }

    class Amplifier
    {
        public string AmpName;
        public int AmpPhaseSetting;
        public string AmpResult;
        public IntComputer intComputer;
        public Amplifier pairedAmp;

        public Amplifier(int ampPhaseSetting)
        {
            this.AmpPhaseSetting = ampPhaseSetting;
        }
    }

    public class IntComputer
    {
        public int PhaseSetting = 0;
        public string IntComputerProgram = string.Empty;
        public Queue<string> inputSignals = new Queue<string>();
        public string LastOutput = string.Empty;
        public string[] lineSplit;
        public bool executing = false;
        public bool awaitingInput = false;
        private int opCodePosition = 0;
        public bool phaseSettingSet = false;

        public IntComputer(int phaseSetting, string intComputerProgram)
        {
            this.PhaseSetting = phaseSetting;
            this.inputSignals.Enqueue(phaseSetting.ToString());

            this.IntComputerProgram = intComputerProgram;
            lineSplit = intComputerProgram.Split(",");

            executing = true;
        }

        public void ExecuteIntComputer()
        {
                while (opCodePosition < lineSplit.Length)
                {
                    var opCode = lineSplit[opCodePosition];

                    var parameterModes = new int[3];

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

                            OpCode1(ref lineSplit, opCodePosition, parameterModes);

                            opCodePosition += 4;
                            break;

                        case "02":

                            OpCode2(ref lineSplit, opCodePosition, parameterModes);

                            opCodePosition += 4;
                            break;

                        case "03":
                        //Console.WriteLine("Enter your intput ... ");
 
                        if (inputSignals.Any())
                        {
                            var input = inputSignals.Dequeue();

                            var saveLocation3 = int.Parse(lineSplit[opCodePosition + 1]);

                            lineSplit[saveLocation3] = input;

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
                                var saveLocation4 = int.Parse(lineSplit[opCodePosition + 1]);

                            LastOutput =  lineSplit[saveLocation4];
                            }
                            else
                            {
                                var saveLocation4 = int.Parse(lineSplit[opCodePosition + 1]);

                            LastOutput =  saveLocation4.ToString();
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

            //return executing;
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


    }
}
