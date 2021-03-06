using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;

namespace AOC2020
{
    public static class StringExtensions
    {
        public static string[] PerNewLine(this string input)
        {
            return input.Split(Environment.NewLine);
        }

        public static string[] PerDoubleLine(this string input)
        {
            return input.Split(Environment.NewLine + Environment.NewLine);
        }

        public static (string, string) SplitInTwo(this string input, char delim)
        {
            var split = input.Split(delim);
            return (split[0], split[1]);
        }

        public static (string, string, string) SplitInThree(this string input, char delim)
        {
            var split = input.Split(delim);
            return (split[0], split[1], split[2]);
        }

        public static int[] AsIntArray(this string input)
        {
            return input.Split(Environment.NewLine).Select(int.Parse).ToArray();
        }

        public static string[] AsStringArray(this string input)
        {
            return input.Split(Environment.NewLine).ToArray();
        }

        public static char[] AsCharArray(this string input)
        {
            return input.Split(Environment.NewLine).Select(char.Parse).ToArray();
        }

        public static double[] AsDoubleArray(this string input)
        {
            return input.Split(Environment.NewLine).Select(double.Parse).ToArray();
        }

        public static long[] AsLongArray(this string input)
        {
            return input.Split(Environment.NewLine).Select(long.Parse).ToArray();
        }

        public static T[][] As2DArray<T>(this string input)
        {
            return input.Split(Environment.NewLine).Select(line => line.Select(c => Parse<T>(c.ToString())).ToArray()).ToArray();
        }

        public static Tree<T> AsTree<T>(this string input, string delim, Func<string, T> dataFn)
        {
            var nodeMap = new Dictionary<T, TreeNode<T>>();
            var lines = input.Split(Environment.NewLine);
            foreach (var line in lines) 
            {
                var split = line.Split(delim);
                var parentData = dataFn(split[0]);
                var childData = split[1].Split(' ').Select(dataFn);

                if (!nodeMap.ContainsKey(parentData))
                {
                    nodeMap[parentData] = new TreeNode<T>(parentData);
                }

                var parentNode = nodeMap[parentData];

                foreach (var childDatum in childData) 
                {
                    if (!nodeMap.ContainsKey(childDatum))
                    {
                        nodeMap[childDatum] = new TreeNode<T>(childDatum);
                    }

                    var childNode = nodeMap[childDatum];
                    childNode.Parent = parentNode;
                    parentNode.Children.Add(childNode);
                }
            }

            var roots = nodeMap.Values.Count(n => n.Parent == null);

            if (roots > 1) throw new Exception("Data does not represent tree (more than one root)");
            if (roots < 1) throw new Exception("Data represents tree with cycle");

            var root = nodeMap.Values.First(n => n.Parent == null);
            return new Tree<T>(root);
        }

        public static Graph<T> AsGraph<T>(this string input, string delim, Func<string, T> dataFn, bool directed = false)
        {
            var nodeMap = new Dictionary<T, GraphNode<T>>();
            var lines = input.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                var split = line.Split(delim);
                var domicileData = dataFn(split[0]);
                var neighborData = split[1].Split(' ').Select(dataFn);

                if (!nodeMap.ContainsKey(domicileData))
                {
                    nodeMap[domicileData] = new GraphNode<T>(domicileData);
                }

                var domicileNode = nodeMap[domicileData];

                foreach (var neighborDatum in neighborData) 
                {
                    if (!nodeMap.ContainsKey(neighborDatum))
                    {
                        nodeMap[neighborDatum] = new GraphNode<T>(neighborDatum);
                    }

                    var neighborNode = nodeMap[neighborDatum];
                    
                    domicileNode.Neighbors.Add(neighborNode);

                    if (!directed)
                    {
                        neighborNode.Neighbors.Add(domicileNode);
                    }
                }
            }

            return new Graph<T>(nodeMap.Values);
        }

        public static WGraph<T> AsWGraph<T>(this string input, Func<string, T> dataFn, Func<string, int> weightFn, bool directed = false)
        {
            var nodeMap = new Dictionary<T, WGraphNode<T>>();
            var lines = input.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                var fromData = dataFn(split[0]);
                var weight = weightFn(split[1]);
                var toData = dataFn(split[2]);

                if (!nodeMap.ContainsKey(fromData))
                {
                    nodeMap[fromData] = new WGraphNode<T>(fromData);
                }

                var fromNode = nodeMap[fromData];
                
                if (!nodeMap.ContainsKey(toData))
                {
                    nodeMap[toData] = new WGraphNode<T>(toData);
                }

                var toNode = nodeMap[toData];

                fromNode.Edges.Add(new WGraphEdge<T>(toNode, weight));

                if (!directed)
                {
                    toNode.Edges.Add(new WGraphEdge<T>(fromNode, weight));
                }
            }

            return new WGraph<T>(nodeMap.Values);
        }

        public static T1 Regex<T1>(this string input, string pattern)
        {
            var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
            return (T1)Parse<T1>(match.Groups[1].ToString());
        }

        public static (T1, T2) Regex<T1, T2>(this string input, string pattern)
        {
            var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
            return
            (
                (T1)Parse<T1>(match.Groups[1].ToString()),
                (T2)Parse<T2>(match.Groups[2].ToString())
            );
        }

        public static (T1, T2, T3) Regex<T1, T2, T3>(this string input, string pattern)
        {
            var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
            return
            (
                (T1)Parse<T1>(match.Groups[1].ToString()),
                (T2)Parse<T2>(match.Groups[2].ToString()),
                (T3)Parse<T3>(match.Groups[3].ToString())
            );
        }

        public static (T1, T2, T3, T4) Regex<T1, T2, T3, T4>(this string input, string pattern)
        {
            var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
            return
            (
                Parse<T1>(match.Groups[1].ToString()),
                Parse<T2>(match.Groups[2].ToString()),
                Parse<T3>(match.Groups[3].ToString()),
                Parse<T4>(match.Groups[4].ToString())
            );
        }

        
        public static (T1, T2, T3, T4, T5) Regex<T1, T2, T3, T4, T5>(this string input, string pattern)
        {
            var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
            return
            (
                Parse<T1>(match.Groups[1].ToString()),
                Parse<T2>(match.Groups[2].ToString()),
                Parse<T3>(match.Groups[3].ToString()),
                Parse<T4>(match.Groups[4].ToString()),
                Parse<T5>(match.Groups[5].ToString())
            );
        }

        private static TYPE Parse<TYPE>(string input)
        {
            object value;

            if (typeof(TYPE) == typeof(int))
            {
                value = int.Parse(input);
            }
            else if (typeof(TYPE) == typeof(long))
            {
                value = long.Parse(input);
            }
            else if (typeof(TYPE) == typeof(double))
            {
                value = double.Parse(input);
            }
            else if (typeof(TYPE) == typeof(string))
            {
                value = input;
            }
            else if (typeof(TYPE) == typeof(char))
            {
                value = char.Parse(input);
            }
            else if (typeof(TYPE) == typeof(ulong))
            {
                value = ulong.Parse(input);
            }
            else
            {
                throw new Exception("Trying to parse unexpected value type");
            }

            return (TYPE)value;
        }
    }
}