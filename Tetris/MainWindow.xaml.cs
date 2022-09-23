using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tetris.Game;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _updateTimer;
        private readonly TetrisGame _game;

        public MainWindow()
        {
            InitializeComponent();

            _updateTimer = new DispatcherTimer();
            _updateTimer.Interval = TimeSpan.FromSeconds(1 / 10); // 10 FPS
            _updateTimer.Tick += Updater_Tick;
            _game = new TetrisGame(new TetrisGameRules
            {
                MapWidth = 10,
                MapHeight = 20
            });
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            initRenderer();
            _game.Init();
            _updateTimer.Start();
        }

        private void Updater_Tick(object? sender, EventArgs e)
        {
            _game.Update();

            if (_game.GameState == TetrisGameStates.GameOver)
            {
                _updateTimer.IsEnabled = false;
                MessageBox.Show("GAME OVER");
            }
            else
            {
                renderGame();
            }
        }
        
        private Brush? backgroundBrush;
        private Rectangle[,]? blocks;
        
        private void initRenderer()
        {
            gridMap.Children.Clear();
            
            gridMap.Rows = _game.Map.Height;
            gridMap.Columns = _game.Map.Width;
            
            backgroundBrush = new SolidColorBrush(Colors.Black);

            blocks = new Rectangle[_game.Map.Height, _game.Map.Width];
            for (int y = 0; y < _game.Map.Height; y++)
            {
                for (int x = 0; x < _game.Map.Width; x++)
                {
                    var block = createBlock(backgroundBrush);
                    gridMap.Children.Add(block);
                    blocks[y, x] = block;
                }
            }
        }

        private void renderGame()
        {
            if (blocks == null)
                throw new InvalidOperationException("call initRenderer() first");

            foreach (var item in blocks)
            {
                item.Fill = backgroundBrush;
            }

            foreach (var item in _game.Map.Objects)
            {
                var shape = item.Shape;
                for (int y = 0; y < shape.Matrix.Height; y++)
                {
                    for (int x = 0; x < shape.Matrix.Width; x++)
                    {
                        if (shape.Matrix.Get(x, y))
                        {
                            var realX = x + item.Position.X;
                            var realY = y + item.Position.Y;
                            
                            if (checkPositionInRenderBorder(realX, realY))
                                blocks[realY, realX].Fill = new SolidColorBrush(
                                    Color.FromRgb(item.Color.R, item.Color.G, item.Color.B));
                        }
                    }
                }
            }
        }

        private bool checkPositionInRenderBorder(int x, int y)
        {
            return (x >= 0)
                && (y >= 0)
                && (x < blocks.GetLength(1))
                && (y < blocks.GetLength(0));
        }

        private Rectangle createBlock(Brush brush)
        {
            var rec = new Rectangle();
            rec.Stroke = new SolidColorBrush(Colors.White);
            rec.Fill = brush;
            return rec;
        }
        
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.Key == Key.A)
            {
                _game.MoveLeft();
            }
            else if (e.Key == Key.D)
            {
                _game.MoveRight();
            }
            else if (e.Key == Key.S)
            {
                _game.MoveDown();
            }
            else if (e.Key == Key.Space)
            {
                _game.Drop();
            }
            else if (e.Key == Key.W)
            {
                _game.RotateToRight();
            }
        }
    }
}
