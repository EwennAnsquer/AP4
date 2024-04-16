using System.Net.Http.Json;

namespace AP4.Services;

public class LoginService
{
    HttpClient _httpClient;

    public LoginService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<User> Login(string Mail, string Password) //permet de se connecter
    {
        var data = new
        {
            email = Mail,
            password = Password,
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/GetFindUser", JsonConvert.SerializeObject(data));

        return await response.Content.ReadFromJsonAsync<User>();
    }
}
