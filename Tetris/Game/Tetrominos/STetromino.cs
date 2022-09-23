using System.Drawing;

namespace Tetris.Game.Tetrominos
{
    public class STetromino : ITetromino
    {
        public Matrix2D Matrix => Matrix2D.FromArray(new int[2, 3]
        {
            { 0, 1, 1 },
            { 1, 1, 0 }
        });

        public Color Color => Color.Green;
    }
}
