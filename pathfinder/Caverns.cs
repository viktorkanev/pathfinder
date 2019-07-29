using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace pathfinder
{
    class Caverns
    {
        private string file;
        private float cavCount;
        private Graph cavGraph;
        public string[] tokens;

        private float maxX = 0;
        private float maxY = 0;


        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        public Caverns(String filename)
        {
            file = filename + ".cav";

            cavGraph = new Graph();

            LoadFile();

            CreateGraph();
        }

        public float MaxX { get { return this.maxX; } }
        public float MaxY { get { return this.maxY; } }

        private void LoadFile()
        {
            StreamReader rdr;
            try
            {
               rdr = new StreamReader(file);

                cavCount = 0;

                using (rdr)
                {
                    string line;

                    while((line = rdr.ReadLine()) != null)
                    {
                        line = line.Trim();

                        tokens = Regex.Split(line, @",");

                        tokens = tokens.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        //gets the number of Caverns, the zero index of the .cav file
                        cavCount = float.Parse(tokens[0]);
                    }
                }
            
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file: " + file);
            }
            
        }

        private void CreateGraph()
        {
            int nodeId = 1;
            float maxX = 0, maxY = 0;

            for(int i = 1; i < tokens.Length - Math.Pow(cavCount, 2) - 1; i+=2)
            {
                float x = float.Parse(tokens[i]);
                float y = float.Parse(tokens[i + 1]);

                if (x > maxX)
                    maxX = x;

                if (y > maxY)
                    maxY = y;
                //creates new node and assigns ID and coordinates
                Node newNode = new Node(nodeId, x, y);
                nodeId++;
                cavGraph.AddNode(newNode);
            }
            //Displays coordinates of each node in console
            Console.WriteLine(cavGraph.ToString());

            int from = 1;
            int to = 1;

            for (int i = ((int)cavCount*2)+1; i < tokens.Length; i++)
            {
                if (tokens[i].Equals("1"))
                {
                    Console.WriteLine("Connectivity of cavern " + from + " and " + to);
                    cavGraph.AssignID(from, to);
                }

                from++;
                if(from>cavCount)
                {
                    from = 1;
                    to++;
                }
            }
        }

        public List<Node> AStar()
        {
            Node startNode = cavGraph.Nodes[0];
            Node endNode = cavGraph.Nodes[(int)cavCount - 1];

            List<Node> openSet = new List<Node>();
            List<Node> closedSet = new List<Node>();
            //adds the start node on top of the list
            openSet.Add(startNode);

            float currentC = float.PositiveInfinity;

            List<Node> currentPath = new List<Node>();
            currentPath.Add(startNode);
            Dictionary<Node, float> pathC = new Dictionary<Node, float>();
            //path from start is always 0
            pathC[startNode] = 0;

            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
            Dictionary<Node, float> totalC = new Dictionary<Node, float>();
            totalC[startNode] = Heuristic(startNode, endNode);

            while(openSet.Count != 0)
            {
                Node currentNode = null;
                currentC = float.PositiveInfinity;
                foreach(Node n in openSet)
                {
                    if(totalC[n] < currentC)
                    {
                        currentC = totalC[n];
                        currentNode = n;
                    }
                }
                
                if(currentNode == endNode)
                {
                    Console.WriteLine("Done!");
                    return Recalculate(cameFrom, currentNode);
                }

                List<Edge> neighbors = currentNode.PassageW;
                foreach(Edge e in neighbors)
                {
                    Node neighbor = e.End;

                    if (closedSet.Find(n => n.Equals(neighbor)) != null)
                    {
                        continue;
                    }

                    float newScore = pathC[currentNode] + e.Weight;

                    if (openSet.Find(n => n.Equals(neighbor)) == null)
                    {
                        openSet.Add(neighbor);
                    }
                    else if(newScore >= pathC[neighbor])
                    {
                        continue;
                    }

                    cameFrom[neighbor] = currentNode;
                    pathC[neighbor] = newScore;
                    totalC[neighbor] = pathC[neighbor] + Heuristic(neighbor, endNode);

                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
            }
            return null;
        }

        public float Heuristic(Node node1, Node node2)
        {
            PointF startCoord = node1.Coord;
            PointF endCoord = node2.Coord;

            double x = Math.Pow((endCoord.X - startCoord.X), 2);
            double y = Math.Pow((endCoord.Y - startCoord.Y), 2);
            

            return (float)Math.Sqrt(x + y);
        }

        public List<Node> Recalculate(Dictionary<Node, Node>path, Node currentNode)
        {
            List<Node> fPath = new List<Node>();

            if (!path.Keys.Contains(currentNode))
            {
                return new List<Node> { currentNode };
            }

            fPath = Recalculate(path, path[currentNode]);
            fPath.Add(currentNode);

            return fPath;
        }

        public double CalcLength(List<Node> path)
        {
            double len = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                len += Heuristic(path[i], path[i + 1]);
            }

            return len;
        }
    }
}
