namespace AP4.ViewModel;

public partial class PlusViewModel : BaseViewModel
{
    public PlusViewModel()
    {
    }

    [RelayCommand]
    async Task Deconnexion() //permet de se déconnecter en supprimmant la mention du user dans le code et en redirigant vers la view MainPageView qui redirigera vers la view LoginView
    {
        CurrentUser = null;
        await Shell.Current.GoToAsync("///MainPage", false);
    }
}
