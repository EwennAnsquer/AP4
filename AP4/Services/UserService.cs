namespace AP4.Services;

public class UserService
{
    HttpClient _httpClient;

    public UserService()
    {
        _httpClient = new HttpClient();
    }

    public async Task UpdateUserStockPointsFidelite() //permet d'update les points de fidélités d'un user
    {
        var data = new
        {
            Id = Constantes.CurrentUser.id,
            stockPointsFidelite = Constantes.CurrentUser.StockPointsFidelite,
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/updateUser", JsonConvert.SerializeObject(data));
    }

    public async Task<User> GetFindUserByEmail() //permet de récupérer certaines des infos sur un user avec une adresse mail
    {
        var data = new
        {
            email = Constantes.CurrentUser.email,
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/GetFindUserByEmail", JsonConvert.SerializeObject(data));

        return await response.Content.ReadFromJsonAsync<User>();
    }
}
