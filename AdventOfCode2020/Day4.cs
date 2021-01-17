using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    [TestClass]
    public class Day4
    {
        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day4.txt", "InputTests")]
        public void Day4UnitTests()
        {
            List<string> RequiredPassportFields = new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            string input = File.ReadAllText(@"ProblemInputs\Day4.txt");
            var passports = input.Split("\r\n\r\n")
                .Select(r => r.Replace("\r\n", " ").Split(' '))
                .Select(r => r.Select(rr => rr.Split(':'))
                    .ToDictionary(k => k[0], v => v[1]));
            int countOfValidPassports = 0;
            foreach (var passport in passports)
            {
                if (RequiredPassportFields.All(x => passport.ContainsKey(x)) && IsValid(passport)) countOfValidPassports++;

            }
            Console.WriteLine(countOfValidPassports);
            

        }

        //looked up regex values
        public static bool IsValid(Dictionary<string, string> input)
        {
            if (!IsBetween(int.Parse(input["byr"]), 1920, 2002))
            {
                return false;
            }

            if (!IsBetween(int.Parse(input["iyr"]), 2010, 2020))
            {
                return false;
            }

            if (!IsBetween(int.Parse(input["eyr"]), 2020, 2030))
            {
                return false;
            }

            var hgt = Regex.Match(input["hgt"], @"^(?<value>\d{2,3})(?<unit>cm|in)$");
            if (!hgt.Success)
            {
                return false;
            }

            if (hgt.Groups["unit"].Value == "cm" && !IsBetween(int.Parse(hgt.Groups["value"].Value), 150, 193))
            {
                return false;
            }

            if (hgt.Groups["unit"].Value == "in" && !IsBetween(int.Parse(hgt.Groups["value"].Value), 59, 76))
            {
                return false;
            }

            if (!Regex.IsMatch(input["hcl"], "^#[0-9a-f]{6}$"))
            {
                return false;
            }

            if (!Regex.IsMatch(input["ecl"], @"(amb|blu|brn|gry|grn|hzl|oth)"))
            {
                return false;
            }

            if (!Regex.IsMatch(input["pid"], @"^\d{9}$"))
            {
                return false;
            }

            return true;
        }

        private static bool IsBetween(int input, int min, int max) => input >= min && input <= max;
    }
}
