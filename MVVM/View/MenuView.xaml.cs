using Mensa_App.Classes.Models;
using Mensa_App.Classes.ViewModels;
using Mensa_App.MVVM.Models;
using System.Diagnostics;
using System.Windows.Input;

namespace Mensa_App.Classes.View;

public partial class MenuView : TabbedPage
{
    MenuViewModel MenuViewModel {  get; set; }
    public MenuView()
	{
		InitializeComponent();
        MenuViewModel = new MenuViewModel();
        BindingContext = MenuViewModel;
	}

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        MenuViewModel.Menu.UserMenu.Add(e.SelectedItem as Dish);
        foreach (var dish in MenuViewModel.Menu.UserMenu)
        {
            MenuViewModel.Menu.UserMenuSum += dish.Price;
        }
    }
}