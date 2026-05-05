using System;
using System.Collections.Generic;

namespace ASD_2
{
    public class Node
    {
        private readonly bool useArray;
        private readonly int alphabetSize;
        private readonly char baseChar;

        public List<Edge> EdgesList;
        public Edge[] EdgesArray;

        public int SuffixIndex = -1;

        public Node(bool useArray, int alphabetSize = 128, char baseChar = '\0')
        {
            this.useArray = useArray;
            this.alphabetSize = alphabetSize;
            this.baseChar = baseChar;

            if (useArray)
                EdgesArray = new Edge[alphabetSize];
            else
                EdgesList = new List<Edge>();
        }

        public Edge FindEdge(char c)
        {
            if (useArray)
            {
                int idx = c - baseChar;
                return (idx >= 0 && idx < alphabetSize) ? EdgesArray[idx] : null;
            }
            else
            {
                foreach (var e in EdgesList)
                    if (Program.GlobalText[e.Start] == c)
                        return e;
                return null;
            }
        }

        public void AddEdge(Edge e)
        {
            if (useArray)
            {
                char c = Program.GlobalText[e.Start];
                int idx = c - baseChar;

               
                if (idx < 0 || idx >= alphabetSize)
                    throw new Exception($"Символ '{c}' вне диапазона алфавита");
                EdgesArray[idx] = e;

            }
            else
            {
                EdgesList.Add(e);
            }
        }

        public IEnumerable<Edge> GetEdges()
        {
            if (useArray)
            {
                foreach (var e in EdgesArray)
                    if (e != null)
                        yield return e;
            }
            else
            {
                foreach (var e in EdgesList)
                    yield return e;
            }
        }

        public int Degree()
        {
            if (useArray)
                return EdgesArray.Count(e => e != null);
            else
                return EdgesList.Count;
        }
    }
}
 