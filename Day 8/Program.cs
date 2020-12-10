using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day8 {
    class Program {
        public static int Accumulator;

        public static void Run()
        {
            
            var values = Day8.Input.Values;

            var index = 0;
            
            for(int i = 0; i < values.Count; i++ )
            {
                Accumulator = 0;
                var copy = values.Select(x => x).ToList();

                var value = copy[i].Split(' ');
                value[1] = copy[1].Replace("+", "");

                switch (value[0])
                {
                    case "acc":
                        
                        break;

                    case "jmp":
                        copy[i] = copy[i].Replace("jmp", "nop");

                        if(!Game(copy))
                        {
                            Console.WriteLine(Accumulator);
                            Console.ReadKey();
                        } else
                        {
                            Console.WriteLine("Loop");
                            //Console.ReadKey();
                        }

                        break;

                    case "nop":
                        
                        break;
                }
            }
        }        

        public static bool Game(List<string> values)
        {
            var jumped = false;
            var accumulated = false;
            var noped = false;

            var isInLoop = false;

            var visitedInstruction = values.Select(x => 0).ToList();

            bool gameIsOn = true;

            while (gameIsOn)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    var value = values[i].Split(' ');
                    value[1] = value[1].Replace("+", "");

                    if (visitedInstruction[i] > 400)
                    {
                        isInLoop = true;
                        gameIsOn = false;
                        break;
                    }

                    switch (value[0])
                    {
                        case "acc":
                            Accumulator += int.Parse(value[1]);


                            accumulated = true;
                            break;

                        case "jmp":
                            i -= 1;
                            i += int.Parse(value[1]);



                            jumped = true;
                            break;

                        case "nop":


                            noped = true;
                            break;
                    }


                    visitedInstruction[i]++;
                }

                return isInLoop;
            }

            return isInLoop;
        }
    }
}
