using System.Collections.Generic;

namespace AP4.Services;

public class ProductService
{
    HttpClient _httpClient;
    public ProductService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Product>> GetAllProductFromCategorie(int id)
    {
        var data = new
        {
            Id=id
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/GetProduitsParCategorie", JsonConvert.SerializeObject(data));

        if (!response.IsSuccessStatusCode)
        {
            return new List<Product>();
        }

        return await response.Content.ReadFromJsonAsync<List<Product>>();
    }
}

