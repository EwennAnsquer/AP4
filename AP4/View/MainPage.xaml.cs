namespace AP4
{
    public partial class MainPage : ContentPage
    {
        public MainPage(UserViewModel userViewModel)
        {
            InitializeComponent();
            BindingContext = userViewModel;
            SetStyle();
        }

        void SetStyle()
        {
        }

    }

}
