using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using TMinesweeper.View;

namespace TMinesweeper.Controller
{
    class SoundThread
    {
        private SoundPlayer player;
        
        // This method will be called when the thread is started. 
        public void PlayBomb()
        {
            player = new SoundPlayer(Resource1.bomb);
            player.PlaySync();
        }

        public void PlayGoodJob()
        {
            player = new SoundPlayer(Resource1.goodJob);
            player.PlaySync();
        }

        public void PlayWinning()
        {
            player = new SoundPlayer(Resource1.winning);
            player.PlaySync();
        }
        public void RequestStop()
        {
            _shouldStop = true;
            player.Stop();
        }
        // Volatile is used as hint to the compiler that this data 
        // member will be accessed by multiple threads. 
        private volatile bool _shouldStop;

    }
}
