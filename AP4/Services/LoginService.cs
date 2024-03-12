namespace AP4.Services;

public class LoginService
{
    HttpClient _httpClient;

    public LoginService()
    {
        _httpClient = new HttpClient();
    }

    public async Task Login(string Mail, string Password)
    {

    }
}
