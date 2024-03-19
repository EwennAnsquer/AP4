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

    [ObservableProperty]
    ObservableCollection<Commande> productsCommande = new();

    [ObservableProperty]
    bool isProductsCommandeFill = false;

    [ObservableProperty]
    Product product;

    [ObservableProperty]
    double productsCommandTotalPrice = 0;

    [ObservableProperty]
    int productsCommandTotalPoints = 0;

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
        pro1.prix = 1;
        pro1.pointGagne = 1;

        Product pro2 = new();
        pro2.nom = "tacos2";
        pro2.imageUrl = "tacos.png";
        pro2.prix = 2;
        pro2.pointGagne = 2;

        Product pro3 = new();
        pro3.nom = "tacos3";
        pro3.imageUrl = "tacos.png";
        pro3.prix = 3;
        pro3.pointGagne = 3;

        Product pro4 = new();
        pro4.nom = "tacos4";
        pro4.imageUrl = "tacos.png";
        pro4.prix = 4;
        pro4.pointGagne = 4;

        Product pro5 = new();
        pro5.nom = "tacos5";
        pro5.imageUrl = "tacos.png";
        pro5.prix = 5;
        pro5.pointGagne = 5;

        Product pro6 = new();
        pro6.nom = "tacos6";
        pro6.imageUrl = "tacos.png";
        pro6.prix = 6;
        pro6.pointGagne = 6;

        Product pro7 = new();
        pro7.nom = "tacos7";
        pro7.imageUrl = "tacos.png";
        pro7.prix = 7;
        pro7.pointGagne = 7;

        Product pro8 = new();
        pro8.nom = "tacos8";
        pro8.imageUrl = "tacos.png";
        pro8.prix = 8;
        pro8.pointGagne = 8;

        Product pro9 = new();
        pro9.nom = "tacos9";
        pro9.imageUrl = "tacos.png";
        pro9.prix = 9;
        pro9.pointGagne = 9;

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

        //if(ProductsCommandTotalPrice == null && ProductsCommandTotalPoints == null) CalculTotalPriceProductsCommand();
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
        Product = p;
        await Shell.Current.GoToAsync(nameof(ProductPriceView), false);
    }

    [RelayCommand]
    void CalculTotalPriceProductsCommand()
    {
        double totalPrice = 0;

        foreach (Commande p in ProductsCommande)
        {
            totalPrice += p.product.prix;
        }

        ProductsCommandTotalPrice = totalPrice;

        if(ProductsCommandTotalPrice != 0 || ProductsCommandTotalPoints != 0) IsProductsCommandeFill = true;
    }

    [RelayCommand]
    async Task GoToAchatView()
    {
        await Shell.Current.GoToAsync(nameof(AchatView), false);
    }

    [RelayCommand]
    async Task GoToBackFromProductPriceView(string monnaie)
    {
        Commande c = new();
        c.product = Product;

        await Shell.Current.GoToAsync("///CommandeView", false);
        IsProductsCommandeFill = true;

        if(monnaie == "currency")
        {
            c.monnaie = Product.prix + "€";
            ProductsCommandTotalPrice += Product.prix;
        }else if (monnaie == "points")
        {
            c.monnaie = Product.pointGagne+" points";
            ProductsCommandTotalPoints += Product.pointGagne;
        }

        ProductsCommande.Add(c);

        Product = null;
    }

    [RelayCommand]
    async Task DeleteItem(Commande c)
    {
        int index = ProductsCommande.IndexOf(c);
        ProductsCommande.RemoveAt(index);

        if(ProductsCommande.Count==0) IsProductsCommandeFill=false;
    }

    [RelayCommand]
    async Task Pay()
    {
        await Shell.Current.GoToAsync("///CommandeView", false);

        IsProductsCommandeFill = false;

        ProductsCommande.Clear();

        ((AppShell)App.Current.MainPage).SwitchtoTab(0);
    }
}
