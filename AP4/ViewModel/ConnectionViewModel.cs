namespace AP4.ViewModel;

public partial class ConnectionViewModel : BaseViewModel
{
    [ObservableProperty]
    string mail;
    readonly string mailRegex = @"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$";

    [ObservableProperty]
    string password;

    readonly LoginService LoginService;

    public ConnectionViewModel(LoginService loginService)
    {
        this.LoginService = loginService;
    }

    [RelayCommand]
    async Task Login()
    {
        //await Shell.Current.DisplayAlert("test", Mail+" "+Password, "OK");

        if (!AreAllFieldsFilled())
        {
            await Shell.Current.DisplayAlert("Login Error", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        if (!IsEmailValid())
        {
            await Shell.Current.DisplayAlert("Login Error", "Email is not valid.","OK");
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

        if(CurrentUser != null)
        {
            GoToMainPage();
        }
    }

    [RelayCommand]
    async Task GoToRegister()
    {
        await Shell.Current.GoToAsync(nameof(InscriptionView), false);
    }

    [RelayCommand]
    async Task GoToMainPage()
    {
        await Shell.Current.GoToAsync("///MainPage", false);
    }

    public bool IsEmailValid()
    {
        return Regex.IsMatch(Mail, mailRegex);
    }

    private bool AreAllFieldsFilled()
    {
        return !string.IsNullOrWhiteSpace(Mail)
            && !string.IsNullOrWhiteSpace(Password);
    }
}
