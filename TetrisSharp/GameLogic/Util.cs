using System;

namespace TetrisSharp.GameLogic
{
    class Util
    {
		public static int[,] RotateFigure(int[,] figure, bool right)
        {
			int[,] resultFigure = (int[,])figure.Clone();
			if (right)
			{
				for (int i = 0; i < figure.GetLength(0); i++)
				{
					for (int j = 0; j < figure.GetLength(1); j++)
					{
						resultFigure[i, j] = figure[figure.GetLength(0) - 1 - j, i];
					}
				}
			}
            else 
            {
                for (int i = 0; i < figure.GetLength(0); i++)
                {
                    for (int j = 0; j < figure.GetLength(1); j++)
                    {
						resultFigure[i, j] = figure[j, figure.GetLength(0) - 1 - i];
                    }
                }
            }
			return resultFigure;
		}
        public static Piece CreateRandomPiece()
        {
            Random random = new();
			switch (random.Next(7))
			{
				// I piece
				case 0:
					{
						return new(new int[,] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });
					}
				// J piece
				case 1:
					{
						return new(new int[,] { { 2, 0, 0 }, { 2, 2, 2 }, { 0, 0, 0 } });
					}
				// L piece
				case 2:
					{
						return new(new int[,] { { 0, 0, 3 }, { 3, 3, 3 }, { 0, 0, 0 } });
					}
				// O piece
				case 3:
					{
						return new(new int[,] { { 4, 4 }, { 4, 4 } });
					}
				// S piece
				case 4:
					{
						return new(new int[,] { { 0, 5, 5 }, { 5, 5, 0 }, { 0, 0, 0 } });
					}
				// T piece
				case 5:
					{
						return new(new int[,] { { 0, 6, 0 }, { 6, 6, 6 }, { 0, 0, 0 } });
					}
				// Z piece
				case 6:
					{
						return new(new int[,] { { 7, 7, 0 }, { 0, 7, 7 }, { 0, 0, 0 } });
					}
				default:
                    {
						return new(new int[,] { { } });
                    }
			}
        }
    }
}
