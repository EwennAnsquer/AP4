namespace AP4.ViewModel;

public partial class CommandeViewModel : BaseViewModel
{
    CommandeService commandeService;

    [ObservableProperty]
    CategorieService categorieService;

    ProductService productService;

    UserService userService;

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

    public CommandeViewModel(CommandeService commandeService, CategorieService categorieService, ProductService productService, UserService userService)
    {
        this.commandeService = commandeService;
        this.categorieService = categorieService;
        this.productService = productService;
        this.userService = userService;
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
        await Shell.Current.GoToAsync("///CommandeView", false);
        IsProductsCommandeFill = true;

        if(monnaie == "€")
        {
            ProductsCommandTotalPrice += Product.prixProduit;
        }
        else if (monnaie == "points")
        {
            ProductsCommandTotalPoints += Product.pointsFidelite;
        }

        Product p = new Product();
        p.prixProduit = Product.prixProduit;
        p.pointsFidelite = Product.pointsFidelite;
        p.nomProduit = Product.nomProduit;
        p.id = Product.id;
        p.laCategorie = Product.laCategorie;
        p.imageUrl = Product.imageUrl;
        p.actualCurrency = monnaie;
        ProductsCommande.Add(p);

        Product = null;
    }

    [RelayCommand]
    async Task DeleteItem(Product c)
    {
        if (c.actualCurrency == "€")
        {
            ProductsCommandTotalPrice -= c.prixProduit;
        }
        else if (c.actualCurrency == "points")
        {
            ProductsCommandTotalPoints -= c.pointsFidelite;
        }

        ProductsCommande.Remove(c);

        if(ProductsCommande.Count==0) IsProductsCommandeFill=false;
    }

    [RelayCommand]
    async Task Pay()
    {
        if (Constantes.CurrentUser.StockPointsFidelite < ProductsCommandTotalPoints)
        {
            await Shell.Current.DisplayAlert("Error Pas assez de points", "Vous n'avez pas assez de points pour payer cette commande. Essayez de payer en euros.", "OK");
            return;
        }

        Commande commande = await CreerCommande();

        if(commande != null)
        {
            List<Product> banList = new();

            foreach (Product p in ProductsCommande)
            {
                if (!banList.Contains(p))
                {
                    int quantite = ProductsCommande.Count(x => x == p);
                    await CreerCommander(p.id, commande.id, quantite);
                    banList.Add(p);
                }
            }

            await Shell.Current.GoToAsync("///CommandeView", false);

            IsProductsCommandeFill = false;

            ProductsCommande.Clear();

            ProductsCommandTotalPoints = 0;
            ProductsCommandTotalPoints = 0;

            Constantes.CurrentUser.StockPointsFidelite -= ProductsCommandTotalPoints;
            Constantes.CurrentUser.StockPointsFidelite += (int)(ProductsCommandTotalPrice * 0.25);

            try
            {
                IsBusy = true;
                await userService.UpdateUserStockPointsFidelite();
                await Shell.Current.DisplayAlert("Commande", "Merci d'avoir commander.", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Creer Commande Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }


        }

        ((AppShell)App.Current.MainPage).SwitchtoTab(0);
    }

    async Task<Commande> CreerCommande()
    {
        Commande commande = null;

        try
        {
            IsBusy = true;
            commande = await commandeService.CreerCommande();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Creer Commande Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }

        return commande;
    }

    async Task CreerCommander(int productId, int commandeId, int quantite)
    {

        try
        {
            IsBusy = true;
            await commandeService.CreerCommander(productId, commandeId, quantite);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Creer Commander Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
