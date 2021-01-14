namespace LifeGame
{
    using SFML.Graphics;
    using System;

    public class LifeGame : SfmlApplication
    {
        /// <summary>
        /// Название окна
        /// </summary>
        private const string _title = "Game of life";

        /// <summary>
        /// Ширина вселенной
        /// </summary>
        private const uint _width = 800;

        /// <summary>
        /// Высота "вселенной"
        /// </summary>
        private const uint _height = 600;

        /// <summary>
        /// РАНДОМ!!!!!!!!
        /// </summary>
        private Random _rnd = new Random();

        /// <summary>
        /// примитив sfml для рисования нашей "вселенной"
        /// </summary>
        private RectangleShape _worldShape = new RectangleShape(new SFML.System.Vector2f(_width, _height));
        
        /// <summary>
        /// картинка для отображения текушего состояния колонии
        /// </summary>
        private Image _image = new Image(_width, _height);
        /// <summary>
        /// Массив "клеток"
        /// true - живая клетка
        /// false - мертвая
        /// </summary>
        private bool[] _cells = new bool[_width * _height];
        /// <summary>
        /// Массив для хранения элементов следующего поколения
        /// </summary>
        private bool[] _nextCellsBuffer = new bool[_width * _height];
        public LifeGame() : base(_title, _width, _height)
        {
            _worldShape.Texture = new Texture(_image);
            InitRandom(10);
        }
        
        /// <summary>
        /// Метод вызываемый при отрисовке очередного кадра в базовом классе
        /// Базовый класс передает сюда окно, которое может быть использовано для отрисовки
        /// </summary>
        /// <param name="window"></param>
        protected override void Draw(RenderWindow window)
        {
            window.Draw(_worldShape);
        }

        /// <summary>
        /// Метод обновления состояния
        /// </summary>
        /// <param name="elapsedTime">Время прошедшее с прошлого обновления</param>
        protected override void Update(float elapsedTime)
        {
            //Т.к. клеток дохуя, не будем никак использовать время
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    var index = GetCellInex(i, j);
                    _nextCellsBuffer[index] = IsCellAlive(i, j);

                    if (_nextCellsBuffer[index])
                    {
                        _image.SetPixel((uint)j, (uint)i, Color.Green);
                    }
                    else
                    {
                        _image.SetPixel((uint)j, (uint)i, Color.Black);
                    }

                }
            }
            _worldShape.Texture.Update(_image);
            Array.Copy(_nextCellsBuffer, _cells, _nextCellsBuffer.Length);
        }

        /// <summary>
        /// Метод проверки может ли жить жить "клетка"
        /// </summary>
        private bool IsCellAlive(int i, int j)
        {
            var n = CountNeighbors(i, j);
            if (GetCell(i, j))
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

        /// <summary>
        /// Метод для получения клетки по строке - колонке, чтобы упростить обращение к одномерному массиву
        /// </summary>
        private bool GetCell(int row, int col) => _cells[GetCellInex(row, col)];

        /// <summary>
        /// Метод преобразования строки-колонки к индексу в массиве нашей "вселенной"
        /// </summary>
        private int GetCellInex(int row, int col) => (int)(row * _width + col);

        /// <summary>
        /// Метод инициализации массива клеток
        /// </summary>
        private void InitRandom(uint fillFactor)
        {
            if (fillFactor > 100) fillFactor = 100;
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = fillFactor - _rnd.Next(0, 100) >= 0;
        }

        /// <summary>
        /// Метод посчета соседей
        /// TODO в будующем заменить на что-то более производительное
        /// </summary>
        private int CountNeighbors(int row, int col)
        {
            //Если нужно сделать мир зацикленным - можно поменять правила тута
            int startRow = row - 1 < 0 ? 0 : row - 1;
            int endRow = row + 1 > _height - 1 ? (int)_height - 1 : row + 1;

            int startCol = col - 1 < 0 ? 0 : col - 1;
            int endCol = col + 1 > _width - 1 ? (int)_width - 1 : col + 1;

            int n = 0;
            for (int i = startRow; i <= endRow; i++)
            {
                for (int j = startCol; j <= endCol; j++)
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
