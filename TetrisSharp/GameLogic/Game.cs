namespace TetrisSharp.GameLogic
{
    class Game
    {
        public Field Field { get; }
        public Piece CurrentPiece { get; private set; }
        public Piece NextPiece { get; private set; }
        public int Points { get; private set; }
        public bool GameOver { get; private set; } = false;
        public int TickCount { get; private set; } = 0;

        public Game()
        {
            Field = new Field();
            NextPiece = Util.CreateRandomPiece();
        }

        public void Play()
        {
            while (!GameOver)
            {
                Tick();
            }
        }

        private void AccountRows(int rows)
        {
            switch (rows)
            {
                case 1:
                    {
                        Points += 40;
                        break;
                    }
                case 2:
                    {
                        Points += 100;
                        break;
                    }
                case 3:
                    {
                        Points += 300;
                        break;
                    }
                case 4:
                    {
                        Points += 1200;
                        break;
                    }
            }
        }

        public void Tick()
        {
            TickCount++;
            if (CurrentPiece == null)
            {
                CurrentPiece = new(NextPiece.Figure);
                GameOver = Field.IsBadSpawn(CurrentPiece);
                NextPiece = Util.CreateRandomPiece();
            }
            if (TickCount % 10 == 0)
            {
                if (Field.CanPiecePerformAction(CurrentPiece, PieceAction.DOWN))
                {
                    CurrentPiece.Move(0, 1);
                }
                else
                {
                    AccountRows(Field.SettlePiece(CurrentPiece));
                    CurrentPiece = null;
                }
            }
        }
    }
}
