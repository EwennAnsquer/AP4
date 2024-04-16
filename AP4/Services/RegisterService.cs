using System.Net.Http.Json;

namespace AP4.Services;

public class RegisterService
{
    HttpClient _httpClient;
    public RegisterService() 
    { 
        _httpClient = new HttpClient();
    }

    public async Task<User> Register(string Mail, string Password, string Nom, string Prenom, string Telephone, string DateAnniversaire) //permet de créer un nouveau compte user
    {
        var data = new
        {
            email=Mail,
            nom=Nom,
            prenom=Prenom,
            password=Password,
            telephone=Telephone,
            dateNaissance=DateAnniversaire
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/setInscription", JsonConvert.SerializeObject(data));

        return await response.Content.ReadFromJsonAsync<User>();
    }
}
