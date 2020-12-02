using System;
using System.Collections.Generic;

public static class Search 
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

public class Tree<T>
{
    public Tree(TreeNode<T> root)
    {
        Root = root;
    }
    public TreeNode<T> Root { get; private set; }
}

public class TreeNode<T>
{
    public TreeNode(T data)
    {
        Data = data;
    }
    public T Data { get; set; }
    public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();
    public TreeNode<T> Parent { get; set; }
}
