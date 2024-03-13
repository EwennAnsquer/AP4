namespace AP4.View;

public partial class CommandeView : ContentPage
{
	readonly CommandeViewModel viewModel;
	public CommandeView(CommandeViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
    }
}