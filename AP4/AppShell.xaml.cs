namespace AP4
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(InscriptionView), typeof(InscriptionView));
            Routing.RegisterRoute(nameof(ConnectionView), typeof(ConnectionView));
        }
    }
}
