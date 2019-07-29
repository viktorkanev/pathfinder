using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace pathfinder
{
    class Node
    {
        private int nodeID;
        private PointF coord;

        private List<Edge> passageways = new List<Edge>();

        public Node(int number, float xC, float yC)
        {
            nodeID = number;
            coord = new PointF(xC, yC);
        }

        public void Connn(Edge newEdge)
        {
            passageways.Add(newEdge);
        }

        public int ID { get { return this.nodeID; } }
        public PointF Coord { get { return this.coord; } }
        public List<Edge> PassageW { get { return this.passageways; } }

        public override string ToString()
        {
            return ID + " {" + coord.X + ", " + coord.Y + "} ";
        }
    }
}
