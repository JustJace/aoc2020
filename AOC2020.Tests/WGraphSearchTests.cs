using System.Linq;
using Xunit;
using AOC2020.Search;

namespace AOC2020.Tests
{
    public class WGraphSearchTests
    {
        private const string _acyclicGraph = @"A 3 B
B 4 C
C 2 D
D 4 F
Z 2 F
Y 1 Z
X 3 Y
Q 2 P
P 2 O";
        private const string _cyclicGraph = @"A 3 B
B 4 C
C 2 D
D 4 F
Z 2 F
Y 1 Z
X 3 Y
X 2 A
Q 2 P
P 2 O
Q 9 B
P 4 Y";
        
        [Fact]
        public void should_find_dijkstras_match_on_undirected_acyclic()
        {
            var graph = _acyclicGraph.AsWGraph<char>(char.Parse, int.Parse, false);
            var start = graph.Nodes['A'];
            var node = graph.Dijkstras(start, c => c == 'Z');

            Assert.NotNull(node);
            Assert.Equal('Z', node.Data);
        }

        [Fact]
        public void should_not_find_dijkstras_match_on_undirected_acyclic_if_none_exists()
        {
            var graph = _acyclicGraph.AsWGraph<char>(char.Parse, int.Parse, false);
            var start = graph.Nodes['A'];
            var node = graph.Dijkstras(start, c => c == 'Q');

            Assert.Null(node);
        }

        [Fact]
        public void should_find_dijkstras_match_on_undirected_cyclic()
        {
            var graph = _cyclicGraph.AsWGraph<char>(char.Parse, int.Parse, false);
            var start = graph.Nodes['A'];
            var node = graph.Dijkstras(start, c => c == 'Z');

            Assert.NotNull(node);
            Assert.Equal('Z', node.Data);
        }

        [Fact]
        public void should_not_find_dijkstras_match_on_undirected_cyclic_if_none_exists()
        {
            var graph = _cyclicGraph.AsWGraph<char>(char.Parse, int.Parse, false);
            var start = graph.Nodes['A'];
            var node = graph.Dijkstras(start, c => c == 'M');

            Assert.Null(node);
        }
    }
}