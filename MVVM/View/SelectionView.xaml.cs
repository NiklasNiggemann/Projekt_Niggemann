using Mensa_App.Classes;
using Mensa_App.Classes.View;
using Mensa_App.MVVM.ViewModels;

namespace Mensa_App.MVVM.View;

public partial class SelectionView : ContentPage
{
	SelectionViewModel SelectionViewModel { get; set; }
    public SelectionView()
    {
        InitializeComponent();
        SelectionViewModel = new SelectionViewModel();
        BindingContext = SelectionViewModel;
        MenuView.MainMenuSelected += MenuView_MainMenuSelected;
    }
    private void MenuView_MainMenuSelected(object sender, Dish dish)
    {
        SelectionViewModel.Selection.UserMain = dish;
    }
}