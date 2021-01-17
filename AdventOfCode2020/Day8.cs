using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    [TestClass]
    public class Day8
    {
        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day8.txt", "InputTests")]
        public void Day8Tests()
        {
            var input = File.ReadAllLines(@"InputTests\Day8.txt");

            int AccValue = 0;
            FindAccValueBeforeLoop(input, ref AccValue);
            Console.WriteLine(AccValue);

            for (int i = 0; i < input.Length - 1; i++)
            {
                AccValue = 0;
                string originalInstruction = input[i];
                (string op, int operand) = GetOpperationAccValue(originalInstruction);

                if (op == "jmp")
                {
                    op = "nop";
                }
                else if (op == "nop")
                {
                    op = "jmp";
                }

                input[i] = $"{op } {operand}";
                if (FindAccValueBeforeLoop(input, ref AccValue))
                {
                    Console.WriteLine(AccValue);
                    break;
                }

                input[i] = originalInstruction;
            }
        }

        private bool FindAccValueBeforeLoop(string[] instructions, ref int currentAcc)
        {
            int nextOp = 0;
            int acc = 0;

            HashSet<int> LinesExecuted = new HashSet<int>();

            for (int i = 0; i < instructions.Length - 1; i++)
            {
                if (LinesExecuted.Contains(nextOp))
                {
                    currentAcc = acc;
                    return false;
                }
                (string op, int AccValue) = GetOpperationAccValue(instructions[nextOp]);
                LinesExecuted.Add(nextOp);
                switch (op)
                {
                    case "nop":
                        nextOp++;

                        break;

                    case "acc":
                        acc += AccValue;
                        nextOp++;
                        break;

                    case "jmp":
                        nextOp += AccValue;

                        break;

                    default:
                        break;
                }
                if (nextOp == instructions.Length)
                {
                    currentAcc = acc;
                    return true;
                }
            }
            return false;
        }

        public (string, int) GetOpperationAccValue(string instruction)
        {
            string[] parts = instruction.Split(' ');
            string op = parts[0];
            int operand = int.Parse(parts[1]);
            return (op, operand);
        }
    }
}