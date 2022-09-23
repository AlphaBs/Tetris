using System.Drawing;

namespace Tetris.Game.Tetrominos
{
    public class StraightTetromino : ITetromino
    {
        public Matrix2D Matrix => Matrix2D.FromArray(new int[4, 1]
        {
            { 1 },
            { 1 },
            { 1 },
            { 1 }
        });

        public Color Color => Color.Aqua;
    }
}
