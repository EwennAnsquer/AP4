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
        if(viewModel.AllProductCategorie.Count == 0) //si il n'y a pas de cat�gorie alors on va les r�cup�rer avec l'api
        { 
            List<Categorie> list = await viewModel.CategorieService.GetAllCategorie();

            foreach (var item in list)
            {
                viewModel.AllProductCategorie.Add(item);
            }
        }

        if (viewModel.SelectCategorie == null)
        {
            viewModel.SelectCategorie = viewModel.AllProductCategorie[0]; //on s�lectionne la premi�re cat�gorie dans la collection view
        }
    }
}