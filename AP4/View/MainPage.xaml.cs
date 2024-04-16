namespace AP4;

public partial class MainPage : ContentPage
{
    MainPageViewModel viewModel;
    public MainPage(MainPageViewModel mainPageViewModel)
    {
        BindingContext = mainPageViewModel;
        this.viewModel = mainPageViewModel;
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        if (Constantes.CurrentUser == null) //si il n'y a pas de user enregistrer alors on change de page vers une page de login sinon on passe le user dans le viewModel
        {
            await Shell.Current.GoToAsync(nameof(ConnectionView),false);
        }
        else
        {
            viewModel.User = Constantes.CurrentUser;

            base.OnAppearing();
        }
    }
}
