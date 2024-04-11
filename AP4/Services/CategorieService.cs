using System.Net.Http.Json;

namespace AP4.Services;

public class CategorieService
{
    HttpClient _httpClient;
    public CategorieService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Categorie>> GetAllCategorie()
    {
        var data = new
        {
            Id=Constantes.CurrentUser.id
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/GetAllCategories", JsonConvert.SerializeObject(data));

        return await response.Content.ReadFromJsonAsync<List<Categorie>>();
    }
}

