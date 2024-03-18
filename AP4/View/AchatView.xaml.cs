namespace AP4.View;

public partial class AchatView : ContentPage
{
	CommandeViewModel viewModel;
	public AchatView(CommandeViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}
}