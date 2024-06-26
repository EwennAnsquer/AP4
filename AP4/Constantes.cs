﻿namespace AP4;

public class Constantes
{
    public static User CurrentUser { get; set; } = null; //stockage de l'utilisateur
    public static string BASE_URL { get; } = "http://172.17.0.62:8082"; //stockage de l'url pour l'api

    public async static Task DisplayAlert(string txt) //permet de facilement afficher des données pour le debug
    {
        await Shell.Current.DisplayAlert("test", txt, "OK");
    }

    public async static Task<HttpResponseMessage> GetResponsePost(HttpClient client, string apiUrl, string jsonContent)
    {
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{BASE_URL}{apiUrl}", content);
        return response;
    }

    public async static Task<HttpResponseMessage> GetResponseGet(HttpClient client, string apiUrl, string jsonContent)
    {
        var response = await client.GetAsync($"{BASE_URL}{apiUrl}?jsonContent={Uri.EscapeDataString(jsonContent)}");
        return response;
    }
}
