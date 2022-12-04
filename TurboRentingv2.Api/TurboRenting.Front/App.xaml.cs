namespace TurboRenting.Front
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();   
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int MinWidth = 1280;
            const int MinHeight = 720;

            window.MinimumWidth = MinWidth;
            window.MinimumHeight = MinHeight;

            return window;
        }
    }
}