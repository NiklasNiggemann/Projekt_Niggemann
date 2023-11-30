using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.MVVMS.Models;
using Mensa_App.Classes;
using CommunityToolkit.Mvvm.Input;

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
        SettingsModel.UserAllergyIngredientList.CollectionChanged += UserAllergyIngredientList_CollectionChanged;
    }

    private void UserAllergyIngredientList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        
    }

    [RelayCommand]
    public static void ChangeSelectedDishes(Dish dish)
    {
        SelectionModel.SelectedDishes.Add(dish);
    }
}

