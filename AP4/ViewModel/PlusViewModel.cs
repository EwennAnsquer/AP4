namespace AP4.ViewModel;

public partial class PlusViewModel : BaseViewModel
{
    public PlusViewModel()
    {
    }

    [RelayCommand]
    async Task Deconnexion()
    {
        CurrentUser = null;
        await Shell.Current.GoToAsync("///MainPage", false);
    }
}
