using System;

namespace AdventOfCode2020.Day3 {
    class Program {
        public static void Run()
        {  
            Console.WriteLine(
                TreeCount(1, 1) 
                * TreeCount(3, 1)
                * TreeCount(5, 1)
                * TreeCount(7, 1)
                * TreeCount(1, 2));
            Console.ReadKey();
        }

        public static int TreeCount(int xStep, int yStep)
        {

            var lineLength = Input.Values[0].Length;
            var x = 0;
            var y = 0;
            var trees = 0;

            while (y < Input.Values.Count)
            {
                if (Input.Values[y][x % lineLength].ToString() == "#")
                {
                    trees++;
                }

                x += xStep;
                y += yStep;
            }

            return trees;
        }
    }
}
