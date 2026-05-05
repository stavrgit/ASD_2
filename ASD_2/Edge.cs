using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD_2
{
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
}
