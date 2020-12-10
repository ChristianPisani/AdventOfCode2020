using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day7 {
    class Program {
        public static List<Rule> BagRules;
        public static int Evaluations = 0;

        public static void Run()
        {
            BagRules = new List<Rule>();            

            foreach (var rule in Day7.Input.Values)
            {
                var rules = rule.Split(',').ToList();
                var bagtype = rules[0].Split(new string[] { "contain" }, StringSplitOptions.None);
                var colorRules = bagtype[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                rules.RemoveAt(0);
                colorRules.AddRange(rules);

                var colors = colorRules.Select(x => x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToList();

                try
                {
                    var r = new Rule()
                    {
                        BagType = bagtype[0],
                        AllowedColors = colors?.Select(x => new Tuple<int, string>(int.Parse(x[0]), x[1] + " " + x[2])).ToList()
                    };

                    r.BagType = r.BagType.Replace(" bags ", "");
                    r.BagType = r.BagType.Replace(" bags", "");
                    r.BagType = r.BagType.Replace("bags", "");
                    r.BagType = r.BagType.Replace("bag", "");

                    BagRules.Add(r);
                } catch (Exception e)
                {
                    var r = (new Rule()
                    {
                        BagType = bagtype[0],
                        AllowedColors = new List<Tuple<int, string>>()
                    });

                    r.BagType = r.BagType.Replace(" bags ", "");
                    r.BagType = r.BagType.Replace(" bags", "");
                    r.BagType = r.BagType.Replace("bags", "");
                    r.BagType = r.BagType.Replace("bag", "");

                    BagRules.Add(r);
                }
            }

            var sum = 0;

            var shinyBags = BagRules.Where(x => x.AllowedColors.Any(z => z.Item2 == "shiny gold")).ToList();

            for(int i = 0; i < shinyBags.Count; i++)
            {
                var shinyIndex = BagRules.FindIndex(x => x.BagType == shinyBags[i].BagType);

                BagRules[shinyIndex].CanHoldGold = true;

                foreach (var bagType in BagRules[shinyIndex].AllowedColors)
                {
                    if (bagType.Item2 != "shiny gold")
                    {
                        
                    } else
                    {
                        var bagIndex = BagRules.FindIndex(x => x.BagType == shinyBags[i].BagType);
                        Evaluate(BagRules[bagIndex]);
                    }
                }
            }

            sum = BagRules.Where(x => x.CanHoldGold).ToList().Count;


            var shinyGold = BagRules.FirstOrDefault(x => x.BagType == "shiny gold");
            Console.WriteLine(EvaluateNumber(shinyGold, 0, 1));

            Console.ReadKey();
        }

        public static void Evaluate(Rule rule)
        {
            Evaluations++;

            var applicableBags = BagRules.Where(x => x.AllowedColors.Any(z => z.Item2 == rule.BagType)).ToList();

            var index = BagRules.IndexOf(rule);
            BagRules[index].CanHoldGold = true;

            foreach(var r in applicableBags)
            {
                Evaluate(r);
            }
        }

        public static int EvaluateNumber(Rule rule, int sum, int amount)
        {            
            if (rule.AllowedColors.Count > 0)
            {
                var localSum = 0;
                foreach (var color in rule.AllowedColors)
                {
                    var evaluation = EvaluateNumber(BagRules[BagRules.IndexOf(BagRules.FirstOrDefault(x => x.BagType == color.Item2))], sum, color.Item1);
                    localSum +=(color.Item1 * evaluation);
                }
                sum += 1 + localSum;
            } else
            {
                return 1;
            }

            return sum;
        }
    }

    class Rule {
        public string BagType;
        public List<Tuple<int, string>> AllowedColors;

        public bool CanHoldGold = false;
        public bool Evaluated = false;

        bool _checkedCanHoldGolden = false;
        bool _canHoldGolden = false;

        public bool CanHoldGolden(string check)
        {
            var golden = "shiny gold";

            if (!_checkedCanHoldGolden)
            {
                foreach (var c in AllowedColors)
                {
                    if (c.Item2 == golden)
                    {
                        _canHoldGolden = true;
                    }
                    else
                    {
                        var colorToCheck = Program.BagRules.FirstOrDefault(x => x.BagType == c.Item2);

                        if (colorToCheck != null)
                        {
                            _canHoldGolden = colorToCheck.CanHoldGolden(check + c.Item2);
                        }
                    }
                }

                _checkedCanHoldGolden = true;
            }

            return _canHoldGolden;
        }

        public override string ToString()
        {
            return $"Bagtype: {BagType} AllowedColors: {string.Join(", ", AllowedColors)}";
        }
    }
}
