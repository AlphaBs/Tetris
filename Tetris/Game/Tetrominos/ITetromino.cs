using System.Drawing;

namespace Tetris.Game.Tetrominos
{
    public interface ITetromino
    {
        Color Color { get; }
        Size Size => new Size(Matrix.Width, Matrix.Height);
        Matrix2D Matrix { get; }
    }
}
