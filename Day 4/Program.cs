using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day4 {
    class Program {
        public static void Run()
        {
            var requiredFields = new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            var optionalFields = new List<string>() { "cid" };

            var passwords = new List<string>();

            var password = "";
            foreach (var field in Day3.Input.Values)
            {
                if (!string.IsNullOrEmpty(field))
                {
                    password += " " + field;
                }
                else
                {
                    passwords.Add(password);
                    password = "";
                }
            }

            var valid = true;
            var amount = 0;

            foreach (var pass in passwords)
            {
                var fields = pass.Split(new char[] { ':', ' ' });
                var existingFields = new List<string>();

                for (int i = 1; i < fields.Length; i += 2)
                {
                    if (requiredFields.Contains(fields[i]) && !existingFields.Contains(fields[i]))
                    {
                        existingFields.Add(fields[i]);
                    }
                }

                valid = true;

                if (existingFields.Count == requiredFields.Count)
                {
                    for (int i = 1; i < fields.Length; i += 2)
                    {
                        var field = fields[i];
                        var p = fields[i + 1];

                        if (!IsValid(field, p))
                        {
                            valid = false;
                        }
                    }

                    if (valid)
                    {
                        amount++;
                    }
                }
            }


            Console.WriteLine(amount);
            Console.ReadKey();
        }

        public static bool IsValid(string type, string password)
        {
            try
            {
                switch (type)
                {
                    case "byr":
                        var p = int.Parse(password);
                        return password.Length == 4 && p >= 1920 && p <= 2002;
                        
                    case "iyr":
                        p = int.Parse(password);
                        return password.Length == 4 && p >= 2010 && p <= 2020;
                        
                    case "eyr":
                        p = int.Parse(password);
                        return password.Length == 4 && p >= 2020 && p <= 2030;
                        
                    case "hgt":
                        if (password.EndsWith("cm"))
                        {
                            password = password.Remove(password.Length - 2, 2);

                            p = int.Parse(password);

                            return p >= 150 && p <= 193;
                        }
                        else if (password.EndsWith("in"))
                        {
                            password = password.Remove(password.Length - 2, 2);

                            p = int.Parse(password);

                            return p >= 59 && p <= 76;
                        }
                        break;

                    case "hcl":
                        return password.Length == 7 && FakeRegex(password);
                        
                    case "ecl":
                        return password == "amb" || password == "blu" || password == "brn" || password == "gry" || password == "grn" || password == "hzl" || password == "oth";
                        
                    case "pid":
                        p = int.Parse(password);
                        return password.Length == 9;
                        
                    case "cid":
                        return true;

                    default:
                        return true;
                }
            } catch (Exception e)
            {
                return false;
            }

            return false;
        }

        public static bool FakeRegex(string pass)
        {
            var validChars = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };


            if (pass.StartsWith("#"))
            {
                for (int i = 1; i < pass.Length; i++)
                {
                    if (!validChars.Contains(pass[i].ToString()))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
