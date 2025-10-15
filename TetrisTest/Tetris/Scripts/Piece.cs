using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Scripts
{
    class Piece: IRotable, IMovable
    {
        public int[,] Grid { get; private set; }
        public Color Color { get; private set; }

        private Vector2Int center;
        public Vector2Int Center
        {
            get { return center; }
            set { center = value; }
        }

        public Piece(Shape shape)
        {
            Color = new();
            Center = new(0, 0);
            Initialize(shape);
        }

        private void Initialize(Shape shape)
        {
            int n = 4;
            Grid = new int[n, n];
            Center = new Vector2Int(1, 2);
            switch (shape)
            {
                case Shape.O:
                    Color = Color.Green;
                    Grid[1, 1] = 1;
                    Grid[2, 1] = 1;
                    Grid[1, 2] = 1;
                    Grid[2, 2] = 1;
                    break;
                case Shape.I:
                    Color = Color.Blue;
                    Grid[0, 1] = 1;
                    Grid[1, 1] = 1;
                    Grid[2, 1] = 1;
                    Grid[3, 1] = 1;
                    break;
                case Shape.S:
                    Color = Color.Fuchsia;
                    Grid[1, 2] = 1;
                    Grid[2, 2] = 1;
                    Grid[2, 1] = 1;
                    Grid[3, 1] = 1;
                    break;
                case Shape.Z:
                    Color = Color.Cyan;
                    Grid[1, 1] = 1;
                    Grid[2, 1] = 1;
                    Grid[2, 2] = 1;
                    Grid[3, 2] = 1;
                    break;
                case Shape.L:
                    Color = Color.LightSkyBlue;
                    Grid[1, 2] = 1;
                    Grid[1, 1] = 1;
                    Grid[2, 1] = 1;
                    Grid[3, 1] = 1;
                    break;
                case Shape.J:
                    Color = Color.Beige;
                    Grid[3, 2] = 1;
                    Grid[1, 1] = 1;
                    Grid[2, 1] = 1;
                    Grid[3, 1] = 1;
                    break;
                case Shape.T:
                    Color = Color.BlueViolet;
                    Grid[1, 1] = 1;
                    Grid[2, 1] = 1;
                    Grid[3, 1] = 1;
                    Grid[2, 2] = 1;
                    break;
            }
        }
        public void Down()
        {
            center.Y += 1;
        }

        public void Up()
        {
            center.Y -= 1;
        }

        public void Left()
        {
            center.X -= 1;
        }

        public void Right()
        {
            center.X += 1;
        }

        public void Rotate()
        {
            int n = 4;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int temp = Grid[i,j];
                    Grid[i, j] = Grid[j,i];
                    Grid[j,i] = temp;
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    int temp = Grid[i, j];
                    Grid[i, j] = Grid[i,(n - 1) - j];
                    Grid[i,(n - 1) - j] = temp;
                }
            }
        }
    }
}
