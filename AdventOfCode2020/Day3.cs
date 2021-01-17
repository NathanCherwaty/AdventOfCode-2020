using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    [TestClass]
    public class Day3
    { 
        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day3.txt")]
        public void Day3UnitTests()
        {
            var input = File.ReadAllLines(@"Day3.txt");
            
            var run1 = GetTreesEncountered(input, 1,1);
            var run2 = GetTreesEncountered(input, 3, 1);
            var run3 = GetTreesEncountered(input, 5, 1);
            var run4 = GetTreesEncountered(input, 7, 1);
            var run5 =  GetTreesEncountered(input, 1, 2);

            long result = (long)run1 * run2 * run3 * run4 * run5;
            Console.WriteLine(result);
        }

        private static int GetTreesEncountered(string[] input, int right, int down)
        {
            int lineMultiplyer = 0;
            int treesEncountered = 0;
            for (int i = 1; i < input.Count(); i++)
            {
                int linenumber = i * down;
                if (linenumber > input.Count()) break;// there are no more line left to go down.
                string line = input[i * down];
                var SlopePostion = (i * right) % line.Length;
                if (line[SlopePostion] == '#') treesEncountered++;
                lineMultiplyer++;
            }

            return treesEncountered;
        }

    }
}
