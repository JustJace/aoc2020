using System;
using System.Collections.Generic;
using AOC2020.Structure;

namespace AOC2020.Search
{
    public static class TreeSearchExtensions
    {
        public static TreeNode<T> DFS<T>(this Tree<T> tree, Func<T, bool> searchFn)
        {
            var nodeStack = new Stack<TreeNode<T>>();
            nodeStack.Push(tree.Root);
            while (nodeStack.Count > 0)
            {
                var current = nodeStack.Pop();
                if (searchFn(current.Data))
                {
                    return current;
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        nodeStack.Push(child);
                    }
                }
            }

            return null;
        }

        public static TreeNode<T> BFS<T>(this Tree<T> tree, Func<T, bool> searchFn)
        {
            var nodeQueue = new Queue<TreeNode<T>>();
            nodeQueue.Enqueue(tree.Root);
            while (nodeQueue.Count > 0)
            {
                var current = nodeQueue.Dequeue();
                if (searchFn(current.Data))
                {
                    return current;
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        nodeQueue.Enqueue(child);
                    }
                }
            }

            return null;
        }

        public static TreeNode<T> Dijkstras<T>(this Tree<T> tree, Func<T, int> valueFn, Func<T, bool> searchFn)
        {
            var nodeQueue = new PriorityQueue<TreeNode<T>>(node => valueFn(node.Data));
            nodeQueue.Enqueue(tree.Root);
            while (nodeQueue.Count > 0)
            {
                var current = nodeQueue.Dequeue();
                if (searchFn(current.Data))
                {
                    return current;
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        nodeQueue.Enqueue(child);
                    }
                }
            }

            return null;
        }
    }
}