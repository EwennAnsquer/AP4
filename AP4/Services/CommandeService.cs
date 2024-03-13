namespace AP4.Services;

public class CommandeService
{
    HttpClient _httpClient;

    public CommandeService()
    {
        _httpClient = new HttpClient();
    }
}
