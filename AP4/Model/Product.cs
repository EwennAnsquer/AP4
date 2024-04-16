namespace AP4.Model;

public class Product
{
    public int id { get; set; }
    public string nomProduit { get; set; }
    public double prixProduit { get; set; }
    public int pointsFidelite { get; set; }
    public Categorie laCategorie { get; set; }
    public string imageUrl { get; set; }
    public string actualCurrency { get; set; } //permet de savoir si on achète avec des points ou des euros
}
