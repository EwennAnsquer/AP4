using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AP4.ViewModel;

public partial class InscriptionViewModel : BaseViewModel
{
    [ObservableProperty]
    string mail;
    readonly string mailRegex = @"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$";

    [ObservableProperty]
    string password;

    [ObservableProperty]
    string nom;

    [ObservableProperty]
    string prenom;

    [ObservableProperty]
    string telephone;
    readonly string telephoneRegex = @"^\d{10}$";

    [ObservableProperty]
    string dateAnniversaire = DateTime.Now.ToString();

    readonly RegisterService RegisterService;
    readonly CategorieService CategorieService;
    readonly ProductService ProductService;

    public InscriptionViewModel(RegisterService registerService, CategorieService categorieService, ProductService productService)
    {
        this.RegisterService = registerService;
        this.CategorieService = categorieService;
        this.ProductService = productService;
    }

    [RelayCommand]
    async Task Register()
    {
        //await Shell.Current.DisplayAlert("test", Mail+" "+Password+" "+Nom+" "+" "+Prenom+" "+Telephone+" "+DateAnniversaire, "OK");

        if (!AreAllFieldsFilled())
        {
            await Shell.Current.DisplayAlert("Error", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        if (!IsEmailValid())
        {
            await Shell.Current.DisplayAlert("Error", "Le mail n'est pas conforme.", "OK");
            return;
        }

        if (!IsTelephoneValid())
        {
            await Shell.Current.DisplayAlert("Error", "Le numéro de téléphone n'est pas conforme.", "OK");
            return;
        }

        if(!IsDateAnniversaireValid())
        {
            await DisplayAlert(DateAnniversaire);
            await Shell.Current.DisplayAlert("Error", "La date d'anniversaire n'est pas valide.", "OK");
            return;
        }

        try
        {
            IsBusy = true;
            User user = await RegisterService.Register(Mail, Password, Nom, Prenom, Telephone, DateAnniversaire);
            await Shell.Current.DisplayAlert("Registration", "Registration is successfully done.", "OK");

            AllFieldsEmpty();

            Categorie c1 = await CategorieService.CreerCategorie(user.id, "Tacos", "tacos.png");
            Categorie c2 = await CategorieService.CreerCategorie(user.id,"Burger","burger.png");

            await ProductService.CreerProduit(user.id, "Tacos", 10, 10, "tacos.png", c1.id);

            await ProductService.CreerProduit(user.id, "Big-Mac", 5,5,"bigmac.png",c2.id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Register Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoToLogin()
    {
        await Shell.Current.GoToAsync(nameof(ConnectionView), false);
    }

    private bool AreAllFieldsFilled()
    {
        return !string.IsNullOrWhiteSpace(Mail)
            && !string.IsNullOrWhiteSpace(Password)
            && !string.IsNullOrWhiteSpace(Nom)
            && !string.IsNullOrWhiteSpace(Prenom)
            && !string.IsNullOrWhiteSpace(Telephone)
            && !string.IsNullOrWhiteSpace(DateAnniversaire);
    }

    private void AllFieldsEmpty()
    {
        Mail = string.Empty;
        Password = string.Empty;
        Nom = string.Empty;
        Prenom = string.Empty;
        Telephone = string.Empty;
        DateAnniversaire = DateTime.Now.ToString();
    }

    public bool IsEmailValid()
    {
        return Regex.IsMatch(Mail, mailRegex);
    }

    public bool IsDateAnniversaireValid()
    {
        DateTime date;
        if(DateTime.TryParseExact(DateAnniversaire, "M/d/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out date))
        {
            if(date > DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public bool IsTelephoneValid()
    {
        return Regex.IsMatch(Telephone, telephoneRegex);
    }
}
