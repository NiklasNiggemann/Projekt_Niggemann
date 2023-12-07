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
    public ObservableCollection<Dish> MainMenuView { get; set; }
    public ObservableCollection<Dish> SideMenuView { get; set; }
    public ObservableCollection<Dish> SoupMenuView { get; set; }
    public ObservableCollection<Dish> DessertMenuView { get; set; }
    public string[] DatesStringView => MenuModel.DatesString;
    public MenuViewModel(string url = "/gastronomie/speiseplaene/mensa-basilica-hamm/")
    {
        MenuModel = new MenuModel(url);
        GetMenus();

        SettingsModel.UserAllergyIngredientList.CollectionChanged += UserAllergyIngredientList_CollectionChanged;

    }
    public void GetMenus()
    {
        MainMenuView = new(MenuModel.MainMenu);
        SideMenuView = new(MenuModel.SideMenu);
        SoupMenuView = new(MenuModel.SoupMenu);
        DessertMenuView = new(MenuModel.DessertMenu);
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
    }

    private void UserAllergyIngredientList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        CheckMenu(MainMenuView);
        CheckMenu(SideMenuView);
        CheckMenu(SoupMenuView);
        CheckMenu(DessertMenuView);
    }
    public static void CheckMenu(ObservableCollection<Dish> menu)
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

