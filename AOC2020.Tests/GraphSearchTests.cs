using Xunit;
using AOC2020.Search;
using System;
using System.Linq;

namespace AOC2020.Tests
{
    public class GraphSearchTests
    {
        private const string _acyclicGraph = @"1: 2 3
2: 4 5
3: 6 7";

        private const string _cyclicGraph = @"1: 2 3
2: 4 5
3: 6 7
7: 1";

        [Fact]
        public void should_find_dfs_match_on_undirected_acyclic()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, false);
            var start = graph.Nodes.Values.First();

            var node = graph.DFS(start, i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_dfs_match_on_undirected_acyclic_if_none_exists()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, false);
            var start = graph.Nodes.Values.First();

            var node = graph.DFS(start, i => i == 8);

            Assert.Null(node);
        }

        [Fact]
        public void should_find_dfs_match_on_directed_acyclic()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, true);
            var start = graph.Nodes.Values.First();

            var node = graph.DFS(start, i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_dfs_match_on_directed_acyclic_if_none_exists()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, true);
            var start = graph.Nodes.Values.First();

            var node = graph.DFS(start, i => i == 8);

            Assert.Null(node);
        }

        [Fact]
        public void should_find_bfs_match_on_undirected_acyclic()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, false);
            var start = graph.Nodes.Values.First();

            var node = graph.BFS(start, i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_bfs_match_on_undirected_acyclic_if_none_exists()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, false);
            var start = graph.Nodes.Values.First();

            var node = graph.BFS(start, i => i == 8);

            Assert.Null(node);
        }

        [Fact]
        public void should_find_bfs_match_on_directed_acyclic()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, true);
            var start = graph.Nodes.Values.First();

            var node = graph.BFS(start, i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_bfs_match_on_directed_acyclic_if_none_exists()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, true);
            var start = graph.Nodes.Values.First();

            var node = graph.BFS(start, i => i == 8);

            Assert.Null(node);
        }

        [Fact]
        public void should_find_dijkstras_match_on_undirected_acyclic()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, false);
            var start = graph.Nodes.Values.First();

            var node = graph.Dijkstras(start, i => i, i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_dijkstras_match_on_undirected_acyclic_if_none_exists()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, false);
            var start = graph.Nodes.Values.First();

            var node = graph.Dijkstras(start, i => i, i => i == 8);

            Assert.Null(node);
        }

        [Fact]
        public void should_find_dijkstras_match_on_directed_acyclic()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, true);
            var start = graph.Nodes.Values.First();

            var node = graph.Dijkstras(start, i => i, i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_dijkstras_match_on_directed_acyclic_if_none_exists()
        {
            var graph = _acyclicGraph.AsGraph<int>(": ", int.Parse, true);
            var start = graph.Nodes.Values.First();

            var node = graph.Dijkstras(start, i => i, i => i == 8);

            Assert.Null(node);
        }
    }
}