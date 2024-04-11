namespace AP4.ViewModel;

public partial class CommandeViewModel : BaseViewModel
{
    CommandeService commandeService;

    [ObservableProperty]
    CategorieService categorieService;

    ProductService productService;

    [ObservableProperty]
    ObservableCollection<Categorie> allProductCategorie = new();

    [ObservableProperty]
    Categorie selectCategorie;

    [ObservableProperty]
    ObservableCollection<Product> displayProducts = new();

    [ObservableProperty]
    ObservableCollection<Product> productsCommande = new();

    [ObservableProperty]
    bool isProductsCommandeFill = false;

    [ObservableProperty]
    Product product;

    [ObservableProperty]
    double productsCommandTotalPrice = 0;

    [ObservableProperty]
    int productsCommandTotalPoints = 0;

    public CommandeViewModel(CommandeService commandeService, CategorieService categorieService, ProductService productService)
    {
        this.commandeService = commandeService;
        this.categorieService = categorieService;
        this.productService = productService;
    }

    [RelayCommand]
    public async Task DisplayCategorie(Categorie p)
    {
        DisplayProducts = new();

        List<Product> productList = new();

        try
        {
            IsBusy = true;
            productList = await productService.GetAllProductFromCategorie(p.id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Commande Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
        
        foreach (Product product in productList)
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

        foreach (Product p in ProductsCommande)
        {
            totalPrice += p.prixProduit;
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
        //Commande c = new();
        //c.product = Product;

        await Shell.Current.GoToAsync("///CommandeView", false);
        IsProductsCommandeFill = true;

        if(monnaie == "currency")
        {
            //c.monnaie = Product.prixProduit + "€";
            ProductsCommandTotalPrice += Product.prixProduit;
        }else if (monnaie == "points")
        {
            //c.monnaie = Product.pointsFidelite+" points";
            ProductsCommandTotalPoints += Product.pointsFidelite;
        }

        ProductsCommande.Add(Product);

        Product = null;
    }

    [RelayCommand]
    async Task DeleteItem(Product c)
    {
        int index = ProductsCommande.IndexOf(c);
        ProductsCommande.RemoveAt(index);

        if(ProductsCommande.Count==0) IsProductsCommandeFill=false;
    }

    [RelayCommand]
    async Task Pay()
    {
        Commande commande = await commandeService.CreerCommande();

        List<Product> banList = new();

        foreach (Product p in ProductsCommande)
        {
            if (!banList.Contains(p))
            {
                int quantite = ProductsCommande.Count(x => x == p);
                await commandeService.CreerCommander(p.id, commande.id, quantite);
                banList.Add(p);
            }
        }

        await Shell.Current.GoToAsync("///CommandeView", false);

        IsProductsCommandeFill = false;

        ProductsCommande.Clear();

        ProductsCommandTotalPoints = 0;
        ProductsCommandTotalPoints = 0;

        ((AppShell)App.Current.MainPage).SwitchtoTab(0);
    }
}
