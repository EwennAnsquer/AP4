namespace AP4.View;

public partial class InscriptionView : ContentPage
{
    readonly InscriptionViewModel viewModel;
    public InscriptionView(InscriptionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
        AppShell.SetTabBarIsVisible(this, false);
        AppShell.SetNavBarIsVisible(this, false);
    }
}