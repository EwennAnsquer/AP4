﻿namespace AP4;

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
            base.OnAppearing();
        }
    }

}
