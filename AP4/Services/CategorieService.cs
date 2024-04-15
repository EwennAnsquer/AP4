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

    public async Task<Categorie> CreerCategorie(int id, string nomCategorie, string urlImage)
    {
        var data = new
        {
            UserID = id,
            nomCategorie = nomCategorie,
            urlImage = urlImage,
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/creerCategorie", JsonConvert.SerializeObject(data));

        return await response.Content.ReadFromJsonAsync<Categorie>();
    }
}

