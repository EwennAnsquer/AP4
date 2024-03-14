namespace AP4.View;

public partial class ProductPriceView : ContentPage
{
	readonly ProductPriceViewModel viewModel;
	public ProductPriceView(ProductPriceViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		this.viewModel = viewModel;
	}
}