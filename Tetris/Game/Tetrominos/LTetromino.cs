using System.Drawing;

namespace Tetris.Game.Tetrominos
{
    public class LTetromino : ITetromino
    {
        public Matrix2D Matrix => Matrix2D.FromArray(new int[3, 2]
        {
            { 1, 0 },
            { 1, 0 },
            { 1, 1 }
        });

        public Color Color => Color.Orange;
    }
}
