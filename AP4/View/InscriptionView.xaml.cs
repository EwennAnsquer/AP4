namespace AP4.View;

public partial class InscriptionView : ContentPage
{
    readonly InscriptionViewModel viewModel;
    public InscriptionView(InscriptionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
        AppShell.SetTabBarIsVisible(this, false); //rend invisible la tab bar présente sur la page commande par exemple
        AppShell.SetNavBarIsVisible(this, false); //rend invisible la nav bar
    }
}