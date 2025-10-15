using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public interface IGameView
    {
        int Score { get; set; }
        int Level { get; set; }
        int Lines { get; set; }

        event KeyEventHandler KeyDown;

        void SetPixelMainArea(int x, int y, Color color);
        void SetPixelHelpArea(int x, int y, Color color);
    }
}
