namespace LifeGame
{
    using SFML.Graphics;
    using System;
    using System.Linq;

    public class LifeGame : SfmlApplication
    {
        private const string _title = "Game of life";
        private const uint width = 800;
        private const uint height = 800;
        private readonly Random rnd = new Random();
        private RectangleShape _worldShape = new RectangleShape(new SFML.System.Vector2f(width,height));
        private Image _image = new Image(width, height);
        private bool[] _cells = new bool[width * height];
        private bool[] _nextCellsBuffer = new bool[width * height];
        public LifeGame() : base(_title, width, height)
        {
            _worldShape.Texture = new Texture(_image);
            InitRandom(25);
        }



        protected override void Draw(RenderWindow window)
        {
            window.Draw(_worldShape);
        }

        protected override void Update(float elapsedTime)
        {
            //Т.к. клеток дохуя, не будем никак использовать время

            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    var index = GetCellInex(i, j);
                    _nextCellsBuffer[index] = LifeGameRulles(i, j);

                    if(_nextCellsBuffer[index])
                    {
                        _image.SetPixel((uint)i, (uint)j, Color.White);
                    }
                    else
                    {
                        _image.SetPixel((uint)i, (uint)j, Color.Black);
                    }

                }
            }

            _worldShape.Texture.Update(_image);
            Array.Copy(_nextCellsBuffer, _cells, _nextCellsBuffer.Length);
        }

        private bool LifeGameRulles(int i, int j)
        {
            var n = CountNeighbors(i, j);
            if(GetCell(i,j))
            {
                if (n == 2 || n == 3)
                    return true;
            }
            else
            {
                if (n == 3)
                    return true;
            }
            return false;
        }

        private bool GetCell(int row, int col) => _cells[GetCellInex(row, col)];
        private int GetCellInex(int row, int col) => (int)(row * width + col);
        private void InitRandom(uint fillFactor)
        {
            if (fillFactor > 100) fillFactor = 100;
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = fillFactor - rnd.Next(0, 100) >= 0;
        }
        private int CountNeighbors(int row, int col)
        {
            int startRow = row - 1 < 0 ? 0 : row - 1;
            int endRow = row + 1 > height - 1 ? (int)height - 1 : row + 1;

            int startCol = col - 1 < 0 ? 0 : col - 1;
            int endCol = col + 1 > width - 1 ? (int)width - 1 : col + 1;

            int n = 0;
            for(int i = startRow; i <= endRow; i++)
            {
                for(int j = startCol; j <= endCol; j++)
                {
                    if (i == row && j == col) continue;
                    if (GetCell(i, j))
                    {
                        n++;
                    }
                }
            }
            return n;                    
        }
    }
}
