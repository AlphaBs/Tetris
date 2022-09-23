using System.Net.WebSockets;
using Tetris.Game;

namespace Tetris.Tests
{
    internal class Matrix2DTests
    {
        [Test]
        public void Test()
        {
            var matrix = new Matrix2D(3, 3);
            matrix.Set(0, 0, true);
            matrix.Set(1, 1, true);
            matrix.Set(2, 2, true);

            Assert.IsTrue(matrix.Get(0, 0));
            Assert.IsTrue(matrix.Get(1, 1));
            Assert.IsTrue(matrix.Get(2, 2));
        }

        [Test]
        public void TestOutOfBorder()
        {
            var matrix = new Matrix2D(3, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                matrix.Get(3, 3);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                matrix.Get(-1, -1);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                matrix.Set(3, 3, true);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                matrix.Set(-1, -1, false);
            });
        }

        [Test]
        public void TestFromArray()
        {
            var arr = new int[,]
            {
                {1,1,1 },
                {0,0,0 },
                {1,0,1 }
            };
            
            var matrix = Matrix2D.FromArray(arr);
            
            for (int y = 0; y < arr.GetLength(0); y++)
            {
                for (int x = 0; x < arr.GetLength(1); x++)
                {
                    Assert.That(matrix.Get(x, y), Is.EqualTo(arr[x, y] != 0));
                }
            }
        }

        [Test]
        public void TestRotate()
        { 
            
        }
    }
}
