using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days.Day8
{
    class Day8
    {
        public void Part1()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day8\day8input.txt"))
            {
                var width = 25;
                var height = 6;


                var line = streamReader.ReadLine();

                var listOfLayers = new List<List<string>>();


                var position = 0;

                while(true)
                {
                    //var lineChars = line.ToCharArray();

                    try
                    {
                        var image = new List<string>();

                        for(var h = 1; h <= height; h++)
                        {
                            var parsedWidth = line.Substring(position, width);

                            position = position + width;

                            image.Add(parsedWidth);
                        }

                        listOfLayers.Add(image);
                    }
                    catch
                    {
                        break;
                    }
                    
                }

                var dic = new Dictionary<int, List<string>>();

                foreach(var layer in listOfLayers)
                {
                    var joined = string.Join(' ', layer.ToArray()).Replace(" ", "");

                    var countofZeros = joined.ToCharArray().Where(c => c.Equals('0')).Count();

                    dic.TryAdd(countofZeros, layer);

                }

                var lowest = dic.Min(d => d.Key);

                var lowestLayer = dic[lowest];

                var numberof1 = string.Join(' ', lowestLayer.ToArray()).Replace(" ", "").ToCharArray().Where(c => c.Equals('1')).Count();

                var numberof2 = string.Join(' ', lowestLayer.ToArray()).Replace(" ", "").ToCharArray().Where(c => c.Equals('2')).Count();

                var result = numberof1 * numberof2;


                //var list = listOfLayers.SelectMany(l => l.Select(i => i)).ToList();

                //var countOfZeros = list.Select(v => v.ToCharArray().Where(a => a == '0'));

                //var lowest = countOfZeros.Min(z => z.Count());

                Console.WriteLine(result);



            }
        }

        public void Part2()
        {
            Console.WriteLine("Part 1");

            using (var streamReader = new StreamReader($@"{Common.Constants.FILE_LOCATION}Day8\day8input.txt"))
            {
                var width = 25;
                var height = 6;


                var line = streamReader.ReadLine();

                var listOfLayers = new List<List<string>>();


                var position = 0;

                while (true)
                {
                    //var lineChars = line.ToCharArray();

                    try
                    {
                        var image = new List<string>();

                        for (var h = 1; h <= height; h++)
                        {
                            var parsedWidth = line.Substring(position, width);

                            position = position + width;

                            image.Add(parsedWidth);
                        }

                        listOfLayers.Add(image);
                    }
                    catch
                    {
                        break;
                    }

                }

                var imageArray = new string[height, width];

                foreach(var layer in listOfLayers)
                {
                    for(var h = 0; h < height; h++)
                    {
                        var currentLayer = layer[h].ToCharArray();

                        for(var w = 0; w < width; w++)
                        {
                            var currentPixel = currentLayer[w];

                            if (currentPixel == '2')
                            {
                                if (string.IsNullOrWhiteSpace(imageArray[h, w]) 
                                    && imageArray[h, w] != "#" 
                                    && imageArray[h, w] != "-")
                                {
                                    imageArray[h, w] = " ";
                                }
                                
                                continue;
                            }

                            if (currentPixel == '1')
                            {
                                if (string.IsNullOrWhiteSpace(imageArray[h, w]))
                                {
                                    imageArray[h, w] = "#";
                                }
                                
                                continue;
                            }

                            if (currentPixel == '0')
                            {
                                if (string.IsNullOrWhiteSpace(imageArray[h, w]))
                                {
                                    imageArray[h, w] = "-";
                                }
                                
                                continue;  
                            }
                        }
                    }
                }

                using (var d = new StreamWriter(@"C:\Users\Alexander\Documents\adventofcode2019\AdventOfCode2019\AdventOfCode2019\Days\test.txt"))
                {
                    for (var h = 0; h < height; h++)
                    {
                        for (var w = 0; w < width; w++)
                        {
                       Console.Write(imageArray[h, w]);
                        }
                        Console.Write(Environment.NewLine);
                    }
                }
                

                //var dic = new Dictionary<int, List<string>>();

                //foreach (var layer in listOfLayers)
                //{
                //    var joined = string.Join(' ', layer.ToArray()).Replace(" ", "");

                //    var countofZeros = joined.ToCharArray().Where(c => c.Equals('0')).Count();

                //    dic.TryAdd(countofZeros, layer);

                //}

                //var lowest = dic.Min(d => d.Key);

                //var lowestLayer = dic[lowest];

                //var numberof1 = string.Join(' ', lowestLayer.ToArray()).Replace(" ", "").ToCharArray().Where(c => c.Equals('1')).Count();

                //var numberof2 = string.Join(' ', lowestLayer.ToArray()).Replace(" ", "").ToCharArray().Where(c => c.Equals('2')).Count();

                //var result = numberof1 * numberof2;


                ////var list = listOfLayers.SelectMany(l => l.Select(i => i)).ToList();

                ////var countOfZeros = list.Select(v => v.ToCharArray().Where(a => a == '0'));

                ////var lowest = countOfZeros.Min(z => z.Count());

                //Console.WriteLine(result);



            }
        }


    }
}
