using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Scripts
{
    class GameState
    {
        public int Score { get; private set; }
        public int Lines { get; private set; }
        public int Level { get; private set; }

        private int maxLevel;
        private int scoreToLevelUp;

        public GameState(int score = 0, int lines = 0, int level = 1)
        {
            Score = score;
            Lines = lines;
            Level = level;
            maxLevel = 10;
            scoreToLevelUp = 1;
        }
        public double CheckLevel(double TimerInterval)
        {
            double newTimerInterval = TimerInterval;
            if ((Score / scoreToLevelUp) + 1  > Level && Level < maxLevel)
                newTimerInterval = 500 - (Level++ * 50);
            return newTimerInterval;
        }

        public void UpdateScore(int score)
        {
            Score += score;
        }

        public void UpdateLines(int lines)
        {
            Lines += lines;
            UpdateScore(100 * lines);
        }
    }
}
