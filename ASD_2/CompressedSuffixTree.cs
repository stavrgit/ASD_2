using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace ASD_2
{
    public class CompressedSuffixTree
    {
        private readonly string text;
        public Node Root;

        private readonly bool useArray;
        private readonly int alphabetSize;
        private readonly char baseChar;

        public CompressedSuffixTree(string s, bool useArray, int alphabetSize = 256, char baseChar = '\0')
        {
            this.useArray = useArray;
            this.alphabetSize = alphabetSize;
            this.baseChar = baseChar;

            text = s + "$";
            Program.GlobalText = text;

            Root = new Node(useArray, alphabetSize, baseChar);

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
                    // Создаем новый лист
                    var leaf = new Node(useArray, alphabetSize, baseChar);
                    leaf.SuffixIndex = startIndex;
                    current.AddEdge(new Edge(i, text.Length - 1, leaf));
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

                if (k > edgeEnd)
                {
                    // Полное совпадение ребра идем дальше
                    current = edge.Child;
                    continue;
                }

                // Частичное совпадение  делаем split
                if (i < text.Length)
                {
                    Node splitNode = new Node(useArray, alphabetSize, baseChar);
                    Edge remainingEdge = new Edge(k, edgeEnd, edge.Child);

                    // Новое ребро для текущего суффикса
                    Node leafNode = new Node(useArray, alphabetSize, baseChar);
                    leafNode.SuffixIndex = startIndex;
                    Edge newEdge = new Edge(i, text.Length - 1, leafNode);

                    // Обновляем текущее ребро
                    edge.End = k - 1;
                    edge.Child = splitNode;

                    // Добавляем оба ребра в splitNode
                    splitNode.AddEdge(remainingEdge);
                    splitNode.AddEdge(newEdge);
                }
                else
                {
                    Node splitNode = new Node(useArray, alphabetSize, baseChar);

                    Edge oldEdge = new Edge(k, edgeEnd, edge.Child);

                    Node leafNode = new Node(useArray, alphabetSize, baseChar);
                    leafNode.SuffixIndex = startIndex;

                    Edge newEdge = null;
                    if (i < text.Length)
                    {
                        newEdge = new Edge(i, text.Length - 1, leafNode);
                    }

                    edge.End = k - 1;
                    edge.Child = splitNode;
                    splitNode.AddEdge(oldEdge);

                    if (newEdge != null)
                        splitNode.AddEdge(newEdge);

                    return;
                }

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

            var edges = node.GetEdges().ToList();
            edges.Sort((a, b) => text[a.Start].CompareTo(text[b.Start]));

            foreach (var e in edges)
                DFS(e.Child, result);
        }

        public (int branching, double avgDegree) ComputeStats()
        {
            int totalNodes = 0;
            int branching = 0;
            int totalDegree = 0;

            bool isWorstCase = text.Distinct().Count() == 1;
            bool isBestCase = text.Distinct().Count() == text.Length;

            var queue = new Queue<Node>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                totalNodes++;

                var edges = node.GetEdges().ToList();
                int deg = edges.Count;
                totalDegree += deg;

                if (deg > 1)
                {
                    if (node == Root)
                    {
                        if (isBestCase)
                            branching++;
                    }
                    else
                    {
                        branching++;
                    }
                }

                foreach (var e in edges)
                    queue.Enqueue(e.Child);
            }

            double avgDegree = totalNodes > 0 ? (double)totalDegree / totalNodes : 0;
            return (branching, avgDegree);
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
            foreach (var e in node.GetEdges())
            {
                if (e.Start < 0 || e.End >= text.Length || e.Start > e.End)
                    throw new Exception($"Ошибка: некорректные индексы ребра ({e.Start}, {e.End}).");

                // Проверяем что подстрока существует
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
            int deg = node.Degree(); 

            if (deg == 0)
                return 1; 

            int sum = 0;
            foreach (var e in node.GetEdges()) 
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
