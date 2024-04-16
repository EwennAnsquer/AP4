namespace AP4.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy; //permet de gérer l'état de la page pour l'activity indicator

    [ObservableProperty]
    string title = "";

    public bool IsNotBusy => !IsBusy;
}
