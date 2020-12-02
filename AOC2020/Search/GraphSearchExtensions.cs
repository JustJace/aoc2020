using System;
using System.Collections.Generic;
using AOC2020.Structure;

namespace AOC2020.Search
{
    public static class GraphSearchExtensions
    {
        public static GraphNode<T> DFS<T>(this Graph<T> Graph, GraphNode<T> start, Func<T, bool> searchFn)
        {
            var nodeStack = new Stack<GraphNode<T>>();
            var seen = new HashSet<T>();
            seen.Add(start.Data);
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
                    foreach (var neighbor in current.Neighbors)
                    {
                        if (!seen.Contains(neighbor.Data))
                        {
                            nodeStack.Push(neighbor);
                            seen.Add(neighbor.Data);
                        }
                    }
                }
            }

            return null;
        }

        public static GraphNode<T> BFS<T>(this Graph<T> Graph, GraphNode<T> start, Func<T, bool> searchFn)
        {
            var nodeQueue = new Queue<GraphNode<T>>();
            var seen = new HashSet<T>();
            seen.Add(start.Data);
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
                    foreach (var neighbor in current.Neighbors)
                    {
                        if (!seen.Contains(neighbor.Data))
                        {
                            nodeQueue.Enqueue(neighbor);
                            seen.Add(neighbor.Data);
                        }
                    }
                }
            }

            return null;
        }

        public static GraphNode<T> Dijkstras<T>(this Graph<T> Graph, GraphNode<T> start, Func<T, int> valueFn, Func<T, bool> searchFn)
        {
            var nodeQueue = new PriorityQueue<GraphNode<T>>(node => valueFn(node.Data));
            var seen = new HashSet<T>();
            seen.Add(start.Data);
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
                    foreach (var neighbor in current.Neighbors)
                    {
                        if (!seen.Contains(neighbor.Data))
                        {
                            nodeQueue.Enqueue(neighbor);
                            seen.Add(neighbor.Data);
                        }
                    }
                }
            }

            return null;
        }

        public static GraphNode<T> AStar<T>(this Graph<T> Graph, GraphNode<T> start, Func<T, int> valueFn, Func<T, int> heuristicFn, Func<T, bool> searchFn)
        {
            return Dijkstras(Graph, start, data => valueFn(data) + heuristicFn(data), searchFn);
        }
    }
}