namespace AP4.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    User user;
    List<OffreSpecial> allOffreSpecials { get; set; } = new();

    [ObservableProperty]
    List<OffreSpecial> currentOffreSpecials = new();

    [ObservableProperty]
    int nbPoints = 0;

    List<PointFidelite> allPoints = new();

    [ObservableProperty]
    List<PointFidelite> closeToEndPoints = new();
    public MainPageViewModel()
    {
        if(Constantes.CurrentUser == null)
        {
            Constantes.CurrentUser = new User();
            Constantes.CurrentUser.id = 1;
            Constantes.CurrentUser.nom = "Nom";
            Constantes.CurrentUser.prenom = "prenom";
            Constantes.CurrentUser.telephone = "1234567890";
            Constantes.CurrentUser.dateNaissance = "2003-08-09";
            Constantes.CurrentUser.email = "email@email.com";
            Constantes.CurrentUser.StockPointsFidelite = 100;

            User = CurrentUser;
        }

        if (!allOffreSpecials.Any())
        {
            OffreSpecial o1 = new OffreSpecial();
            o1.dateDebut = new DateTime(2024, 03, 10);
            o1.dateFin = new DateTime(2024, 05, 10);
            o1.nom = "Points X2";
            o1.avantage = "x2";
            o1.description = "Gagne deux fois plus de points.";
            OffreSpecial o3 = new OffreSpecial();
            o3.dateDebut = new DateTime(2024, 03, 10);
            o3.dateFin = new DateTime(2024, 05, 10);
            o3.nom = "Points X2";
            o3.avantage = "x2";
            o3.description = "Gagne deux fois plus de points.";
            OffreSpecial o4 = new OffreSpecial();
            o4.dateDebut = new DateTime(2024, 03, 10);
            o4.dateFin = new DateTime(2024, 05, 10);
            o4.nom = "Points X2";
            o4.avantage = "x2";
            o4.description = "Gagne deux fois plus de points.";
            OffreSpecial o5 = new OffreSpecial();
            o5.dateDebut = new DateTime(2024, 03, 10);
            o5.dateFin = new DateTime(2024, 05, 10);
            o5.nom = "Points X2";
            o5.avantage = "x2";
            o5.description = "Gagne deux fois plus de points.";
            OffreSpecial o2 = new OffreSpecial();
            o2.dateDebut = new DateTime(2024, 08, 9);
            o2.dateFin = new DateTime(2024, 08, 9);
            o2.nom = "Anniversaire";
            o2.avantage = "+25";
            o2.description = "Joyeux Anniversaire.";

            allOffreSpecials.Add(o1);
            allOffreSpecials.Add(o2);
            allOffreSpecials.Add(o3);
            allOffreSpecials.Add(o3);
            allOffreSpecials.Add(o4);
        }

        if (!allPoints.Any())
        {
            PointFidelite p1 = new PointFidelite();
            p1.dateDebut = new DateTime(2024, 03, 10);
            p1.dateFin = new DateTime(2024, 04, 10);
            p1.effect = '+';
            p1.points = 25;

            PointFidelite p2 = new PointFidelite();
            p2.dateDebut = new DateTime(2023, 03, 10);
            p2.dateFin = new DateTime(2023, 04, 10);
            p2.effect = '+';
            p2.points = 25;

            PointFidelite p3 = new PointFidelite();
            p3.dateDebut = new DateTime(2024, 02, 15);
            p3.dateFin = new DateTime(2024, 03, 15);
            p3.effect = '+';
            p3.points = 10;

            PointFidelite p4 = new PointFidelite();
            p4.dateDebut = new DateTime(2024, 02, 19);
            p4.dateFin = new DateTime(2024, 03, 19);
            p4.effect = '+';
            p4.points = 20;

            PointFidelite p5 = new PointFidelite();
            p5.dateDebut = new DateTime(2024, 03, 18);
            p5.dateFin = new DateTime(2024, 04, 18);
            p5.effect = '-';
            p5.points = 5;

            PointFidelite p6 = new PointFidelite();
            p6.dateDebut = new DateTime(2024, 02, 20);
            p6.dateFin = new DateTime(2024, 03, 20);
            p6.effect = '+';
            p6.points = 10;

            allPoints.Add(p1);
            allPoints.Add(p2);
            allPoints.Add(p3);
            allPoints.Add(p4);
            allPoints.Add(p5);
            allPoints.Add(p6);
        }
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

    [RelayCommand]
    async Task GetCurrentPoints()
    {
        if (!CurrentOffreSpecials.Any())
        {
            foreach (PointFidelite p in allPoints)
            {
                if (p.dateDebut <= DateTime.Now && p.dateFin >= DateTime.Now && p.effect == '+')
                {
                    NbPoints += p.points;
                }

                if (p.dateDebut <= DateTime.Now && p.effect == '-')
                {
                    NbPoints -= p.points;
                }

                if (p.effect=='+')
                {
                    ifPointCloseToEnd(p);
                }
            }
        }
    }

    private async void ifPointCloseToEnd(PointFidelite p)
    {
        TimeSpan dif = (p.dateFin - DateTime.Now);

        if (dif <= TimeSpan.FromDays(7) && dif >= TimeSpan.Zero)
        {
            CloseToEndPoints.Add(p);
        }
    }
}
