using System;

namespace AdventOfCode2020.Day2 {
    class Program {
        public static void Run()
        {
            var part1Amount = 0;
            var part2Amount = 0;

            for (int i = 0; i < Input.Values.Count; i++)
            {
                var input = Input.Values[i];

                var sides = input.Split(':');

                var value = sides[1];

                var splitRule = sides[0].Split(' ');
                var rules = splitRule[0].Split('-');

                var character = splitRule[1];

                var password1 = new Password()
                {
                    Max = int.Parse(rules[1]),
                    Min = int.Parse(rules[0]),
                    Char = character,
                    Value = value
                };

                var password2 = new PasswordVariant()
                {
                    Max = int.Parse(rules[1]),
                    Min = int.Parse(rules[0]),
                    Char = character,
                    Value = value
                };

                if (password1.IsValid()) part1Amount++;
                if (password2.IsValid()) part2Amount++;
            }

            Console.WriteLine($"Part 1: {part1Amount}");
            Console.WriteLine($"Part 2: {part2Amount}");
            Console.ReadLine();
        }
    }

    class Password {
        public int Max;
        public int Min;
        public string Char;
        public string Value;

        public bool IsValid()
        {
            int amount = 0;

            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i].ToString().Equals(Char))
                {
                    amount++;
                }
            }

            return amount <= Max && amount >= Min;
        }

        public override string ToString()
        {
            return $"Max: {Max}, Min: {Min}, Char: {Char}";
        }
    }

    class PasswordVariant {
        public int Max;
        public int Min;
        public string Char;
        public string Value;

        public bool IsValid()
        {
            Value = Value.Replace(" ", "");

            try
            {
                var firstLetterInPosition = Value[Min - 1].ToString().Equals(Char);
                var secondLetterInPosition = Value[Max - 1].ToString().Equals(Char);

                return (firstLetterInPosition || secondLetterInPosition) && !(firstLetterInPosition && secondLetterInPosition);
            } catch (Exception e)
            {

                return false;
            }
        }

        public override string ToString()
        {
            return $"Max: {Max}, Min: {Min}, Char: {Char}";
        }
    }
}
