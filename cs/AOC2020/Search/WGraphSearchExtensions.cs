using System;
using System.Collections.Generic;
using AOC2020.Structure;

namespace AOC2020.Search
{
    public static class WGraphSearchExtensions
    {
        public static WGraphNode<T> Dijkstras<T>(this WGraph<T> Graph, WGraphNode<T> start, Func<T, bool> searchFn)
        {
            var edgeQueue = new PriorityQueue<WGraphEdge<T>>(edge => edge.Weight);
            var seen = new HashSet<T>();
            seen.Add(start.Data);
            edgeQueue.Enqueue(new WGraphEdge<T>(start, 0));
            while (edgeQueue.Count > 0)
            {
                var current = edgeQueue.Dequeue();
                if (searchFn(current.Destination.Data))
                {
                    return current.Destination;
                }
                else
                {
                    foreach (var edge in current.Destination.Edges)
                    {
                        if (!seen.Contains(edge.Destination.Data))
                        {
                            edgeQueue.Enqueue(edge);
                            seen.Add(edge.Destination.Data);
                        }
                    }
                }
            }

            return null;
        }

        public static WGraphNode<T> AStar<T>(this WGraph<T> Graph, WGraphNode<T> start, Func<T, int> heuristicFn, Func<T, bool> searchFn)
        {
            var edgeQueue = new PriorityQueue<WGraphEdge<T>>(edge => edge.Weight + heuristicFn(edge.Destination.Data));
            var seen = new HashSet<T>();
            seen.Add(start.Data);
            edgeQueue.Enqueue(new WGraphEdge<T>(start, 0));
            while (edgeQueue.Count > 0)
            {
                var current = edgeQueue.Dequeue();
                if (searchFn(current.Destination.Data))
                {
                    return current.Destination;
                }
                else
                {
                    foreach (var edge in current.Destination.Edges)
                    {
                        if (!seen.Contains(edge.Destination.Data))
                        {
                            edgeQueue.Enqueue(edge);
                            seen.Add(edge.Destination.Data);
                        }
                    }
                }
            }

            return null;
        }
    }
}