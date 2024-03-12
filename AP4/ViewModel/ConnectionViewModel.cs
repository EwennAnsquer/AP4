namespace AP4.ViewModel;

public partial class ConnectionViewModel : BaseViewModel
{
    [ObservableProperty]
    string mail;

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
        await Shell.Current.DisplayAlert("test", Mail+" "+Password, "OK");
       //await LoginService.Login(Mail,Password);
    }

    [RelayCommand]
    async Task GoToRegister()
    {
        await Shell.Current.GoToAsync(nameof(InscriptionView));
    }
}
