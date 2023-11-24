using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.MVVMS.Models;
using Mensa_App.Classes;
using CommunityToolkit.Mvvm.Input;
using Mensa_App.MVVMS.Models;

namespace Mensa_App.MVVMS.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    public MenuModel MyMenuModel { get; set; }
    public List<Dish> MainMenuView { get; set; }
    public List<Dish> SideMenuView { get; set; }
    public List<Dish> SoupMenuView { get; set; }
    public List<Dish> DessertMenuView { get; set; }

    public MenuViewModel()
    {
        MyMenuModel = new MenuModel();
        MainMenuView = MyMenuModel.MainMenu;
        SideMenuView = MyMenuModel.SideMenu;
        SoupMenuView = MyMenuModel.SoupMenu;
        DessertMenuView = MyMenuModel.DessertMenu;
    }
    [RelayCommand]
    public static void ChangeSelectedMain(Dish dish)
    {
        SelectionModel.SelectedMain = dish;
    }
    [RelayCommand]
    public static void ChangeSelectedSide(Dish dish)
    {
        SelectionModel.SelectedSide = dish;
    }
    [RelayCommand]
    public static void ChangeSelectedSoup(Dish dish)
    {
        SelectionModel.SelectedSoup = dish;
    }
    [RelayCommand]
    public static void ChangeSelectedDessert(Dish dish)
    {
        SelectionModel.SelectedDessert = dish;
    }
}

