namespace AP4.Services;

public class UserService
{
    HttpClient _httpClient;

    public UserService()
    {
        _httpClient = new HttpClient();
    }

    public async Task UpdateUserStockPointsFidelite()
    {
        var data = new
        {
            Id = Constantes.CurrentUser.id,
            stockPointsFidelite = Constantes.CurrentUser.StockPointsFidelite,
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/updateUser", JsonConvert.SerializeObject(data));
    }

    public async Task<User> GetFindUserByEmail()
    {
        var data = new
        {
            email = Constantes.CurrentUser.email,
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/GetFindUserByEmail", JsonConvert.SerializeObject(data));

        return await response.Content.ReadFromJsonAsync<User>();
    }
}
