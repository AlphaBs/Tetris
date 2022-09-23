using System.Drawing;
using Tetris.Game.Tetrominos;

namespace Tetris.Game
{
    public class TetrisObject
    {
        public TetrisObject(ITetromino shape)
        {
            Shape = shape;
            Color = shape.Color;
        }

        public ITetromino Shape { get; }
        public Color Color { get; set; }
        public Point Position { get; set; }

        public Rectangle ToRectangle()
        {
            return new Rectangle(Position, Shape.Size);
        }
    }
}
