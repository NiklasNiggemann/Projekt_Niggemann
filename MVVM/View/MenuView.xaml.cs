using Mensa_App.Classes.Models;
using Mensa_App.Classes.ViewModels;
using Mensa_App.MVVM.Models;
using System.Diagnostics;
using System.Windows.Input;

namespace Mensa_App.Classes.View;

public partial class MenuView : TabbedPage
{
    MenuViewModel MenuViewModel { get; set; }
    public MenuView()
    {
        InitializeComponent();
        MenuViewModel = new MenuViewModel();
        BindingContext = MenuViewModel;
    }
    public MenuView(string URL)
    {
        InitializeComponent();
        MenuViewModel = new MenuViewModel(URL);
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

    private void Button_Clicked_0(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        MenuView newMenuView = new MenuView(MenuViewModel.Menu.DatesURL[0]);
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        MenuViewModel = new MenuViewModel(MenuViewModel.Menu.DatesURL[1]);
    }

    private void Button_Clicked_3(object sender, EventArgs e)
    {
        MenuViewModel = new MenuViewModel(MenuViewModel.Menu.DatesURL[2]);
    }

    private void Button_Clicked_4(object sender, EventArgs e)
    {
        MenuViewModel = new MenuViewModel(MenuViewModel.Menu.DatesURL[3]);
    }
}