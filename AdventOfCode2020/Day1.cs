using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    [TestClass]
    public class Day1
    {
        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day1.txt", "InputTests")]
        public void Day1UnitTests()

        {
            var inputLines = File.ReadAllLines(@"ProblemInputs\Day1.txt");
            List<int> numberInput = new List<int>();
            foreach (var line in inputLines)
            {
                numberInput.Add(int.Parse(line));
            }
            var answer = ReturnProductOfTwoNumbersThatAddto2020(numberInput);
            Console.WriteLine(answer);
        }

        public int ReturnProductOfTwoNumbersThatAddto2020(List<int> NumberList)
        {
            foreach (var number in NumberList)
            {
                var counterSum = 2020 - number;
                if(NumberList.Contains(counterSum))
                {
                    return number * NumberList[NumberList.IndexOf(counterSum)];
                }
            }
            // counter sum was not found
            return -1;
        }

        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day1.txt", "InputTests")]
        public void Day1Part2()
        {
            var inputLines = File.ReadAllLines(@"ProblemInputs\Day1.txt");
            List<int> numberInput = new List<int>();
            foreach (var line in inputLines)
            {
                numberInput.Add(int.Parse(line));
            }
            var answer = ReturnProductOfThreeNumbersThatAddto2020(numberInput);
            Console.WriteLine(answer);
        }

        public int ReturnProductOfThreeNumbersThatAddto2020(List<int> NumberList)
        {
            int totalSum = 2020;
            for (int i = 0; i < NumberList.Count; i++)
            {
                int currentSum = totalSum - NumberList[i];
                for (int j = 0; j < NumberList.Count; j++)
                {
                    if (NumberList.Contains(currentSum - NumberList[j]))
                        return NumberList[i] * NumberList[j] * NumberList[NumberList.IndexOf(currentSum - NumberList[j])];
                }
            }
            //triplet was not found
            return -1;
        }


    }
}
