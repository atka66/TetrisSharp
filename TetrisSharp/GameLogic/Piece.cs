using System;
using System.Diagnostics;

namespace TetrisSharp.GameLogic
{
    class Piece
    {
        public int[,] Figure { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Piece(int[,] figure)
        {
            Figure = figure;
            X = 4;
            Y = 0;
        }

        public void Move(int x, int y)
        {
            X += x;
            Y += y;
        }

        public delegate void TraverseDelegate(int x, int y, int value);

        public void Traverse(TraverseDelegate traverseDelegate)
        {
            for (int i = 0; i < Figure.GetLength(0); i++)
            {
                for (int j = 0; j < Figure.GetLength(1); j++)
                {
                    traverseDelegate(j, i, Figure[i,j]);
                }
            }
        }
    }
}
