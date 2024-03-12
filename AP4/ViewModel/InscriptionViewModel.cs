namespace AP4.ViewModel;

public partial class InscriptionViewModel : BaseViewModel
{
    [ObservableProperty]
    string mail;

    [ObservableProperty]
    string password;

    [ObservableProperty]
    string nom;

    [ObservableProperty]
    string prenom;

    [ObservableProperty]
    string telephone;

    [ObservableProperty]
    string dateAnniversaire;

    readonly RegisterService RegisterService;

    public InscriptionViewModel(RegisterService registerService)
    {
        this.RegisterService = registerService;
    }

    [RelayCommand]
    async Task Register()
    {
        await Shell.Current.DisplayAlert("test", Mail+" "+Password+" "+Nom+" "+" "+Prenom+" "+Telephone+" "+DateAnniversaire, "OK");
        //await RegisterService.Register(Mail, Password, Nom, Prenom, Telephone, DateAnniversaire);
    }

    [RelayCommand]
    async Task GoToLogin()
    {
        await Shell.Current.GoToAsync(nameof(ConnectionView));
    }
}
