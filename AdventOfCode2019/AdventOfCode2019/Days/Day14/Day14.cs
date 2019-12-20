using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day14
{
    class Day14
    {
        Dictionary<string, long>  availableReactants = new Dictionary<string, long>();
        Dictionary<string, Reaction> reactions = new Dictionary<string, Reaction>();

        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day14\day14input.txt"))
            {
                availableReactants.Add("ORE", long.MaxValue);

                while (!streamReader.EndOfStream)
                {
                    var reactionString = streamReader.ReadLine();

                    var reaction = new Reaction(reactionString);

                    reactions.Add(reaction.Output.name, reaction);
                }

                CheckAndGenerate("FUEL", 1);

                Console.WriteLine((long.MaxValue - availableReactants["ORE"]).ToString());
            }
        }

        public void Part2()
        {
            reactions = new Dictionary<string, Reaction>();
            availableReactants = new Dictionary<string, long>();

            Console.WriteLine("Part 2");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day14\day14input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var reactionString = streamReader.ReadLine();

                    var reaction = new Reaction(reactionString);

                    reactions.Add(reaction.Output.name, reaction);
                }

                availableReactants.Add("ORE", 1000000000000);

                //Generate("FUEL", 1);

                var generatedWithOre = 431448;//1000000000000 - availableReactants["ORE"];

                while(true)
                {
                    var availableOre = availableReactants["ORE"];
                    Console.WriteLine($"available ore {availableOre}");
                    var c = Math.Ceiling((double)availableOre / generatedWithOre);

                    Console.WriteLine($"producing {c} fuels");

                    if (!Generate("FUEL", (long)c))
                    {
                        break;
                    }

                    Console.WriteLine($"currently produced {availableReactants["FUEL"]} fuels");
                }
                
                Console.WriteLine($"Maximum fuel with 1 tril ore = {availableReactants["FUEL"]}");
            }
        }

        private bool CheckAndGenerate(string reactant, long quantity)
        {
            if (!availableReactants.ContainsKey(reactant))
            {
                availableReactants.Add(reactant, 0);
            }

            var quantityToGenerate = quantity - availableReactants[reactant];

            if (availableReactants[reactant] < quantity)
            {
                if (!Generate(reactant, quantityToGenerate))
                {
                    return false;
                }
            }

            availableReactants[reactant] = availableReactants[reactant] - quantity;

            return true;
        }

        private bool Generate(string chemical, long quanity)
        {
            if (chemical == "ORE") // We have infinate ore so skipit
            {
                return false;
            }
                
            var reaction = reactions[chemical];

            var reactantsNeeded = (long)Math.Ceiling((double)quanity / reaction.Output.quantity);

            foreach (var reactant in reaction.Inputs)
            {
                if (!CheckAndGenerate(reactant.name, reactantsNeeded * reactant.quantity))
                {
                    return false;
                }
            }
                
            availableReactants[chemical] = availableReactants.GetValueOrDefault(chemical) + (reactantsNeeded * reaction.Output.quantity);
            return true;
        }
    }

    public class Reaction
    {
        public Reaction(string line)
        {
            this.Parse(line);
        }

        public Reactant Output { get; set; }
        public Reactant[] Inputs { get; set; }

        public void Parse(string reaction)
        {
            var reactionSplit = reaction.Split("=>").Select(r => r.Trim()).ToList();

            var outputSplit = reactionSplit[1].Split(" ");

            this.Output = new Reactant(outputSplit[1], int.Parse(outputSplit[0]));

            var inputs = reactionSplit[0].Split(',').ToList();
            var inputObjects = new List<Reactant>();

            foreach (var input in inputs)
            {
                var inputsplit = input.Trim().Split(" ");

                var inputObj = new Reactant(inputsplit[1], int.Parse(inputsplit[0]));

                inputObjects.Add(inputObj);
            }

            this.Inputs = inputObjects.ToArray();
        }
    }

    public class Reactant
    {
        public string name;
        public long quantity;

        public Reactant(string name, long quantity)
        {
            this.name = name;
            this.quantity = quantity;
        }
    }

}
