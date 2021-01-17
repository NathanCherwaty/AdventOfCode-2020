using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    [TestClass]
    public class Day6
    {
        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day6.txt", "InputTests")]
        public void Day6UnitTests()
        {
            string input = File.ReadAllText(@"ProblemInputs\Day6.txt");
            var Groups = input.Split("\r\n\r\n").ToList();
            var totalSum = 0;

            List<char> QuestionsAnsweredYes = new List<char>();
            Groups.ForEach(group => totalSum += group.Replace(Environment.NewLine, string.Empty)
                                                              .GroupBy(x => x)
                                                              .Select(x => new { Letter = x.Key, Count = x.Count() })
                                                              .Where(x => x.Count == group.Split(Environment.NewLine).Length)
                                                              .OrderBy(x => x.Letter)
                                                              .Select(x => x)
                                                              .Count());

            Console.WriteLine(totalSum);
        }
    }
}