using System.Drawing;

namespace Tetris.Game.Tetrominos
{
    public class ZTetromino : ITetromino
    {
        public Matrix2D Matrix => Matrix2D.FromArray(new int[2, 3]
        {
            { 1, 1, 0 },
            { 0, 1, 1 }
        });

        public Color Color => Color.IndianRed;
    }
}
