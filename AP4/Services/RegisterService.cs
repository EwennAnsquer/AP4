using System.Diagnostics;
using System.Net.Http.Json;

namespace AP4.Services;

public class RegisterService
{
    HttpClient _httpClient;
    public RegisterService() 
    { 
        _httpClient = new HttpClient();
    }

    public async Task Register(string Mail, string Password, string Nom, string Prenom, string Telephone, string DateAnniversaire)
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

        string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        var request = new HttpRequestMessage(HttpMethod.Post, "http://172.17.0.62:8082/api/mobile/setInscription");
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        //request.Content = content;
        //var response = await _httpClient.SendAsync(request);
        var response = await _httpClient.PostAsync("http://172.17.0.62:8082/api/mobile/setInscription", content);
        //response.EnsureSuccessStatusCode();
        //Console.WriteLine(await response.Content.ReadAsStringAsync());
        //Debug.WriteLine(await response.Content.ReadAsStringAsync());

        await Shell.Current.DisplayAlert("test", "je register", "OK");
    }
}
