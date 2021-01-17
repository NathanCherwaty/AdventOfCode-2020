using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    [TestClass]
    public class Day5
    {
        [TestMethod]
        [DeploymentItem(@"ProblemInputs\Day5.txt", "InputTests")]
        public void Day5UnitTests()
        {
            var input = File.ReadAllLines(@"ProblemInputs\Day5.txt");
            int maxSeatNumber = 0;
            List<int> seatIds = new List<int>();
            foreach (var passport in input)
            {
                var result = FindSeat(passport);
                seatIds.Add((result.Item1 * 8) + result.Item2);
                maxSeatNumber = maxSeatNumber > ((result.Item1 * 8) + result.Item2) ? maxSeatNumber : ((result.Item1 * 8) + result.Item2);
            }
            var sortedSeatIds = seatIds.OrderByDescending(i=> i).ToImmutableSortedSet();
            Console.WriteLine(Enumerable
                .Range(sortedSeatIds.Min(), seatIds.Count + 1)
                .SingleOrDefault(id => !seatIds.Contains(id)));
        }

        [TestMethod]
        public void exampleset ()
        {
            FindSeat("BFFFBBFRRR").Should().BeEquivalentTo(new Tuple<int, int>(70, 7));
            FindSeat("FFFBBBFRRR").Should().BeEquivalentTo(new Tuple<int, int>(14, 7));
            FindSeat("BBFFBBFRLL").Should().BeEquivalentTo(new Tuple<int, int>(102, 4));
            
        }

        // can do a divide and conquer method...

        public Tuple<int, int> FindSeat (string boardingPass)
        {
            int lastRow = 127;
            int lastColumn = 7;
            decimal currentFloor = 0;
            decimal currentCieling = 127;
            decimal currentColumnfloor = 0;
            decimal currentColumnCieling = 7;
            

            foreach (var character in boardingPass)
            {
                switch (character)
                {
                    case 'F':
                        {
                            currentCieling = Math.Floor((currentCieling + currentFloor) / 2);
                            break;
                        }
                    case 'B':
                        {
                            currentFloor = Math.Ceiling((currentFloor + currentCieling) / 2);
                            break;
                        }
                    case 'L':
                        {
                            currentColumnCieling = Math.Floor((currentColumnCieling + currentColumnfloor) / 2);
                            break;
                        }
                    case 'R':
                        {
                            currentColumnfloor = Math.Ceiling((currentColumnCieling + currentColumnfloor) / 2);
                            break;
                        }
                }
            }

            var row = boardingPass[6] == 'F' ? currentFloor : currentCieling;
            var seat = boardingPass[9] == 'L' ? currentColumnfloor : currentColumnCieling;

            return new Tuple<int, int>( (int)row, (int)seat );
        }
    }
}
