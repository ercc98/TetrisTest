using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

using Tetris.Scripts;

namespace Tetris
{
    public class GameLogic : GameLogicBase
    {
        #region private Variables
        Piece currentPiece;
        Piece nextPiece;
        GameState gamestate;
        UIDrawer uIDrawer;
        GameGridManager gameGridManager;
        bool gameOver = false;
        #endregion

        public GameLogic(IGameView view)
            : base(view)
        {
            Initialize();
            uIDrawer.DrawGameGrid();
            uIDrawer.DrawNextPiece(nextPiece);
        }

        public void Initialize()
        {
            TimerInterval = 500;
            gamestate = new GameState(0, 0, 1);
            gameGridManager = new GameGridManager(20, 10);
            uIDrawer = new UIDrawer(View, gameGridManager);
            nextPiece = new Piece((Shape)new Random().Next(1, 8));
            GetNewPiece();
        }

        private void GetNewPiece()
        {           
            currentPiece = nextPiece;
            currentPiece.Center = new Vector2Int(2, 0);
            nextPiece = new Piece((Shape)new Random().Next(1, 8));
            uIDrawer.DrawNextPiece(nextPiece);
            int addedLines = gameGridManager.CheckRows();
            gamestate.UpdateLines(addedLines);
        }
        
        #region events
        protected override void OnTimerTick()
        {
            if (gameOver)
            {
                uIDrawer.DrawGameOverEffect();
                return;
            }
            uIDrawer.DrawGameGrid();
            currentPiece.Down();
            if (!gameGridManager.IsValidInGrid(currentPiece))
            {
                currentPiece.Up();
                if (gameGridManager.IsValidInGrid(currentPiece))
                {
                    int addedScore = gameGridManager.UpdateGameGrid(currentPiece);
                    gamestate.UpdateScore(addedScore);
                    int addedLines = gameGridManager.CheckRows();
                    gamestate.UpdateLines(addedLines);
                    uIDrawer.DrawGameGrid();
                    TimerInterval = gamestate.CheckLevel(TimerInterval);
                    GetNewPiece();
                }
                else 
                {
                    gameOver = true;
                }
            }
            uIDrawer.DrawPiece(currentPiece);
            uIDrawer.DrawUI(gamestate);
        }
        
        protected override void OnKeyPressed(Keys key)
        {
            if (gameOver)
                return;
            switch (key)
            {
                case Keys.Down:
                    currentPiece.Down();
                    if (!gameGridManager.IsValidInGrid(currentPiece))
                        currentPiece.Up();
                    break;
                case Keys.Right:
                    currentPiece.Right();
                    if (!gameGridManager.IsValidInGrid(currentPiece))
                        currentPiece.Left();
                    break;
                case Keys.Left:
                    currentPiece.Left();
                    if (!gameGridManager.IsValidInGrid(currentPiece))
                        currentPiece.Right();
                    break;
                case Keys.Space:
                    currentPiece.Rotate();
                    if (!gameGridManager.IsValidInGrid(currentPiece))
                    {
                        currentPiece.Rotate();
                        currentPiece.Rotate();
                        currentPiece.Rotate();
                    }
                    break;
            }
            uIDrawer.DrawGameGrid();
            uIDrawer.DrawPiece(currentPiece);            
        }
        #endregion
    }
}
