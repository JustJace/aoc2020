using System.Collections.Generic;

namespace AOC2020.Structure
{
    public class WGraph<T>
    {
        public Dictionary<T, WGraphNode<T>> Nodes { get; } = new Dictionary<T, WGraphNode<T>>();
        public WGraph(IEnumerable<WGraphNode<T>> nodes)
        {
            foreach (var node in nodes)
            {
                Nodes[node.Data] = node;
            }
        }
    }

    public class WGraphNode<T>
    {
        public WGraphNode(T data)
        {
            Data = data;
        }

        public T Data { get; }
        public HashSet<WGraphEdge<T>> Edges { get; } = new HashSet<WGraphEdge<T>>();
    }

    public class WGraphEdge<T>
    {
        public WGraphEdge(WGraphNode<T> destination, int weight)
        {
            Destination = destination;
            Weight = weight;
        }

        public WGraphNode<T> Destination { get; }
        public int Weight { get; }
    }
}