using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Scripts
{
    class GameGridManager
    {
        private int[,] gameGrid;

        public int[,] GameGrid
        {
            get { return gameGrid; }
            private set { gameGrid = value; }
        }

        public GameGridManager(int X, int Y)
        {
            gameGrid = new int[X, Y];
        }

        public int UpdateGameGrid(Piece piece)
        {
            int[,] grid = piece.Grid;
            Vector2Int center = piece.Center;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 1)
                    {
                        gameGrid[center.Y + j, center.X + i] = 1;
                    }
                }
            }
            return 3;
        }

        public int CheckRows()
        {
            int rowsMultiplier = 0;
            for (var y = gameGrid.GetLength(0) - 1; y > 0; y--)
            {
                bool isFull = true;
                for (var x = 0; x < gameGrid.GetLength(1); x++)
                {
                    if (gameGrid[y, x] == 0)
                    {
                        isFull = false;
                        break;
                    }
                }
                if (isFull)
                {
                    DeleteRow(y);
                    y++;
                    rowsMultiplier++;
                }
            }
            return rowsMultiplier;
        }

        private void DeleteRow(int row)
        {
            for (var y = row; y > 0; --y)
            {
                for (var x = 0; x < gameGrid.GetLength(1); ++x)
                {
                    gameGrid[y, x] = gameGrid[y - 1, x];
                }
            }
            for (var x = 0; x < gameGrid.GetLength(1); ++x)
            {
                gameGrid[0, x] = 0;
            }
        }

        public bool IsValidInGrid(Piece piece)
        {
            int[,] grid = piece.Grid;
            Vector2Int center = piece.Center;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 1)
                    {
                        if (center.X + i < 0 ||
                            center.Y + i < 0 ||
                            center.X + i > gameGrid.GetLength(1) - 1 ||
                            center.Y + j >= gameGrid.GetLength(0) ||
                            gameGrid[center.Y + j, center.X + i] != 0)
                            return false;
                    }
                }
            }
            return true;
        }

    }
}
