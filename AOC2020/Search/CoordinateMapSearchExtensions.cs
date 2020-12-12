using System;
using System.Collections.Generic;
using AOC2020.Structure;

namespace AOC2020.Search
{
    public static class CoordinateMapSearchExtensions
    {
        public static CoordinateNode<T> BFS<T>(this CoordinateMap<T> map, CoordinateNode<T> start, Func<T, bool> searchFn, Func<T, bool> pruneFn)
        {
            var nodeQueue = new Queue<CoordinateNode<T>>();
            var seen = new HashSet<Coordinate>();
            seen.Add(start.Coordinate);
            nodeQueue.Enqueue(start);
            while (nodeQueue.Count > 0)
            {
                var current = nodeQueue.Dequeue();
                if (searchFn(current.Data))
                {
                    return current;
                }
                else
                {
                    foreach (var neighbor in current.OrthNeighbors)
                    {
                        if (!seen.Contains(neighbor.Coordinate))
                        {
                            seen.Add(neighbor.Coordinate);

                            if (pruneFn(neighbor.Data)) continue;

                            nodeQueue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return null;
        }

        public static CoordinateNode<T> DFS<T>(this CoordinateMap<T> map, CoordinateNode<T> start, Func<T, bool> searchFn, Func<T, bool> pruneFn)
        {
            var nodeStack = new Stack<CoordinateNode<T>>();
            var seen = new HashSet<Coordinate>();
            seen.Add(start.Coordinate);
            nodeStack.Push(start);
            while (nodeStack.Count > 0)
            {
                var current = nodeStack.Pop();
                if (searchFn(current.Data))
                {
                    return current;
                }
                else
                {
                    foreach (var neighbor in current.OrthNeighbors)
                    {
                        if (!seen.Contains(neighbor.Coordinate))
                        {
                            seen.Add(neighbor.Coordinate);

                            if (pruneFn(neighbor.Data)) continue;

                            nodeStack.Push(neighbor);
                        }
                    }
                }
            }

            return null;
        }
    }
}