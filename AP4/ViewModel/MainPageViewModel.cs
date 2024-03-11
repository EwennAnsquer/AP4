namespace AP4.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    User user;

    List<OffreSpecial> allOffreSpecials { get; set; } = new();

    [ObservableProperty]
    List<OffreSpecial> currentOffreSpecials = new();
    public MainPageViewModel()
    {
        User = new User();
        User.Id = 1;
        User.Name = "Anne Marie Françoise fdhgugdfjugiufdghiufn uit";
        User.Point = 100;

        OffreSpecial o1 = new OffreSpecial();
        o1.dateDebut = new DateTime(2024,03,10);
        o1.dateFin = new DateTime(2024,05,10);
        o1.nom = "Points X2";
        o1.avantage = "x2";
        o1.description = "Gagne deux fois plus de points.";
        OffreSpecial o2 = new OffreSpecial();
        o2.dateDebut = new DateTime(2024, 08, 9);
        o2.dateFin = new DateTime(2024, 08, 9);
        o2.nom = "Anniversaire";
        o2.avantage = "+25";
        o2.description = "Joyeux Anniversaire.";

        allOffreSpecials.Add(o1);
        allOffreSpecials.Add(o2);
    }

    [RelayCommand]
    void GetCurrentOffreSpecials()
    {
        List<OffreSpecial> list = new();

        foreach (OffreSpecial o in allOffreSpecials)
        {
            if (o.dateDebut <= DateTime.Now && o.dateFin >= DateTime.Now)
            {
                list.Add(o);
            }
        }

        CurrentOffreSpecials = list;
    }
}
