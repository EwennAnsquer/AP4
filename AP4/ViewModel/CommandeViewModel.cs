using System.Collections.ObjectModel;

namespace AP4.ViewModel;

public partial class CommandeViewModel : BaseViewModel
{
    readonly CommandeService commandeService;
    [ObservableProperty]
    ObservableCollection<ProductCategorie> allProductCategorie = new();

    [ObservableProperty]
    ProductCategorie firstProductCategorie;
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

        if (AllProductCategorie.Count() > 0) 
        {
            firstProductCategorie = AllProductCategorie[0];
        }
    }

    [RelayCommand]
    async Task DisplayCategorie(ProductCategorie p)
    {
        //await Shell.Current.DisplayAlert("test", p.nom, "ok");
    }
}
