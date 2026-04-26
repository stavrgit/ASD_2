using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace ASD_2
{
    public class Node
    {
        public List<Edge> Edges = new List<Edge>();
        public int SuffixIndex = -1;


        public Edge FindEdge(char c)
        {
            foreach (var e in Edges)
                if (Program.GlobalText[e.Start] == c)
                    return e;
            return null;
        }
    }
    public class Edge
    {
        public int Start;
        public int End;
        public Node Child;

        public Edge(int s, int e, Node child)
        {
            Start = s;
            End = e;
            Child = child;
        }

    }
    public class CompressedSuffixTree
    {
        private readonly string text;
        public Node Root = new Node();

        public CompressedSuffixTree(string s)
        {
            text = s + "$";

            for (int i = 0; i < text.Length; i++)
                InsertSuffix(i);   

        }

        private void InsertSuffix(int startIndex)
        {
            Node current = Root;
            int i = startIndex;

            while (i < text.Length)
            {
                char c = text[i];
                Edge edge = current.FindEdge(c);

                if (edge == null)
                {
                    var leaf = new Node();
                    leaf.SuffixIndex = startIndex;

                    current.Edges.Add(new Edge(i, text.Length - 1, leaf));
                    return;
                }

                int k = edge.Start;
                int edgeEnd = edge.End;

                // Сравниваем символы на ребре 
                while (k <= edgeEnd && i < text.Length && text[k] == text[i])
                {
                    k++;
                    i++;
                }

                // Полное совпадение ребра — идём дальше 
                if (k > edgeEnd)
                {
                    current = edge.Child;
                    continue;
                }

                if (i == text.Length)
                {
                    edge.End = k - 1;
                    return;
                }

                // Частичное совпадение
                Node splitNode = new Node();

                Edge oldEdge = new Edge(k, edgeEnd, edge.Child);

                Node leafNode = new Node();
                leafNode.SuffixIndex = startIndex;

                Edge newEdge = new Edge(i, text.Length - 1, leafNode);

                edge.End = k - 1;
                edge.Child = splitNode;

                splitNode.Edges.Add(oldEdge);
                splitNode.Edges.Add(newEdge);

                return;
            }
        }


        public List<int> BuildSuffixArray()
        {
            var result = new List<int>();
            DFS(Root, result);
            return result;
        }

        private void DFS(Node node, List<int> result)
        {
            if (node.SuffixIndex != -1)
            {
                result.Add(node.SuffixIndex);
                return;
            }

            // сортируем ребра по первому символу
            node.Edges.Sort((a, b) => text[a.Start].CompareTo(text[b.Start]));

            foreach (var e in node.Edges)
                DFS(e.Child, result);
        }



        // статистика
        public (int branching, double avgDegree) ComputeStats()
        {
            int totalNodes = 0;
            int branching = 0;
            int totalDegree = 0;

            var stack = new Stack<Node>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                totalNodes++;

                int deg = node.Edges.Count;
                totalDegree += deg;

                if (deg >= 2)
                    branching++;

                foreach (var e in node.Edges)
                    stack.Push(e.Child);
            }

            double avg = totalNodes > 0 ? (double)totalDegree / totalNodes : 0;
            return (branching, avg);
        }
        public void Validate()
        {
            CheckEdges(Root);
            CheckLeafCount(); 
            CheckSuffixArrayCorrectness();
        }   

        // Проверка корректности ребер
        private void CheckEdges(Node node)
        {
            foreach (var e in node.Edges)
            {
                if (e.Start < 0 || e.End >= text.Length || e.Start > e.End)
                    throw new Exception($"Ошибка: некорректные индексы ребра ({e.Start}, {e.End}).");

                // Проверяем, что подстрока существует
                string edgeText = text.Substring(e.Start, e.End - e.Start + 1);
                if (edgeText.Length == 0)
                    throw new Exception("Ошибка: пустое ребро.");

                CheckEdges(e.Child);
            }
        }

        // Количество листьев = длина строки
        private void CheckLeafCount()
        {
            int leaves = CountLeaves(Root);
            if (leaves != text.Length)
                throw new Exception($"Ошибка: количество листьев {leaves}, должно быть {text.Length}.");
        }



        private int CountLeaves(Node node)
        {
            if (node.Edges.Count == 0)
                return 1;

            int sum = 0;
            foreach (var e in node.Edges)
                sum += CountLeaves(e.Child);

            return sum;
        }

        // Проверка суффиксного массива
        private void CheckSuffixArrayCorrectness()
        {
            var sa = BuildSuffixArray();

            var expected = new List<int>();
            for (int i = 0; i < text.Length; i++)
                expected.Add(i);

            expected.Sort((a, b) => string.CompareOrdinal(text.Substring(a), text.Substring(b)));

            for (int i = 0; i < sa.Count; i++)
            {
                if (sa[i] != expected[i])
                    throw new Exception("Ошибка: суффиксный массив, построенный по дереву, некорректен.");
            }
        }

    }
}
