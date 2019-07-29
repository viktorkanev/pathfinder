using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pathfinder
{
    class Graph
    {
        private List<Node> nodes = new List<Node>();

        public void AddNode(Node newNode)
        {
            nodes.Add(newNode);
        }

        public void Conn(Node node1, Node node2)
        {
            Edge newEdge = new Edge(node1, node2);
            node1.Connn(newEdge);
            node2.Connn(newEdge);
        }

        public void AssignID(int node1, int node2)
        {
            Edge newEdge = new Edge(nodes[node1 - 1], nodes[node2 - 1]);
            nodes[node1 - 1].Connn(newEdge);
        }

        public List<Node> Nodes { get { return nodes; } }

        public override string ToString()
        {
            string str = "";
            foreach (Node n in nodes)
            {
                str += "Node " + n.ToString() + "\n";
            }

            return str;
        }
    }
}
