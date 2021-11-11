using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGen
{
    public class MapGenerator
    {
        public static int[,] DrunkenBox(int width, int height, int fillTarget)
        {
            // Initial Values
            float tilesCovered = 0;
            float mapCoverage = 0;

            // Initiate the map
            int[,] map = new int[width, height];
            // Console.WriteLine("Initiated...");

            // Get the starting position
            Random random = new Random();
            //int start_x = random.Next(width);
            //int start_y = random.Next(height);
            int start_x = width / 2;
            int start_y = height / 2;

            // Console.WriteLine("Generating {0} by {1} map, starting position {2}x, {3}y...", width, height, start_x, start_y);

            map[start_x, start_y] = 1;
            int x = start_x;
            int y = start_y;
            while (mapCoverage < fillTarget)
            {
                // Pick a random direction
                int direction = random.Next(4);

                // Take a step in that direction and change it from 0 to 1
                switch (direction)
                {
                    case 0: // north
                        int closeNorth = y;
                        int chanceNorth = random.Next(closeNorth);
                        if (chanceNorth + 10 < height && y + 2 < height)
                            //if (y + 2 < height)
                            y++;
                        map[x, y] = 1;
                        break;
                    case 1: // east
                        int closeEast = x;
                        int chanceEast = random.Next(closeEast);
                        if (chanceEast + 10 < width && x + 2 < width)
                            //if (x + 2 < width)
                            x++;
                        map[x, y] = 1;
                        break;
                    case 2: // south
                        int closeSouth = height - y;
                        int chanceSouth = random.Next(closeSouth);
                        if (chanceSouth - 10 < width && y - 1 > 0)
                            //if (y - 1 > 0)
                            y--;
                        map[x, y] = 1;
                        break;
                    case 3: // west
                        int closeWest = width - y;
                        int chanceWest = random.Next(closeWest);
                        if (chanceWest - 10 < width && x - 1 > 0)
                            //if (x - 1 > 0)
                            x--;
                        map[x, y] = 1;
                        break;
                }
                // Calculate progress
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                        if (map[i, j] == 1)
                        {
                            tilesCovered++;
                        }
                }
                float totalArea = width * height;
                mapCoverage = (tilesCovered / totalArea) * 100;

                //// Debug
                //Console.WriteLine("Current location: [{0},{1}] Total Coverage: {3} tiles, {2}%", x, y, mapCoverage, tilesCovered);
                ////

                // Reset progress for next loop
                tilesCovered = 0;
            }

            return map;
        }
        public static int[,] DrunkenWalk(int stepTarget)
        {
            // Initial Values   
            Random random = new Random();
            int tilesCovered = 0;

            // Initiate the map
            List<int[]> map = new List<int[]>();
            int[] startingCoords = { 0, 0 };
            map.Add(startingCoords);

            int x = 0;
            int y = 0;
            while (tilesCovered < stepTarget - 1)
            {
                // Pick a random direction
                int direction = random.Next(4);

                // Take a step in that direction
                switch (direction)
                {
                    case 0: // north
                        y++;
                        break;
                    case 1: // east
                        x++;
                        break;
                    case 2: // south
                        y--;
                        break;
                    case 3: // west
                        x--;
                        break;
                }

                // Save the new coordinates to an array of coordinates
                int[] newCoordinate = { x, y };
                bool addCoords = false;

                // Console.WriteLine("Trying " + newCoordinate[0] + " " + newCoordinate[1]);  //Debug

                foreach (var m in map)
                {
                    if (m[0] == newCoordinate[0] && m[1] == newCoordinate[1])
                    {
                        // Console.WriteLine("Duplicate coordinate: {0}, {1}", newCoordinate[0], newCoordinate[1]);  //Debug
                        addCoords = false;
                        break;
                    }
                    else
                    {
                        addCoords = true;
                    }
                }

                if (addCoords == true)
                {
                    // Console.WriteLine("Added new coordinate: {0}, {1}", newCoordinate[0], newCoordinate[1]);
                    map.Add(newCoordinate);
                    tilesCovered++;
                }
            }

            List<int> xCoords = new List<int>();
            List<int> yCoords = new List<int>();

            foreach (var m in map)
            {
                //Console.WriteLine(m[0].ToString() + " " + m[1].ToString());
                xCoords.Add(m[0]);
                yCoords.Add(m[1]);
            }

            // Get the minimim and maximum values of the X and Y coordinates
            int maxValueX = xCoords.Max();
            int minValueX = xCoords.Min();
            int maxValueY = yCoords.Max();
            int minValueY = yCoords.Min();
            //Console.WriteLine("Min/Max XY values: " + minValueX + " " + maxValueX + ", " + minValueY + " " + maxValueY);

            // Get the new grid width by adding the absoute values of the maximum X and Y coordinates
            int newGridWidth = Math.Abs(minValueX) + Math.Abs(maxValueX) + 1;
            int newGridHeight = Math.Abs(minValueY) + Math.Abs(maxValueY) + 1;
            //Console.WriteLine("Generating new 2D array {0}x{1}...", newGridWidth, newGridHeight);

            // Initialize the new map grid
            // +2 for border padding
            int[,] newMap = new int[newGridHeight + 2, newGridWidth + 2];

            // Go through and add the absulote value of the maximum negative number
            List<int> absoluteXCoords = new List<int>();
            foreach (int value in xCoords)
            {
                absoluteXCoords.Add(value + Math.Abs(minValueX) + 1);
                //Console.WriteLine("X {0} = {1}", value, (value + Math.Abs(minValueX) + 1));
            }
            List<int> absoluteYCoords = new List<int>();
            foreach (int value in yCoords)
            {
                absoluteYCoords.Add(value + Math.Abs(minValueY) + 1);
                //Console.WriteLine("Y: {0} = {1}", value, (value + Math.Abs(minValueY) + 1));
            }

            for (int i = 0; i < stepTarget; i++)
            {
                int newX = absoluteXCoords[i];
                int newY = absoluteYCoords[i];

                //Console.WriteLine(newX + ", " + newY);
                newMap[newY, newX] = 1;
            }

            return newMap;
        }
    }
}