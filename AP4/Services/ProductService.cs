using System.Collections.Generic;

namespace AP4.Services;

public class ProductService
{
    HttpClient _httpClient;
    public ProductService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Product>> GetAllProductFromCategorie(int id) //permet de récupérer tous les produits d'une catégorie
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

    public async Task CreerProduit(int id, string nomProduit, int prixProduit, int pointsFidelite, string imageUrl, int categorieId) //permet de créer un produit et de le lier à un user
    {
        var data = new
        {
            UserID = id,
            nomProduit = nomProduit,
            prixProduit = prixProduit,
            pointsFidelite = pointsFidelite,
            imageUrl = imageUrl,
            categorieId = categorieId
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/creerProduit", JsonConvert.SerializeObject(data));
    }
}

