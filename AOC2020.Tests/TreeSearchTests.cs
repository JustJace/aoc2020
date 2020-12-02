using System;
using AOC2020.Structure;
using Xunit;
using AOC2020.Search;

namespace AOC2020.Tests
{
    public class TreeSearchTests
    {
            private const string _treeString = @"1: 2 3
2: 4 5
3: 6 7";

        [Fact]
        public void should_find_dfs_match_if_exists()
        {
            var tree = _treeString.AsTree<int>(": ", int.Parse);

            var node = tree.DFS(i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_dfs_match_if_none_exists()
        {
            var tree = _treeString.AsTree<int>(": ", int.Parse);

            var node = tree.DFS(i => i == 8);

            Assert.Null(node);
        }

        [Fact]
        public void should_find_bfs_match_if_exists()
        {
            var tree = _treeString.AsTree<int>(": ", int.Parse);

            var node = tree.BFS(i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_bfs_match_if_none_exists()
        {
            var tree = _treeString.AsTree<int>(": ", int.Parse);

            var node = tree.BFS(i => i == 8);

            Assert.Null(node);
        }

        [Fact]
        public void should_find_djikstras_match_if_exists()
        {
            var tree = _treeString.AsTree<int>(": ", int.Parse);

            var node = tree.Dijkstras(i => i, i => i == 6);

            Assert.NotNull(node);
            Assert.Equal(6, node.Data);
        }

        [Fact]
        public void should_not_find_djikstras_match_if_none_exists()
        {
            var tree = _treeString.AsTree<int>(": ", int.Parse);

            var node = tree.Dijkstras(i => i, i => i == 8);

            Assert.Null(node);
        }
    }
}