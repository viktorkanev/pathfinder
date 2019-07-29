using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pathfinder
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                string file = args[0];
                Caverns cave = new Caverns(file);

                List<Node> path = cave.AStar();
                Console.WriteLine("\nRoute: ");
                foreach (Node n in path)
                {
                    Console.Write(n.ID + " ");
                }
                Console.WriteLine(String.Format("\nLength: " + "{0:0.00} units", cave.CalcLength(path)));

                //writes solution onto solution.csn
                using (StreamWriter sw = new StreamWriter(file + ".csn"))
                {
                    foreach (Node n in path)
                    {
                        sw.Write(n.ID + " ");
                    }
                    Console.WriteLine("\nSolution saved to {0}.csn!", file);
                }
            }
            catch (Exception e)
            {
                string file = args[0];
                Console.WriteLine("Path not found or file does not exist!");
                using (StreamWriter sw = new StreamWriter(file + ".csn"))
                {
                    sw.Write("0");
                    Console.WriteLine("\nSolution saved to  {0}.csn!", file);
                }
            }
        }
    }
}
