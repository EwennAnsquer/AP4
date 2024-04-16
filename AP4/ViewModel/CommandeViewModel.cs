namespace AP4.ViewModel;

public partial class CommandeViewModel : BaseViewModel
{
    CommandeService commandeService;

    [ObservableProperty]
    CategorieService categorieService;

    ProductService productService;

    UserService userService;

    [ObservableProperty]
    ObservableCollection<Categorie> allProductCategorie = new(); //liste des catégories

    [ObservableProperty]
    Categorie selectCategorie; //la catégorie sélectionné. Par défaut c'est la première.

    [ObservableProperty]
    ObservableCollection<Product> displayProducts = new(); //les produits actuellement affichés sur la page de commande

    [ObservableProperty]
    ObservableCollection<Product> productsCommande = new(); //les produits présents dans la commande

    [ObservableProperty]
    bool isProductsCommandeFill = false; //permet de savoir si il y a des produits dans la commande

    [ObservableProperty]
    Product product; //produit garder en mémoire lorsqu'on va dans la ProductPriceView

    [ObservableProperty]
    double productsCommandTotalPrice = 0; //prix total en euros de la commande

    [ObservableProperty]
    int productsCommandTotalPoints = 0; //prix total en points de la commande

    public CommandeViewModel(CommandeService commandeService, CategorieService categorieService, ProductService productService, UserService userService)
    {
        this.commandeService = commandeService;
        this.categorieService = categorieService;
        this.productService = productService;
        this.userService = userService;
    }

    [RelayCommand]
    public async Task DisplayCategorie(Categorie p) //permet de récupérer tous les produits d'une catégorie donné
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
    async Task GoToProductPrice(Product p) //garde en mémoire le produit choisi et change de view pour la ProcductPriceView
    {
        Product = p;
        await Shell.Current.GoToAsync(nameof(ProductPriceView), false);
    }

    [RelayCommand]
    void CalculTotalPriceProductsCommand() //Permet de calculer le prix total de la commande en euros
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
    async Task GoToAchatView() //change la view pour AchatView
    {
        await Shell.Current.GoToAsync(nameof(AchatView), false);
    }

    [RelayCommand]
    async Task GoToBackFromProductPriceView(string monnaie) //par de la view ProductPriceView vers la view CommandeView et ajoute un produit à la liste ProductsCommande avec son choix de monnaie (euros ou points)
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
    async Task DeleteItem(Product c) //enlève le produit de la commande et calcul de la nouvelle valeur totale de la commande
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
    async Task Pay() //permet de payer en enlevant des points au user et en réinitialisant les interfaces et en créant les commandes dans la BDD
    {
        if (Constantes.CurrentUser.StockPointsFidelite < ProductsCommandTotalPoints) //sécuriter pour ne pouvoir que payer avec des points disponibles
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
                    int quantite = ProductsCommande.Count(x => x == p); //calcul de la quantité de produit dans une commande
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
            Constantes.CurrentUser.StockPointsFidelite += (int)(ProductsCommandTotalPrice * 0.25); //les points gagnés par le user corespondent à 25% du montant en euros de la commande en euros

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

        ((AppShell)App.Current.MainPage).SwitchtoTab(0); //change le tab de la nav bar pour celui de l'accueil
    }

    async Task<Commande> CreerCommande() //permet de créer une commande
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

    async Task CreerCommander(int productId, int commandeId, int quantite) //permet de créer un lien entre le produit et la commande
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
