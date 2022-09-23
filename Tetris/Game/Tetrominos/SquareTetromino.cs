using System.Drawing;

namespace Tetris.Game.Tetrominos
{
    public class SquareTetromino : ITetromino
    {
        public Matrix2D Matrix => Matrix2D.FromArray(new int[2, 2]
        {
            { 1, 1 },
            { 1, 1 }
        });

        public Color Color => Color.Yellow;
    }
}