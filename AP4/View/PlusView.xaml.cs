namespace AP4.View;

public partial class PlusView : ContentPage
{
	PlusViewModel viewModel { get; set; }
	public PlusView(PlusViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		this.viewModel = viewModel;
	}
}