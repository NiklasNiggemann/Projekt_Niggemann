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
    private void MainMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        MenuViewModel.Menu.UserMenu[0] = e.SelectedItem as Dish;
    }

    private void SideMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        MenuViewModel.Menu.UserMenu[1] = e.SelectedItem as Dish;
    }

    private void SoupMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        MenuViewModel.Menu.UserMenu[2] = e.SelectedItem as Dish;
    }

    private void DessertMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        MenuViewModel.Menu.UserMenu[3] = e.SelectedItem as Dish;
    }
}