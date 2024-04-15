﻿namespace AP4.Services;

public class CommandeService
{
    HttpClient _httpClient;

    public CommandeService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<Commande> CreerCommande()
    {
        var data = new
        {
            UserID = Constantes.CurrentUser.id
        };

        var response = await GetResponsePost(_httpClient, "/api/mobile/creerCommande", JsonConvert.SerializeObject(data));

        return await response.Content.ReadFromJsonAsync<Commande>();
    }

    public async Task CreerCommander(int produit, int commande, int quantite)
    {
        var data = new
        {
            UserID = Constantes.CurrentUser.id,
            Id = Constantes.CurrentUser.id,
            leProduit = produit,
            laCommande = commande,
            quantite = quantite
        };

        await GetResponsePost(_httpClient, "/api/mobile/creerCommander", JsonConvert.SerializeObject(data));
    }
}
