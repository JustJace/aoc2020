using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_011_2 : Solver<int>
    {
        public override int Day => 11;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_11.input";
        protected override int GetAnswer(string input)
        {
            var previous = new CoordinateMap<char>(input.As2DArray<char>(), c => c);
            var hasChangedSeat = true;
            var nextMapString = "";

            while (hasChangedSeat)
            {
                hasChangedSeat = false;
                nextMapString = "";

                for (var r = 0; r < previous.Height; r++)
                {
                    for (var c = 0; c < previous.Width; c++)
                    {
                        var current = previous.Locate(c, r);
                        if (current.Data == '.') { nextMapString += '.'; continue; }

                        var neighbors = CountVisibleNeighbors(current);

                        if (current.Data == 'L' && neighbors == 0) { hasChangedSeat = true; nextMapString += '#'; }
                        else if (current.Data == '#' && neighbors >= 5) { hasChangedSeat = true; nextMapString += 'L'; }
                        else { nextMapString += current.Data; }
                    }

                    nextMapString += Environment.NewLine;
                }

                previous = new CoordinateMap<char>(nextMapString.TrimEnd().As2DArray<char>(), c => c);
            }

            return nextMapString.Count(c => c == '#');
        }

        private int CountVisibleNeighbors(CoordinateNode<char> current)
        {
            var count = 0;
            if (HasOccupiedSeatInDirection(current, "NW")) count++;
            if (HasOccupiedSeatInDirection(current, "N")) count++;
            if (HasOccupiedSeatInDirection(current, "NE")) count++;
            if (HasOccupiedSeatInDirection(current, "E")) count++;
            if (HasOccupiedSeatInDirection(current, "SE")) count++;
            if (HasOccupiedSeatInDirection(current, "S")) count++;
            if (HasOccupiedSeatInDirection(current, "SW")) count++;
            if (HasOccupiedSeatInDirection(current, "W")) count++;
            return count;
        }

        private bool HasOccupiedSeatInDirection(CoordinateNode<char> current, string direction)
        {
            if (!DirectionExists(current, direction))
            {
                return false;
            }

            var seatInDirection = SeatInDirection(current, direction);

            if (seatInDirection.Data == '#')
            {
                return true;
            }
            else if (seatInDirection.Data == 'L')
            {
                return false;
            }
            else
            {
                return HasOccupiedSeatInDirection(seatInDirection, direction);
            }
        }

        private CoordinateNode<Char> SeatInDirection(CoordinateNode<char> current, string direction)
        {
            switch (direction)
            {
                case "NW": return current.NorthWest;
                case "N": return current.North;
                case "NE": return current.NorthEast;
                case "E": return current.East;
                case "SE": return current.SouthEast;
                case "S": return current.South;
                case "SW": return current.SouthWest;
                case "W": return current.West;
                default: throw new Exception($" wtf is this direction {direction}");
            }
        }

        private bool DirectionExists(CoordinateNode<char> current, string direction)
        {
            switch (direction)
            {
                case "NW": return current.HasNorthWest;
                case "N": return current.HasNorth;
                case "NE": return current.HasNorthEast;
                case "E": return current.HasEast;
                case "SE": return current.HasSouthEast;
                case "S": return current.HasSouth;
                case "SW": return current.HasSouthWest;
                case "W": return current.HasWest;
                default: throw new Exception($" wtf is this direction {direction}");
            }
        }
    }
}
