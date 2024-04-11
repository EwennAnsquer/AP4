namespace AP4.Model;

public class Product
{
    public int id { get; set; }
    public string nomProduit { get; set; }
    public double prixProduit { get; set; }
    public int pointsFidelite { get; set; }
    public Categorie laCategorie { get; set; }
    public string imageUrl { get; set; }
}
