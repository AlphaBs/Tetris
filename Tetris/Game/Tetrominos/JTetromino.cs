using System.Drawing;

namespace Tetris.Game.Tetrominos
{
    public class JTetromino : ITetromino
    {
        public Matrix2D Matrix => Matrix2D.FromArray(new int[3, 2]
        {
            { 0, 1 },
            { 0, 1 },
            { 1, 1 }
        });

        public Color Color => Color.Blue;
    }
}
