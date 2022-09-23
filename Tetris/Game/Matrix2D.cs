namespace Tetris.Game
{
    public class Matrix2D
    {
        public static Matrix2D FromArray(bool[,] arr)
        {
            return FromArray(arr, v => v);
        }

        public static Matrix2D FromArray(int[,] arr)
        {
            return FromArray(arr, v => v != 0);
        }

        public static Matrix2D FromArray<T>(T[,] arr, Func<T, bool> checker)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr));
            if (arr.Length == 0)
                throw new ArgumentException("Array is empty", nameof(arr));

            int h = arr.GetLength(0);
            int w = arr.GetLength(1);

            var m = new Matrix2D(w, h);
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    m.Set(x, y, checker(arr[y, x]));
                }
            }

            return m;
        }

        public Matrix2D(int w, int h)
        {
            if (w < 0 || h < 0)
                throw new ArgumentOutOfRangeException();

            this.Width = w;
            this.Height = h;

            this.innerMatrix = new bool[Height, Width];
        }

        private readonly bool[,] innerMatrix;

        public int Width { get; }
        public int Height { get; }

        /// <summary>
        /// (x, y) 값을 가져옴. 0부터 시작
        /// </summary>
        public bool Get(int x, int y)
        {
            if (!checkBorder(x, y))
                throw new ArgumentOutOfRangeException();
            return innerMatrix[y, x];
        }

        /// <summary>
        /// (x, y) 값을 설정. 0부터 시작
        /// </summary>
        public void Set(int x, int y, bool value)
        {
            if (!checkBorder(x, y))
                throw new ArgumentOutOfRangeException();
            innerMatrix[y, x] = value;
        }
        
        /// <summary>
        /// 왼쪽 90도 회전
        /// </summary>
        public Matrix2D RotateToLeft()
        {
            var newMatrix = new Matrix2D(Height, Width);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var rotatedValue = this.Get(y, Width - x - 1);
                    newMatrix.Set(x, y, rotatedValue);
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// 오른쪽 90도 회전
        /// </summary>
        public Matrix2D RotateToRight()
        {
            var newMatrix = new Matrix2D(Height, Width);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var rotatedValue = this.Get(Height - y - 1, x);
                    newMatrix.Set(x, y, rotatedValue);
                }
            }

            return newMatrix;
        }

        private bool checkBorder(int x, int y)
        {
            return (x >= 0)
                && (y >= 0)
                && (x < Width)
                && (y < Height);
        }
    }
}
