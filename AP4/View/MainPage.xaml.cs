namespace AP4;

public partial class MainPage : ContentPage
{
    readonly MainPageViewModel viewModel;
    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        BindingContext = mainPageViewModel;
        this.viewModel = mainPageViewModel;
    }

    protected async override void OnAppearing()
    {
        if (viewModel.User == null)
        {
            await Shell.Current.GoToAsync(nameof(ConnectionView));
        }
        else
        {
            viewModel.GetCurrentPointsCommand.Execute(null);
            viewModel.GetCurrentOffreSpecialsCommand.Execute(null);
            base.OnAppearing();
        }
    }

}
