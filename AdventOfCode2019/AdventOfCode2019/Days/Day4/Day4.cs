using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day4
{
    class Day4
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day4\day4input.txt"))
            {
               
            }

            var hash = new HashSet<int>();

            for (var i = 246540; i <= 787419; i++)
            {
                char pastChar = '#';
                var count = 0;
                bool adjacentDigitsTheSame = false;
                bool digitsIncrease = true;
                var lengthOfRepeated = 1;

                foreach(var number in i.ToString().ToCharArray())
                {
                    count++;

                    if (count == 1)
                    {
                        pastChar = number;
                        continue;
                    }

                    if (pastChar == number)
                    {
                        adjacentDigitsTheSame = true;
                    }

                    if (int.Parse(pastChar.ToString()) > int.Parse(number.ToString()))
                    {
                        digitsIncrease = false;
                    }

                    if (!digitsIncrease)
                    {
                        break;
                    }

                    pastChar = number;


                }

                if (!digitsIncrease || !adjacentDigitsTheSame)
                {
                    continue;
                }

                hash.Add(i);
            }

            Console.WriteLine(hash.Count);
        }

        public void Part2()
        {
            Console.WriteLine("Part 2");

            var hash = new HashSet<int>();

            for (var i = 246540; i <= 787419; i++)
            {
                char pastChar = '#';
                var count = 0;
                bool adjacentDigitsTheSame = false;
                bool digitsIncrease = true;
                var lengthOfRepeated = 1;
                Dictionary<char, bool> containsGroupOf2 = new Dictionary<char, bool>();


                foreach (var number in i.ToString().ToCharArray())
                {
                    count++;

                    if (count == 1)
                    {
                        pastChar = number;
                        continue;
                    }

                    if (pastChar == number)
                    {
                        if (lengthOfRepeated >= 2)
                        {
                            if (containsGroupOf2.Any())
                            {
                                if (containsGroupOf2.TryGetValue(number, out var value))
                                {
                                    containsGroupOf2.Remove(number);
                                }
                            }

                            if (!containsGroupOf2.Any())
                            {
                                pastChar = number;

                                adjacentDigitsTheSame = false;

                                continue;
                            }

                            continue;
                        }

                        lengthOfRepeated++;
                        adjacentDigitsTheSame = true;
                        containsGroupOf2.TryAdd(number, true);
                    }
                    else
                    {
                        lengthOfRepeated = 1;
                    }

                    if (int.Parse(pastChar.ToString()) > int.Parse(number.ToString()))
                    {
                        digitsIncrease = false;
                    }

                    if (!digitsIncrease)
                    {
                        pastChar = number;
                        break;
                    }

                    pastChar = number;


                }

                if (!digitsIncrease || !adjacentDigitsTheSame)
                {
                    continue;
                }

                hash.Add(i);
            }

            Console.WriteLine(hash.Count);
        }
    }
}
