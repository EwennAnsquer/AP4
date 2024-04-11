using static Microsoft.Maui.ApplicationModel.Permissions;

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
        if (Constantes.CurrentUser == null)
        {
            await Shell.Current.GoToAsync(nameof(ConnectionView),false);
        }
        else
        {
            viewModel.User = Constantes.CurrentUser;
            viewModel.GetCurrentPointsCommand.Execute(null);
            viewModel.GetCurrentOffreSpecialsCommand.Execute(null);

            if (viewModel.CurrentOffreSpecials.Count != 0 && viewModel.IsNotifHasBeenPush == false)
            {
                var request = new NotificationRequest
                {
                    NotificationId = new Random().Next(),
                    Title = "Offres spéciales disponibles.",
                    CategoryType = NotificationCategoryType.Status,
                };

                viewModel.IsNotifHasBeenPush = true;

                await LocalNotificationCenter.Current.Show(request);
            }

            base.OnAppearing();
        }
    }
}
