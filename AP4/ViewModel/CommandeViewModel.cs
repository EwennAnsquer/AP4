using System.Collections.ObjectModel;

namespace AP4.ViewModel;

public partial class CommandeViewModel : BaseViewModel
{
    readonly CommandeService commandeService;
    [ObservableProperty]
    ObservableCollection<ProductCategorie> allProductCategorie = new();

    [ObservableProperty]
    ProductCategorie firstProductCategorie;

    [ObservableProperty]
    ObservableCollection<Product> displayProducts = new();
    public CommandeViewModel(CommandeService commandeService)
    {
        this.commandeService = commandeService;

        ProductCategorie p1 = new();
        p1.nom = "Tacos";
        p1.imageUrl = "tacos.png";
        ProductCategorie p2 = new();
        p2.nom = "Burger";
        p2.imageUrl = "tacos.png";
        ProductCategorie p3 = new();
        p3.nom = "Pizza";
        p3.imageUrl = "tacos.png";
        ProductCategorie p4 = new();
        p4.nom = "Crêpe";
        p4.imageUrl = "tacos.png";
        ProductCategorie p5 = new();
        p5.nom = "Salade";
        p5.imageUrl = "tacos.png";
        ProductCategorie p6 = new();
        p6.nom = "Dessert";
        p6.imageUrl = "tacos.png";

        AllProductCategorie.Add(p1);
        AllProductCategorie.Add(p2);
        AllProductCategorie.Add(p3);
        AllProductCategorie.Add(p4);
        AllProductCategorie.Add(p5);
        AllProductCategorie.Add(p6);

        Product pro1 = new();
        pro1.nom = "tacos1";
        pro1.imageUrl = "tacos.png";

        Product pro2 = new();
        pro2.nom = "tacos2";
        pro2.imageUrl = "tacos.png";

        Product pro3 = new();
        pro3.nom = "tacos3";
        pro3.imageUrl = "tacos.png";

        Product pro4 = new();
        pro4.nom = "tacos4";
        pro4.imageUrl = "tacos.png";

        Product pro5 = new();
        pro5.nom = "tacos5";
        pro5.imageUrl = "tacos.png";

        Product pro6 = new();
        pro6.nom = "tacos6";
        pro6.imageUrl = "tacos.png";

        Product pro7 = new();
        pro7.nom = "tacos7";
        pro7.imageUrl = "tacos.png";

        Product pro8 = new();
        pro8.nom = "tacos8";
        pro8.imageUrl = "tacos.png";

        Product pro9 = new();
        pro9.nom = "tacos9";
        pro9.imageUrl = "tacos.png";

        p1.products.Add(pro1);
        p1.products.Add(pro3);
        p1.products.Add(pro2);
        p1.products.Add(pro4);
        p1.products.Add(pro5);
        p1.products.Add(pro6);
        p1.products.Add(pro7);
        p1.products.Add(pro8);
        p2.products.Add(pro9);

        if (AllProductCategorie.Count > 0)
        {
            firstProductCategorie = AllProductCategorie[0];
            DisplayCategorie(FirstProductCategorie);
        }
    }

    [RelayCommand]
    void DisplayCategorie(ProductCategorie p)
    {
        //await Shell.Current.DisplayAlert("test", p.nom, "ok");
        DisplayProducts.Clear();
        foreach (Product product in p.products)
        {
            DisplayProducts.Add(product);
        }
    }

    [RelayCommand]
    async Task GoToProductPrice(Product p)
    {
        //await Shell.Current.DisplayAlert("test", p.nom, "ok");
        await Shell.Current.GoToAsync(nameof(ProductPriceView), true, new Dictionary<string, object>
        {
            {"Product", p }
        });
    }
}
