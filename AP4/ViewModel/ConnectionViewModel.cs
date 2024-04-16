namespace AP4.ViewModel;

public partial class ConnectionViewModel : BaseViewModel
{
    [ObservableProperty]
    string mail;
    readonly string mailRegex = @"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$"; //regex pour les mails

    [ObservableProperty]
    string password;

    readonly LoginService LoginService;

    public ConnectionViewModel(LoginService loginService)
    {
        this.LoginService = loginService;
    }

    [RelayCommand]
    async Task Login() //permet de se connecter si on valide toutes les sécurités
    {
        if (!AreAllFieldsFilled())
        {
            await Shell.Current.DisplayAlert("Login Error", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        if (!IsEmailValid())
        {
            await Shell.Current.DisplayAlert("Login Error", "L'Email n'est pas valide.","OK");
            return;
        }

        try
        {
            IsBusy = true;
            CurrentUser = await LoginService.Login(Mail, Password);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Login Error",ex.Message,"OK");
        }
        finally
        {
            IsBusy = false;
        }

        if(CurrentUser != null) //si on a un utilisateur alors on change de view pour la MainPageView
        {
            GoToMainPage();
        }
    }

    [RelayCommand]
    async Task GoToRegister() //permet de changer de view pour la view RegisterView
    {
        await Shell.Current.GoToAsync(nameof(InscriptionView), false);
    }

    [RelayCommand]
    async Task GoToMainPage() //permet de changer de view pour la view MainPageView
    {
        await Shell.Current.GoToAsync("///MainPage", false);
    }

    public bool IsEmailValid() //permet de savoir si un mail est valide
    {
        return Regex.IsMatch(Mail, mailRegex);
    }

    private bool AreAllFieldsFilled() //permet de savoir si tous les champs ne sont pas vide
    {
        return !string.IsNullOrWhiteSpace(Mail)
            && !string.IsNullOrWhiteSpace(Password);
    }
}
