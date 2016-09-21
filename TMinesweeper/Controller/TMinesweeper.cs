using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;
using TMinesweeper.Model;
using TMinesweeper.View;
using Timer = System.Windows.Forms.Timer;

namespace TMinesweeper.Controller
{
    public class TMinesweeper
    {
        //our model
        private readonly MineField mineField;
        private readonly SoundThread soundPlayer;
        private readonly Timer timer;
        private int timerCounter;

        //our view
        private readonly MinesweeperWindow mainWindow;

        public TMinesweeper()
        {
            //initialize the model with 10 mines
            mineField = new MineField(10);
            soundPlayer = new SoundThread();

            //setup timer
            timer = new Timer();
            timer.Interval = 1000; //1 sec
            timer.Tick += TimerTick;
            timerCounter = 0;

            //intialize view
            mainWindow = new MinesweeperWindow();
            //set up event handling
            mainWindow.ButtonPressed += OnButtonClicked;

            //run the app
            mainWindow.ShowDialog();
        }

        public string GetTimeString()
        {
            //create time span from our counter
            TimeSpan time = TimeSpan.FromSeconds(timerCounter);

            //format that into a string
            string timeString = time.ToString(@"mm\:ss");

            //return it
            return timeString;
        }

        private void TimerTick(object  cvsender, EventArgs e)
        {
            timerCounter++;
            mainWindow.UpdateTimerLabel(GetTimeString());
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;

            //get the current button that was clicked
            var currentButton = (KeyValuePair<int, Button>)sender;
            //get the current space that was clicked
            var currentSpaceValue = mineField.Spaces[currentButton.Key];

            if (mouseEventArgs.Button == MouseButtons.Right)
            {
                mainWindow.FlagButton(currentButton, mineField);
            }
            else
            {
                //start timer
                timer.Start();

                //if it was a mine - BOOM!
                if (currentSpaceValue.Occupied)
                {
                    //GAME IS LOST STUFF HERE
                    timer.Stop();
                    mainWindow.Detonate(mineField);
                    mainWindow.ShowAll(mineField);
                    Thread soundThread = new Thread(soundPlayer.PlayBomb);
                    soundThread.IsBackground = true;
                    soundThread.Start();
                    MessageBox.Show("BOOOOM!");
                    soundThread.Abort();
                    mainWindow.Close();
                }

                if (mainWindow.DisableButton(currentSpaceValue, mineField))
                {
                    //GAME IS WON STUFF HERE
                    timer.Stop();
                    Thread soundThread = new Thread(soundPlayer.PlayGoodJob);
                    soundThread.IsBackground = true;
                    soundThread.Start();
                    MessageBox.Show("Good Job: " + GetTimeString());
                    soundThread.Abort();
                    mainWindow.Close();
                }
            }
        }
    }
}
