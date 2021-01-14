namespace LifeGame
{
    using SFML.Graphics;
    using SFML.System;

    /// <summary>
    /// Базовое приложение
    /// </summary>
    public abstract class SfmlApplication
    {
        private readonly RenderWindow _window;
        public SfmlApplication(string title, uint width = 800, uint height = 800)
        {
            _window = new RenderWindow(new SFML.Window.VideoMode(width, height), title, SFML.Window.Styles.Titlebar | SFML.Window.Styles.Close);
            _window.Closed += (s, e) =>
            {
                _window.Close();
            };
        }

        public void Run()
        {
            Clock clock = new Clock();
            while(_window.IsOpen)
            {
                _window.DispatchEvents();
                _window.Clear();
                Update(clock.Restart().AsSeconds());
                Draw(_window);
                _window.Display();
            }
        }

        protected abstract void Update(float elapsedTime);
        protected abstract void Draw(RenderWindow window);
    }
}
