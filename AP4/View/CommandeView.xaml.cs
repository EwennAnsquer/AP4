namespace AP4.View;

public partial class CommandeView : ContentPage
{
	readonly CommandeViewModel viewModel;
	public CommandeView(CommandeViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        if(viewModel.AllProductCategorie.Count == 0) //si il n'y a pas de catégorie alors on va les récupérer avec l'api
        { 
            List<Categorie> list = await viewModel.CategorieService.GetAllCategorie();

            foreach (var item in list)
            {
                viewModel.AllProductCategorie.Add(item);
            }
        }

        if (viewModel.SelectCategorie == null)
        {
            viewModel.SelectCategorie = viewModel.AllProductCategorie[0]; //on sélectionne la première catégorie dans la collection view
        }
    }
}