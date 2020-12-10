using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day9 {
    class Program {
        
        public static void Run()
        {
            Double number = 1492208709;

            var input = Day9.Input.Values.Select(x => Double.Parse(x)).ToList();

            for(int i = 0; i < input.Count; i++)
            {
                for(int j = 1; j < input.Count; j++)
                {
                    if(i != j)
                    {
                        var sequence = input.Skip(i).Take(j);

                        if (sequence.Aggregate((a, b) => a + b) == number)
                        {
                            Double smallest = Double.MaxValue;
                            Double biggest = 0;

                            foreach(var num in sequence)
                            {
                                if (num < smallest) smallest = num;
                                if (num > biggest) biggest = num;
                            }

                            Console.WriteLine("From: " + sequence.First() + " To: " + sequence.Last() + " Smallest: " + smallest + " Biggest: " + biggest + " Answer: " + (smallest + biggest));
                            Console.ReadLine();
                        }
                    }
                }
            }
        }   

        public static void Part1()
        {
            int preambleLength = 25;

            var input = Day9.Input.Values;

            for (int i = 0; i < input.Count - preambleLength - 1; i++)
            {
                var range = input.Skip(i).Take(preambleLength).Select(x => Double.Parse(x)).ToList();
                var number = Double.Parse(input[preambleLength + i]);

                if (!NumberValid(range, number))
                {
                    Console.WriteLine(number);
                    Console.ReadLine();
                }
            }
        }
        
        public static bool NumberValid(List<Double> selection, Double number)
        {
            for(int i = 0; i < selection.Count; i++)
            {
                for(int j = 0; j < selection.Count; j++)
                {
                    if(j != i)
                    {
                        if(selection[i] + selection[j] == number)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
