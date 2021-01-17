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
    public class Day2
    {
        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day2.txt", "InputTests")]
        public void Day2Part1()
        {
            var inputLines = File.ReadAllLines(@"ProblemInputs\Day2.txt");
            int validPasswords = 0;
            foreach (var password in inputLines)
            {
                if (PasswordIsValid(password)) validPasswords++; 
            }

            Console.WriteLine(validPasswords);
        }

        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day2.txt", "InputTests")]
        public void Day2Part2()
        {
            var inputLines = File.ReadAllLines(@"ProblemInputs\Day2.txt");
            int validPasswords = 0;
            foreach (var password in inputLines)
            {
                if (PasswordIsValidByPosition(password)) validPasswords++;
            }

            Console.WriteLine(validPasswords);
        }

        [TestMethod]
        public void EdgeCases()
        {
            Assert.IsTrue(PasswordIsValid("2-9 c: ccccccccc"));
            Assert.IsFalse(PasswordIsValid("1-3 b: cdefg"));
            Assert.IsTrue(PasswordIsValid("1-3 a: abcde"));

        }
        [TestMethod]
        public void EdgeCasesByPostion()
        {
            Assert.IsFalse(PasswordIsValidByPosition("2-9 c: ccccccccc"));
            Assert.IsFalse(PasswordIsValidByPosition("1-3 b: cdefg"));
            Assert.IsTrue(PasswordIsValidByPosition("1-3 a: abcde"));

        }

        private bool PasswordIsValid(string password)
        {
            var splitInput = password.Split(" ");
            var frequency = splitInput[0].Split("-");
            var frequencyFloor = int.Parse(frequency[0]);
            var frequencyCieling = int.Parse(frequency[1]);

            char letter = (splitInput[1])[0];

            var Userpassword = splitInput[2];

            int LetterFoundCount = 0;

            foreach (var character in Userpassword)
            {
                if (character == letter) LetterFoundCount++;
            }
            
            return frequencyFloor <= LetterFoundCount && LetterFoundCount <= frequencyCieling;
        }

        private bool PasswordIsValidByPosition(string password)
        {
            var splitInput = password.Split(" ");
            var positions = splitInput[0].Split("-");
            var postion1 = int.Parse(positions[0]) -1; // there is no concept of zero index so we must subtract 1
            var postion2 = int.Parse(positions[1])-1 ;

            char letter = (splitInput[1])[0];

            var Userpassword = splitInput[2];

            bool first = Userpassword[postion1] == letter;
            bool second = Userpassword[postion2] == letter;
            bool third = first ^ second;
            return third;

        }
    }
}
