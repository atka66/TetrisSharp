using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TetrisSharp.GameLogic;

namespace TetrisSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;
        private InputHandler inputHandler;
        private readonly DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new();
            timer.Interval = TimeSpan.FromMilliseconds(33); // ~30 FPS
            timer.Tick += (sender, e) => {
                game.Tick();
                HandleInput();
                Render();
            };
            StartGame();
        }

        private void StartGame()
        {
            game = new();
            inputHandler = new InputHandler(game);
            Render();
            timer.Start();
        }


        private void HandleInput()
        {
            inputHandler.HandleKey(Key.A, () => game.CurrentPiece.Move(-1, 0));
            inputHandler.HandleKey(Key.D, () => game.CurrentPiece.Move(1, 0));
            inputHandler.HandleKey(Key.Q, () => game.CurrentPiece.Figure = Util.RotateFigure(game.CurrentPiece.Figure, false));
            inputHandler.HandleKey(Key.E, () => game.CurrentPiece.Figure = Util.RotateFigure(game.CurrentPiece.Figure, true));
            inputHandler.HandleKey(Key.S, () => game.CurrentPiece.Move(0, 1));
        }

        private void Render()
        {
            if (!game.GameOver)
            {
                ClearAll();
                labelScore.Content = game.Points;
                RenderField();
                if (game.CurrentPiece != null)
                {
                    RenderCurrentPiece();
                }
                if (game.NextPiece != null)
                {
                    RenderNextPiece();
                }
            }
            else
            {
                timer.Stop();
                if (MessageBox.Show($"Your final score is: {game.Points}", "Game Over!") == MessageBoxResult.OK)
                {
                    StartGame();
                }
            }
        }

        private void ClearAll()
        {
            canvasField.Children.Clear();
            canvasNextPiece.Children.Clear();
        }

        private void RenderField()
        {
            int[,] fieldMap = game.Field.Map;
            for (int i = 0; i < fieldMap.GetLength(0); i++)
            {
                for (int j = 0; j < fieldMap.GetLength(1); j++)
                {
                    if (fieldMap[i,j] != 0)
                    {
                        RenderRectangle(canvasField, j, i, fieldMap[i,j]);
                    }
                }
            }
        }

        private void RenderCurrentPiece()
        {
            Piece currentPiece = game.CurrentPiece;
            currentPiece.Traverse((x, y, value) =>
            {
                if (value != 0)
                {
                    RenderRectangle(canvasField, x + currentPiece.X, y + currentPiece.Y, value);
                }
            });
        }

        private void RenderNextPiece()
        {
            game.NextPiece.Traverse((x, y, value) =>
            {
                if (value != 0)
                {
                    RenderRectangle(canvasNextPiece, x, y, value);
                }
            });
        }

        private void RenderRectangle(Canvas canvasField, int j, int i, int color)
        {
            Rectangle rectangle = new();
            rectangle.Width = 14;
            rectangle.Height = 14;
            rectangle.Margin = new Thickness((j * 16) + 1, (i * 16) + 1, 0, 0);
            Color rectangleColor = color switch
            {
                1 => Colors.Cyan,
                2 => Colors.Blue,
                3 => Colors.Orange,
                4 => Colors.Yellow,
                5 => Colors.Green,
                6 => Colors.Magenta,
                7 => Colors.Red,
                _ => Colors.Black
            };
            rectangle.Fill = new SolidColorBrush(rectangleColor);
            canvasField.Children.Add(rectangle);
        }
    }
}
