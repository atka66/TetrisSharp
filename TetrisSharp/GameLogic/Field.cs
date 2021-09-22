namespace TetrisSharp.GameLogic
{
    class Field
    {
        public int[,] Map { get; }

        public Field()
        {
            Map = new int[20, 10];
        }
        public bool IsBadSpawn(Piece piece)
        {
            bool result = false;
            piece.Traverse((x, y, value) =>
            {
                if (value != 0 && Map[piece.Y + y, piece.X + x] != 0)
                {
                    result = true;
                }
            });
            return result;
        }

        public bool CanPiecePerformAction(Piece piece, PieceAction action)
        {
            int xx = 0;
            int yy = 0;
            int[,] tempFigure = (int[,])piece.Figure.Clone();
            switch (action)
            {
                case PieceAction.DOWN:
                    {
                        yy = 1;
                        break;
                    }
                case PieceAction.LEFT:
                    {
                        xx = -1;
                        break;
                    }
                case PieceAction.RIGHT:
                    {
                        xx = 1;
                        break;
                    }
                case PieceAction.ROTATE_LEFT:
                    {
                        tempFigure = Util.RotateFigure(piece.Figure, false);
                        break;
                    }
                case PieceAction.ROTATE_RIGHT:
                    {
                        tempFigure = Util.RotateFigure(piece.Figure, true);
                        break;
                    }
            }

            bool result = true;
            for (int i = 0; i < tempFigure.GetLength(0); i++)
            {
                for (int j = 0; j < tempFigure.GetLength(1); j++)
                {
                    if (tempFigure[i, j] != 0)
                    {
                        int checkingX = piece.X + xx + j;
                        int checkingY = piece.Y + yy + i;
                        if (checkingX < 0 || checkingX >= Map.GetLength(1) || checkingY < 0 || checkingY >= Map.GetLength(0) || Map[checkingY, checkingX] != 0)
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        public int SettlePiece(Piece piece)
        {

            piece.Traverse((x, y, value) =>
            {
                if (value != 0)
                {
                    Map[piece.Y + y, piece.X + x] = value;
                }
            });

            int rowsFull = 0;
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                if (IsRowFull(i))
                {
                    rowsFull++;
                    for (int ii = i; ii >= 1; ii--)
                    {
                        for (int j = 0; j < Map.GetLength(1); j++)
                        {
                            Map[ii,j] = Map[ii - 1,j];
                        }
                    }
                }
            }
            return rowsFull;
        }

        private bool IsRowFull(int currentRow)
        {
            for (int i = 0; i < Map.GetLength(1); i++)
            {
                if (Map[currentRow, i] == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
