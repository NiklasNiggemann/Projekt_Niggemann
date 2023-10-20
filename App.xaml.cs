using Mensa_App.Classes.View;

namespace Mensa_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MenuView();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 600;
            const int newHeight = 800;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}