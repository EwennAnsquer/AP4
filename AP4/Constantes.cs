namespace AP4;

public class Constantes
{
    public static User CurrentUser { get; set; } = null;

    public async static Task DisplayAlert(string txt)
    {
        await Shell.Current.DisplayAlert("test", txt, "OK");
    }
}
