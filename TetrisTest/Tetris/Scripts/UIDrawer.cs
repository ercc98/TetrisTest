using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Scripts
{
    class UIDrawer
    {
        private readonly IGameView view;
        private readonly GameGridManager gameGridManager;
        bool flickerhelper;

        public UIDrawer(IGameView view, GameGridManager gameGridManager)
        {
            this.view = view;
            this.gameGridManager = gameGridManager;
            flickerhelper = false;
        }

        public void DrawUI(GameState gameState)
        {
            view.Lines = gameState.Lines;
            view.Score = gameState.Score;
            view.Level = gameState.Level;
        }

        public void DrawGameGrid()
        {
            for (var y = 0; y < gameGridManager.GameGrid.GetLength(0); ++y)
            {
                for (var x = 0; x < gameGridManager.GameGrid.GetLength(1); ++x)
                {
                    switch (gameGridManager.GameGrid[y, x])
                    {
                        case 0:
                            view.SetPixelMainArea(x, y, Color.Red);
                            break;
                        case 1:
                            view.SetPixelMainArea(x, y, Color.Blue);
                            break;
                    }
                }
            }
        }

        public void DrawNextPiece(Piece nextPiece)
        {
            DrawHelpArea();
            DrawPieceHelpArea(nextPiece);
        }

        private void DrawHelpArea()
        {
            for (var y = 0; y < 4; ++y)
            {
                for (var x = 0; x < 4; ++x)
                {
                    view.SetPixelHelpArea(x, y, Color.Red);
                }
            }
        }

        public void DrawPiece(Piece piece)
        {
            int[,] grid = piece.Grid;
            Vector2Int center = piece.Center;
            Color color = piece.Color;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 1)
                    {
                        view.SetPixelMainArea(center.X + i, center.Y + j, color);
                    }
                }
            }
        }

        private void DrawPieceHelpArea(Piece piece)
        {
            int[,] grid = piece.Grid;
            Vector2Int center = piece.Center;
            Color color = piece.Color;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 1)
                    {
                        view.SetPixelHelpArea(i, j, color);
                    }
                }
            }
        }

        public void DrawWhiteBoard()
        {
            for (var y = 0; y < gameGridManager.GameGrid.GetLength(0); ++y)
            {
                for (var x = 0; x < gameGridManager.GameGrid.GetLength(1); ++x)
                {
                    view.SetPixelMainArea(x, y, Color.White);
                }
            }
        }

        public void DrawGameOverEffect()
        {
            if (flickerhelper)
                DrawWhiteBoard();
            else
                DrawGameGrid();
            flickerhelper = !flickerhelper;
        }
    }
}
