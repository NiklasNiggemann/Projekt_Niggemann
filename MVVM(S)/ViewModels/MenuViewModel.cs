using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.MVVMS.Models;
using Mensa_App.Classes;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Collections.ObjectModel;

namespace Mensa_App.MVVMS.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    public MenuModel MenuModel { get; set; }
    public List<Dish> MainMenuView { get; set; }
    public List<Dish> SideMenuView { get; set; }
    public List<Dish> SoupMenuView { get; set; }
    public List<Dish> DessertMenuView { get; set; }
    public MenuViewModel()
    {
        MenuModel = new MenuModel();
        MainMenuView = MenuModel.MainMenu;
        SideMenuView = MenuModel.SideMenu;
        SoupMenuView = MenuModel.SoupMenu;
        DessertMenuView = MenuModel.DessertMenu;
    }
    [RelayCommand]
    public void ChangeSelectedDishes(IList<Dish> selectedDishes)
    {

    }
}

