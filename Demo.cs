using System;
using MapGen;

namespace Krampus12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Forrest Dungeon Generator v0.1 by Joey Honeycutt");

            // To generate a map, run 
            // MapGenerator.DrunkenBox(%Width%, %Height%, %FillTarget%)
            // or 
            // MapGenerator.DrunkenWalk(%StepTarget%)
            // Examples (Uncomment to run):
            // 
            //var map = MapGenerator.DrunkenBox(1000, 1000, 50);
            var map = MapGenerator.DrunkenWalk(1024);

            // Prints the finished product ////
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    switch (map[i, j])
                    {
                        case 0:
                            Console.Write("X");
                            break;
                        case 1:
                            Console.Write(" ");
                            break;
                    }
                }
                Console.WriteLine("\r");
            }
            Console.WriteLine();
        }
    }
}
