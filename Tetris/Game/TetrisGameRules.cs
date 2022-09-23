using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class TetrisGameRules
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public TimeSpan DropSpeed { get; set; } = TimeSpan.FromSeconds(1);
        
    }
}
