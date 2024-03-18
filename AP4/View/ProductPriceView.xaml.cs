namespace AP4.View;

public partial class ProductPriceView : ContentPage
{
	readonly CommandeViewModel viewModel;
	public ProductPriceView(CommandeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		this.viewModel = viewModel;
	}
}