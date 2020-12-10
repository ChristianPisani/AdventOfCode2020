using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day6 {
    class Program {
        public static void Run()
        {
            var answers = Day6.Input.Values;
            var sum = 0;

            var localAnswers = new List<string>();
            var uniqueAnswers = new List<char>();
            foreach(var ans in answers)
            {
                if(String.IsNullOrWhiteSpace(ans))
                {                    
                    var localSum = 0;

                    foreach(var c in uniqueAnswers)
                    {
                        var everyoneAnsweredYes = true;

                        foreach(var a in localAnswers)
                        {
                            if (!a.ToCharArray().ToList().Contains(c)) everyoneAnsweredYes = false;
                        }

                        if (everyoneAnsweredYes) localSum += 1;
                    }

                    sum += localSum;
                    uniqueAnswers = new List<char>();
                    localAnswers = new List<string>();
                } else
                {
                    localAnswers.Add(ans);

                    for (int i = 0; i < ans.Length; i++)
                    {
                        if(!uniqueAnswers.Contains(ans[i]))
                        {
                            uniqueAnswers.Add(ans[i]);
                        }
                    }
                }
            }
            
            Console.WriteLine(sum);
            Console.ReadKey();
        }        
    }
}
