using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class TetrisMap
    {
        public TetrisMap(int w, int h)
        {
            this.Width = w;
            this.Height = h;

            this.Objects = new List<TetrisObject>();
        }

        public int Width { get; }
        public int Height { get; }

        public List<TetrisObject> Objects { get; }
    }
}
