namespace AP4.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    User user;

    [ObservableProperty]
    ObservableCollection<OffreSpecial> currentOffreSpecials = new();

    public MainPageViewModel()
    {
    }
}
