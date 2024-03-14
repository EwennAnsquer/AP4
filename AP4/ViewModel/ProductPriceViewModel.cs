namespace AP4.ViewModel;

[QueryProperty(nameof(Product), "Product")]
public partial class ProductPriceViewModel : BaseViewModel
{
    [ObservableProperty]
    Product product;
    public ProductPriceViewModel() 
    {
        
    }
}
