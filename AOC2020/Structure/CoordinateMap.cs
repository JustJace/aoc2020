using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020.Structure
{
    public class CoordinateMap<T>
    {
        private readonly Func<char, T> _datanFn;
        private readonly int _width;
        private readonly int _height;

        public CoordinateMap(char[][] referenceMap, Func<char, T> dataFn)
        {
            ReferenceMap = referenceMap;
            this._height = referenceMap.Length;
            this._width = referenceMap[0].Length;
            this._datanFn = dataFn;
        }

        public Dictionary<Coordinate, CoordinateNode<T>> NodeMap { get; } = new Dictionary<Coordinate, CoordinateNode<T>>();
        public char[][] ReferenceMap { get; }
        public CoordinateNode<T> Locate(int x, int y) => Locate(new Coordinate(x, y));
        public CoordinateNode<T> Locate(Coordinate coordinate)
        {
            if (coordinate.X < 0 || coordinate.X >= _width) return null;
            if (coordinate.Y < 0 || coordinate.Y >= _height) return null;

            if (!NodeMap.ContainsKey(coordinate))
            {
                NodeMap[coordinate] = new CoordinateNode<T>(this, coordinate, _datanFn(ReferenceMap[coordinate.Y][coordinate.X]));
            }

            return NodeMap[coordinate];
        }
    }

    public class CoordinateNode<T>
    {
        private readonly CoordinateMap<T> _containingMap;

        public CoordinateNode(CoordinateMap<T> containingMap, Coordinate coordinate, T data)
        {
            this._containingMap = containingMap;
            Coordinate = coordinate;
            Data = data;
        }

        public T Data { get; }
        public Coordinate Coordinate { get; }
        public int X => Coordinate.X;
        public int Y => Coordinate.Y;

        public bool HasNorth => North != null;
        public bool HasSouth => South != null;
        public bool HasWest => West != null;
        public bool HasEast => East != null;
        public CoordinateNode<T> North => _containingMap.Locate(X, Y - 1);
        public CoordinateNode<T> South => _containingMap.Locate(X, Y + 1);
        public CoordinateNode<T> West => _containingMap.Locate(X - 1, Y);
        public CoordinateNode<T> East => _containingMap.Locate(X + 1, Y);
        public CoordinateNode<T>[] Neighbors => new [] { North, South, East, West }.Where(n => n != null).ToArray();
    }

    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; private set; }
        public int Y { get; private set; }
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate && (obj as Coordinate).X == X && (obj as Coordinate).Y == Y;
        }
    }
}