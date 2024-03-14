using System.Collections.ObjectModel;

namespace AP4.Model;

public partial class ProductCategorie : ObservableObject
{
    public string nom {  get; set; }
    public string imageUrl { get; set; }
    public ObservableCollection<Product> products { get; set; } = new();
}
