using System;
using System.Collections.Generic;
using System.Drawing;

namespace TMinesweeper.Model
{
    public class Space
    {
        private const int N = 0;
        private const int NE = 1;
        private const int E = 2;
        private const int SE = 3;
        private const int S = 4;
        private const int SW = 5;
        private const int W = 6;
        private const int NW = 7;
        
        public bool Occupied { get; set; }
        public int FieldIndex { get; set; }
        private int mineCount;
        public Point FieldLocation;
        public Dictionary<int, Point> parameter;
        public bool Flagged;
        
        public Space(int index, Boolean newOccupied, Point location)
        {
            FieldIndex = index;
            Occupied = newOccupied;
            FieldLocation = location;
            Flagged = false;

            parameter = new Dictionary<int, Point>();
        }

        public Space()
        {
            
        }

        public void SetMineCount(MineField mineField)
        {
            // Get parameter from the mineField
            // How big of a parameter?
            const int distance = 1;
            //set the parameter
            SetParameter(distance);

            //List<Space> adjacentSpaces = new List<Space>();

            int mineCount = 0;
            foreach (KeyValuePair<int, Point> location in parameter)
            {
                KeyValuePair<int, Space> adjacentSpace = mineField.GetSpace(location.Value);
                if (adjacentSpace.Value != null)
                {
                    if (adjacentSpace.Value.Occupied)
                    {
                        mineCount++;
                    }
                }
            }
            this.mineCount = mineCount;
        }

        public int GetMineCount()
        {
            return mineCount;
        }

        private void SetParameter(int distance)
        {
            // Get North as a point and add it to the list
            Point north = Point.Add(FieldLocation, new Size(0, distance));
            parameter.Add(N, north);
            // Get NE as a point and add it to the list
            Point northEast = Point.Add(FieldLocation, new Size(distance, distance));
            parameter.Add(NE, northEast);
            // Get East as a point and add it to the list
            Point east = Point.Add(FieldLocation, new Size(distance, 0));
            parameter.Add(E, east);
            // Get SE as a point and add it to the list
            Point southEast = Point.Add(FieldLocation, new Size(distance, -distance));
            parameter.Add(SE, southEast);
            // Get South as a point and add it to the list
            Point south = Point.Add(FieldLocation, new Size(0, -distance));
            parameter.Add(S, south);
            // Get SW as a point and add it to the list
            Point southWest = Point.Add(FieldLocation, new Size(-distance, -distance));
            parameter.Add(SW, southWest);
            // Get West as a point and add it to the list
            Point west = Point.Add(FieldLocation, new Size(-distance, 0));
            parameter.Add(W, west);
            // Get NW as a point and add it to the list 
            Point northWest = Point.Add(FieldLocation, new Size(-distance, distance));
            parameter.Add(NW, northWest);
        }

    }
}
