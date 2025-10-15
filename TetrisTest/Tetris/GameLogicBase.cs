using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Tetris
{
    public abstract class GameLogicBase
    {
        private Timer timer;

        protected double TimerInterval
        {
            get => timer.Interval;
            set => timer.Interval = value;
        }

        public IGameView View { get; }

        protected GameLogicBase(IGameView view)
        {
            View = view;
            View.KeyDown += OnKeyDown;

            timer = new Timer();
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        protected abstract void OnTimerTick();
        protected abstract void OnKeyPressed(Keys key);

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyPressed(e.KeyCode);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnTimerTick();
        }
    }
}
