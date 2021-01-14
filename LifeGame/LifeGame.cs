namespace LifeGame
{
    using SFML.Graphics;
    using System;

    public class LifeGame : SfmlApplication
    {
        private const string _title = "Game of life";
        private const uint width = 800;
        private const uint height = 800;
        private readonly Random rnd = new Random();
        private RectangleShape _worldShape = new RectangleShape(new SFML.System.Vector2f(width,height));
        private Image _image = new Image(width, height);
        private bool[] _cells = new bool[width * height];
        private bool[] _nextCellBuffer = new bool[width * height];
        public LifeGame() : base(_title, width, height)
        {
            _worldShape.Texture = new Texture(_image);
            InitRandom(95);
        }

        private void InitRandom(uint fillFactor)
        {
            if (fillFactor > 100) fillFactor = 100;
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = fillFactor - rnd.Next(0, 100) >= 0 ;
        }

        protected override void Draw(RenderWindow window)
        {
            window.Draw(_worldShape);
        }

        protected override void Update(float elapsedTime)
        {

            _worldShape.Texture.Update(_image);
        }
    }
}
