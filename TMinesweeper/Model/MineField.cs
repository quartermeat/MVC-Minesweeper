using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using TMinesweeper.Helpers;

namespace TMinesweeper.Model
{
    public class MineField
    {
        private int WIDTH = 10;
        private int HEIGHT = 10;
        private int numMines;
        private int winConditionCounter;
        public bool winCondition;

        private Dictionary<int, Space> spaces;
        private int numFlags;

        public MineField(int NumOfMines)
        {
            Spaces = new Dictionary<int, Space>();
            numMines = NumOfMines;
            winConditionCounter = 100 - numMines;
            winCondition = false;
            numFlags = numMines;

            //come up with random spots to place our mines/////
            List<int> fieldIndexes = new List<int>(Enumerable.Range(0, 99));
            List<int> mineIndexes = new List<int>();
            fieldIndexes.Shuffle();
            for (int i = 0; i < numMines; i++)
            {
                //get the first 10 from the shuffled list
                mineIndexes.Add(fieldIndexes[i]);
            }
            ////////////////////////////////////////////////////
            int index = 0;
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    Point location = new Point(j, i);

                    //if this index is in our shuffled list lottery...
                    if (mineIndexes.Contains(index))
                    {
                        //add a loaded space
                        Space space = new Space(index, true, location);
                        Spaces.Add(index, space);
                    }
                    else
                    {
                        //add an empty space
                        Space space = new Space(index, false, location);
                        Spaces.Add(index, space);
                    }
                    index++;
                }
            }

            SetMineCounts();

        }



        public int GetFlag()
        {
            if (numFlags > 0)
            {
                return numFlags--;
            }
            return 0;
        }

        public void ReturnFlag()
        {
            numFlags++;
        }

        //getter and setter////////////////////////////
        public void MoveCloserToWinCondition()
        {
            winConditionCounter--;
            if (winConditionCounter == 0)
            {
                winCondition = true;
            }
            
        }

        public Dictionary<int, Space> Spaces
        {
            get { return spaces; }
            set { spaces = value; }
        }

        //get a space by location
        public KeyValuePair<int, Space> GetSpace(Point location)
        {
            foreach (KeyValuePair<int, Space> space in Spaces)
            {
                if (space.Value.FieldLocation == location)
                {
                    return space;
                }
            }
            //if this far, space does not exist at this location...

            return new KeyValuePair<int, Space>();
        }

        public void SetMineCounts()
        {
            foreach (KeyValuePair<int, Space> fieldSpace in spaces)
            {
                fieldSpace.Value.SetMineCount(this);
            }
        }

        /////////////////////////////////////////////
    }
}
