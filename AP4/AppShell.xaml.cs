namespace AP4
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(InscriptionView), typeof(InscriptionView));
            Routing.RegisterRoute(nameof(ConnectionView), typeof(ConnectionView));
            Routing.RegisterRoute(nameof(ProductPriceView), typeof(ProductPriceView));
            Routing.RegisterRoute(nameof(AchatView), typeof(AchatView));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(CommandeView), typeof(CommandeView));
        }

        public void SwitchtoTab(int tabIndex)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                switch (tabIndex)
                {
                    case 0: shelltabbar.CurrentItem = shelltab_0; break;
                    case 1: shelltabbar.CurrentItem = shelltab_1; break;
                    case 2: shelltabbar.CurrentItem = shelltab_2; break;
                };
            });
        }
    }
}
