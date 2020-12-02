using System.Collections.Generic;

namespace AOC2020.Structure
{
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
}