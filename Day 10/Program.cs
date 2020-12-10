using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day10 {
    class Program {

        public static void Run()
        {
            var values = Day10.Input.Values;

            values.Sort();

            var steps = new List<Step>();

            var end = values.Max() + 3;
            values.Add(end);

            var dict = new Dictionary<int, Double>();

            foreach (var value in values)
            {
                dict[value] = 0;
            }
            dict[0] = 1;


            for (int i = 0; i < values.Count; i++)
            {
                for (int j = i + 1; j < values.Count; j++)
                {
                    var diff = values[j] - values[i];
                    //Console.WriteLine(diff);
                    if (diff > 3 || diff <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        dict[values[j]] += dict[values[i]];
                    }
                }
            }

            foreach (var key in dict.Keys)
            {
                Console.WriteLine(dict[key]);
            }
            Console.ReadLine();
        }

        public static void Part1(List<int> values)
        {
            var ones = 0;
            var threes = 0;

            for (int i = 1; i < values.Count; i++)
            {
                var diff = values[i] - values[i - 1];

                if (Math.Abs(diff) == 1)
                {
                    ones++;
                }

                if (Math.Abs(diff) == 3)
                {
                    threes++;
                }

                Console.WriteLine(values[i]);
            }

            Console.WriteLine("Ones: " + ones + " Threes: " + threes);

            Console.ReadLine();
        }

        public static void Part2()
        {

        }
    }

    public class Step {
        public int number;
        public Step next;
        public List<int> sequence;
        public List<int> possibleNext;
        public bool ReachedEnd = false;
    }
}
