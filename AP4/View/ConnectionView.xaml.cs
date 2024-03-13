using Microsoft.Maui.Controls.PlatformConfiguration;

namespace AP4.View;

public partial class ConnectionView : ContentPage
{
	readonly ConnectionViewModel viewModel;

    public ConnectionView(ConnectionViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
        AppShell.SetTabBarIsVisible(this, false);
		AppShell.SetNavBarIsVisible(this, false);
    }
}