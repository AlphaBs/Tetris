using System.Drawing;
using Tetris.Game.Tetrominos;

namespace Tetris.Game
{
    public class TetrisGame
    {
        private readonly TetrisGameRules _rules;
        private readonly Random _random;

        public TetrisGame(TetrisGameRules rules)
        {
            this.GameState = TetrisGameStates.Idle;
            this.Map = new TetrisMap(rules.MapWidth, rules.MapHeight);
            this.Tetrominos = new ITetromino[]
            {
                new JTetromino(),
                new LTetromino(),
                new SquareTetromino(),
                new STetromino(),
                new StraightTetromino(),
                new TTetromino(),
                new ZTetromino(),
            };
            
            this._rules = rules;
            this._random = new Random();
        }
        
        public TetrisGameStates GameState { get; private set; }
        public ITetromino[] Tetrominos { get; }
        public TetrisMap Map { get; }

        private DateTimeOffset lastMoved;
        private TetrisObject? currentBlock;

        public void Init()
        {
            Map.Objects.Clear();
            currentBlock = null;
            lastMoved = DateTimeOffset.MinValue;
            this.GameState = TetrisGameStates.Playing;
        }

        // single thread only
        public void Update()
        {
            if (GameState == TetrisGameStates.GameOver)
                return;

            // create next block
            if (currentBlock == null)
            {
                currentBlock = generateNextBlock();
                if (!checkMovable(currentBlock, new Size(0, 0)))
                {
                    GameState = TetrisGameStates.GameOver;
                }

                Map.Objects.Add(currentBlock);
                lastMoved = DateTimeOffset.Now;
            }

            // drop block
            if (DateTimeOffset.Now - lastMoved > _rules.DropSpeed)
            {
                if (checkMovable(currentBlock, new Size(0, 1)))
                    currentBlock.Position = currentBlock.Position + new Size(0, 1);
                else
                    currentBlock = null;
                
                lastMoved = DateTimeOffset.Now;
            }
        }

        public void MoveLeft()
        {
            if (currentBlock == null)
                return;

            if (checkMovable(currentBlock, new Size(-1, 0)))
                currentBlock.Position = currentBlock.Position + new Size(-1, 0);
        }

        public void MoveRight()
        {
            if (currentBlock == null)
                return;

            if (checkMovable(currentBlock, new Size(1, 0)))
                currentBlock.Position = currentBlock.Position + new Size(1, 0);
        }

        public void MoveDown()
        {
            if (currentBlock == null)
                return;
            if (checkMovable(currentBlock, new Size(0, 1)))
                currentBlock.Position = currentBlock.Position + new Size(0, 1);
        }
        
        public void Drop()
        {
            if (currentBlock == null)
                return;

            while (checkMovable(currentBlock, new Size(0, 1)))
            {
                currentBlock.Position = currentBlock.Position + new Size(0, 1);
            }
        }

        public void RotateToLeft()
        {
            if (currentBlock == null)
                return;
        }

        public void RotateToRight()
        {
            if (currentBlock == null)
                return;
        }

        private bool checkMovable(TetrisObject block, Size to)
        {
            var nextPos = block.Position + to;

            // check collision: block-map
            var movable = (nextPos.X >= 0)
                       && (nextPos.Y >= 0)
                       && (nextPos.X <= Map.Width - block.Shape.Matrix.Width)
                       && (nextPos.Y <= Map.Height - block.Shape.Matrix.Height);

            if (!movable)
                return false;
            
            // collision: block-block
            foreach (var item in Map.Objects)
            {
                if (item == block)
                    continue;
                
                var r1 = new Rectangle(nextPos, block.Shape.Size);
                var r2 = item.ToRectangle();

                var left = Math.Max(r1.Left, r2.Left);
                var right = Math.Min(r1.Right, r2.Right);
                var top = Math.Max(r1.Top, r2.Top);
                var bottom = Math.Min(r1.Bottom, r2.Bottom);
                
                for (int y = 0; y < bottom-top; y++)
                {
                    for (int x = 0; x < right-left; x++)
                    {
                        if (block.Shape.Matrix.Get(x, y) && item.Shape.Matrix.Get(x, y))
                            return false;
                    }
                }
            }

            return true;
        }

        private TetrisObject generateNextBlock()
        {
            var index = _random.Next(0, Tetrominos.Length);
            var tetromino = Tetrominos[index];
            
            var block = new TetrisObject(tetromino);
            block.Position = new Point(_rules.MapWidth / 2, 0);
            return block;
        }
    }
}
