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
        if(viewModel.AllProductCategorie.Count == 0)
        { 
            List<Categorie> list = await viewModel.CategorieService.GetAllCategorie();

            foreach (var item in list)
            {
                viewModel.AllProductCategorie.Add(item);
            }
        }

        if (viewModel.SelectCategorie == null)
        {
            viewModel.SelectCategorie = viewModel.AllProductCategorie[0];
        }
    }
}