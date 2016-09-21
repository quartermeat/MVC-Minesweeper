using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TMinesweeper.Model;

namespace TMinesweeper.View
{

    public partial class MinesweeperWindow : Form
    {
        private Dictionary<int, Button> buttonMap;
        private const int WIDTH = 10;
        private const int HEIGHT = 10;

        public event EventHandler ButtonPressed;
        
        //constructor
        public MinesweeperWindow()
        {
            InitializeComponent();
            CustomInitialization();
        }

        //do our custom intializations of the main window
        private void CustomInitialization()
        {
            //custom window attributes done outside of designer
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            //////////////////////////////////////////////////

            //create a map of buttons//////////////////////
            buttonMap = new Dictionary<int, Button>();

            int index = 0;
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    Button newButton = new Button()
                    {
                        Width = 50,
                        Height = 50,
                        Margin = new Padding(0),
                        BackColor = Color.DarkOliveGreen
                    };
                    newButton.MouseDown += OnMouseClicked;
                    buttonMap.Add(index, newButton);
                    index++;
                }
            }

            //add each button to the layout
            foreach (KeyValuePair<int, Button> currentButton in buttonMap)
            {
                mainPanel.Controls.Add(currentButton.Value);
            }
            /////////////////////////////////////////////////
        }

        //flag a button on a right mouse click
        public void FlagButton(KeyValuePair<int, Button> button, MineField mineField)
        {
            Button currentButton = button.Value;
            
            //get the space we are working with
            Space space = new Space();
            foreach (KeyValuePair<int, Space> currentSpace in mineField.Spaces)
            {
                if (currentSpace.Key == button.Key)
                {
                    space = currentSpace.Value;
                }
            }

            if (space.Flagged)
            {
                currentButton.Image = null;
                space.Flagged = false;
                mineField.ReturnFlag();
            }
            else
            {
                if (mineField.GetFlag() >= 1)
                {
                    currentButton.Image = Resource1.Flag;
                    space.Flagged = true;
                }
            }

        }

        //update timer label
        public void UpdateTimerLabel(string timeString)
        {
            //update label with that string
            timerUpdatedLabel.Text = timeString;
        }

        //cheat code to see everything in field
        public void ShowAll(MineField mineField)
        {
            foreach (KeyValuePair<int, Space> currentSpace in mineField.Spaces)
            {
                if (currentSpace.Value.Occupied)
                {
                    buttonMap[currentSpace.Key].Text = "*";
                }
                else if (currentSpace.Value.GetMineCount() != 0)
                {
                    buttonMap[currentSpace.Key].Text = currentSpace.Value.GetMineCount().ToString();
                }

                buttonMap[currentSpace.Key].Enabled = false;
            }
        }

        //handle a button being left clicked (MAIN LOGIC)
        public bool DisableButton(Space selectedSpace, MineField mineField)
        {
            foreach (KeyValuePair<int, Button> currentButton in buttonMap)
            {
                if (currentButton.Key == selectedSpace.FieldIndex)
                {
                    currentButton.Value.Enabled = false;
                    currentButton.Value.BackColor = Color.Chocolate;
                    mineField.MoveCloserToWinCondition();
                    ClearZeroMineCounts(selectedSpace, mineField);
                    if (selectedSpace.GetMineCount() != 0)
                    {
                        currentButton.Value.Text = selectedSpace.GetMineCount().ToString();
                    }

                }
            }

            return mineField.winCondition;
        }

        //recursively clear buttons that contain minefield spaces with a zero mineCount
        public void ClearZeroMineCounts(Space currentSpace, MineField mineField)
        {
            if (currentSpace.GetMineCount() == 0)
            {
                foreach (KeyValuePair<int, Point> location in currentSpace.parameter)
                {
                    KeyValuePair<int, Space> adjacentSpace = mineField.GetSpace(location.Value);
                    if (adjacentSpace.Value != null && buttonMap[adjacentSpace.Key].Enabled)
                    {
                        buttonMap[adjacentSpace.Key].Enabled = false;
                        buttonMap[adjacentSpace.Key].BackColor = Color.Chocolate;
                        mineField.MoveCloserToWinCondition();
                        if (adjacentSpace.Value.GetMineCount() != 0)
                        {
                            buttonMap[adjacentSpace.Key].Text = adjacentSpace.Value.GetMineCount().ToString();
                        }
                        if (adjacentSpace.Value.GetMineCount() == 0)
                        {
                            //recursive call
                            ClearZeroMineCounts(adjacentSpace.Value, mineField);
                        }
                    }
                }
            }
        }



        public void Detonate(MineField mineField)
        {
            foreach (KeyValuePair<int, Button> button in buttonMap)
            {
                if (mineField.Spaces[button.Key].Occupied)
                {
                    button.Value.BackColor = Color.Red;
                }


            }
        }

        public void OnMouseClicked(object sender, MouseEventArgs e)
        {
            Button pressedButton = sender as Button;

            foreach (KeyValuePair<int, Button> button in buttonMap)
            {
                if (button.Value.Equals(pressedButton))
                {
                    ButtonPressed(button, e);
                }

            }
        }

    }
}
