using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace pathfinder
{
    class Edge
    {
        private Node start;
        private Node end;
        private float weight;

        public Edge(Node edge1, Node edge2)
        {
            start = edge1;
            end = edge2;

            PointF startCoord = start.Coord;
            PointF endCoord = end.Coord;

            double X = Math.Pow((endCoord.X - startCoord.X), 2);
            double Y = Math.Pow((endCoord.Y - startCoord.Y), 2);

            weight = (float)Math.Sqrt(X + Y);
        }

        public float Weight { get { return weight; } }
        public Node End { get { return end; } }
        public Node Start { get { return start; } }

        public override string ToString()
        {
            return "Connectivity of Cavern " + start.ID + " to " + end.ID;
        }
    }
}
