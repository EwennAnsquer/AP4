namespace AP4.ViewModel;

public partial class UserViewModel : BaseViewModel
{
    [ObservableProperty]
    User user;
    public UserViewModel()
    {
        User = new User();
        User.Id = 1;
        User.Name = "Anne Marie Françoise fdhgugdfjugiufdghiufn uit";
        User.Point = 100;
    }
}
