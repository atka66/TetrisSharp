using System.Collections.Generic;
using System.Windows.Input;

namespace TetrisSharp.GameLogic
{
    class InputHandler
    {
        public delegate void KeyDelegate();

        private readonly Game game;
        private readonly IDictionary<Key, PieceAction> assignments;

        public InputHandler(Game game)
        {
            this.game = game;
            assignments = new Dictionary<Key, PieceAction>()
            {
                { Key.A, PieceAction.LEFT},
                { Key.D, PieceAction.RIGHT},
                { Key.Q, PieceAction.ROTATE_LEFT},
                { Key.E, PieceAction.ROTATE_RIGHT},
                { Key.S, PieceAction.DOWN},
            };
        }

        public void HandleKey(Key key, KeyDelegate keyDelegate)
        {
            if (!assignments.ContainsKey(key))
            {
                return;
            }

            if ((Keyboard.GetKeyStates(key) & KeyStates.Down) > 0
                && game.CurrentPiece != null
                && game.Field.CanPiecePerformAction(game.CurrentPiece, assignments[key]))
            {
                keyDelegate();
            }
        }
    }
}
