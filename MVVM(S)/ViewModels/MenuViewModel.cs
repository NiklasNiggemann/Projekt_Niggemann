using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.MVVMS.Models;
using Mensa_App.Classes;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mensa_App.MVVMS.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MainMenuView))]
    [NotifyPropertyChangedFor(nameof(SideMenuView))]
    [NotifyPropertyChangedFor(nameof(SoupMenuView))]
    [NotifyPropertyChangedFor(nameof(DessertMenuView))]
    private MenuModel menuModel;

    [ObservableProperty]
    private List<Dish> mainMenuView;
    [ObservableProperty]
    private List<Dish> sideMenuView; 
    [ObservableProperty]
    private List<Dish> soupMenuView;
    [ObservableProperty]
    private List<Dish> dessertMenuView;

    public string[] DatesStringView => MenuModel.DatesString;
    public MenuViewModel(string url = "/gastronomie/speiseplaene/mensa-basilica-hamm/")
    {
        MenuModel = new MenuModel(url);
        GetMenus();
        SettingsModel.UserAllergyIngredientList.CollectionChanged += UserAllergyIngredientList_CollectionChanged;

    }
    public void GetMenus()
    {
        MainMenuView = MenuModel.MainMenu;
        SideMenuView = MenuModel.SideMenu;
        SoupMenuView = MenuModel.SoupMenu;
        DessertMenuView = MenuModel.DessertMenu;
    }

    [RelayCommand]
    public void ChangeSelectedDate(object date)
    {
        int counter = 0;
        foreach (var dateString in MenuModel.DatesString)
        {
            if (dateString == date.ToString())
                break;
            counter++;
        }
        MenuModel = new MenuModel(MenuModel.DatesURL[counter]);
        GetMenus();
        UpdateAllergies();
    }

    private void UserAllergyIngredientList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        CheckMenu(MainMenuView);
        CheckMenu(SideMenuView);
        CheckMenu(SoupMenuView);
        CheckMenu(DessertMenuView);
    }
    public void UpdateAllergies()
    {
        CheckMenu(MainMenuView);
        CheckMenu(SideMenuView);
        CheckMenu(SoupMenuView);
        CheckMenu(DessertMenuView);
    }
    public static void CheckMenu(List<Dish> menu)
    {
        foreach (var dish in menu)
        {
            foreach (var ingredient in dish.Ingredients)
            {
                ingredient.AllergyWarningColor = Colors.White;

                if (SettingsModel.UserAllergyIngredientList.Count > 0)
                {
                    foreach (var allergy in SettingsModel.UserAllergyIngredientList)
                    {
                        if (ingredient.Name == allergy)
                        {
                            ingredient.AllergyWarningColor = Colors.Red;
                            break;
                        }
                    }
                }
            }
        }
    }

    [RelayCommand]
    public void ChangeSelectedDishes(IList<Dish> selectedDishes)
    {

    }

}

