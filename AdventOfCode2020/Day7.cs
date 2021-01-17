using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    [TestClass]
    public class Day7
    {
        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day7.txt", "InputTests")]
        public void Day7Tests()
        {
            var input = File.ReadAllLines(@"InputTests\Day7.txt").ToList();
            var parsedBagList = ParseBagList(input);
            var parsedBagNumber = ParseNumberOfBagsContainedCount(input);

            Console.WriteLine(parsedBagList.Count(outerBag => CanContain(parsedBagList, outerBag.Key, "shiny gold")));
            Console.WriteLine(NumBagsContained(parsedBagNumber, "shiny gold")- 1);
        }

        private Dictionary<string, HashSet<string>> ParseBagList(List<string> lines)
        {
            var result = new Dictionary<string, HashSet<string>>();

            var regex = new Regex(@"^(\w+ \w+) bags contain (.+)\.");
            var matchRegex = new Regex(@"\d+ (\w+ \w+) bag");

            foreach (var line in lines)
            {
                var dest = new HashSet<string>();

                var groups = regex.Match(line).Groups;
                var source = groups[1].Value;
                if (groups[2].Value != "no other bags")
                {
                    var eachMatch = groups[2].Value.Split(", ");
                    foreach (var match in eachMatch)
                    {
                        dest.Add(matchRegex.Match(match).Groups[1].Value);
                    }
                }
                result[source] = dest;
            }

            return result;
        }

        private Dictionary<string, Dictionary<string, int>> ParseNumberOfBagsContainedCount(List<string> lines)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();

            var regex = new Regex(@"^(\w+ \w+) bags contain (.+)\.");
            var matchRegex = new Regex(@"(\d+) (\w+ \w+) bag");

            foreach (var line in lines)
            {
                var dest = new Dictionary<string, int>();

                var groups = regex.Match(line).Groups;
                var source = groups[1].Value;
                if (groups[2].Value != "no other bags")
                {
                    var eachMatch = groups[2].Value.Split(", ");
                    foreach (var match in eachMatch)
                    {
                        var matchGroups = matchRegex.Match(match).Groups;
                        var quantity = int.Parse(matchGroups[1].Value);
                        var color = matchGroups[2].Value;
                        dest[color] = quantity;
                    }
                }
                result[source] = dest;
            }

            return result;
        }

        private bool CanContain(Dictionary<string, HashSet<string>> mappings, string parentBag, string target, Dictionary<string, bool> BagContainsTargetResults = null)
        {
            BagContainsTargetResults = BagContainsTargetResults ?? new Dictionary<string, bool>();
            if (BagContainsTargetResults.ContainsKey(parentBag)) return BagContainsTargetResults[parentBag];

            if (mappings[parentBag].Count == 0)
            {
                BagContainsTargetResults[parentBag] = false;
                return false;
            }

            if (mappings[parentBag].Contains(target))
            {
                BagContainsTargetResults[parentBag] = true;
                return true;
            }

            if (mappings[parentBag].Any(childBag => CanContain(mappings, childBag, target, BagContainsTargetResults)))
            {
                BagContainsTargetResults[parentBag] = true;
                return true;
            }

            BagContainsTargetResults[parentBag] = false;
            return false;
        }

        private int NumBagsContained(Dictionary<string, Dictionary<string, int>> mappings, string source, Dictionary<string, int> bagContainsResult = null)
        {
            bagContainsResult ??= new Dictionary<string, int>();
            if (bagContainsResult.ContainsKey(source)) return bagContainsResult[source];

            var total = 1 + mappings[source].Select(it => it.Value * NumBagsContained(mappings, it.Key)).Sum();
            bagContainsResult[source] = total;
            return total;
        }
    }
}