using System.Collections.Generic;

namespace AOC2020.Structure
{
    public class Graph<T>
    {
        public Graph(IEnumerable<GraphNode<T>> nodes)
        {
            foreach (var node in nodes)
            {
                Nodes[node.Data] = node;
            }
        }
        public Dictionary<T, GraphNode<T>> Nodes { get; } = new Dictionary<T, Structure.GraphNode<T>>();
    }

    public class GraphNode<T>
    {
        public GraphNode(T data)
        {
            Data = data;
        }

        public T Data { get; }
        public HashSet<GraphNode<T>> Neighbors { get; } = new HashSet<GraphNode<T>>();
    }
}