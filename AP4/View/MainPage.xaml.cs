using System.Diagnostics;

namespace AP4
{
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel viewModel;
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
            this.viewModel = mainPageViewModel;
            SetStyle();
        }

        void SetStyle()
        {
        }

        protected override void OnAppearing()
        {
            viewModel.GetCurrentOffreSpecialsCommand.Execute(null);
            base.OnAppearing();
        }

    }

}
