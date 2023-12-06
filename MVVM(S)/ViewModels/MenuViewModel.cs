using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.MVVMS.Models;
using Mensa_App.Classes;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Collections.ObjectModel;

namespace Mensa_App.MVVMS.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MainMenuView))]
    [NotifyPropertyChangedFor(nameof(SideMenuView))]
    [NotifyPropertyChangedFor(nameof(SoupMenuView))]
    [NotifyPropertyChangedFor(nameof(DessertMenuView))]
    private MenuModel menuModel;
    public List<Dish> MainMenuView
    {
        get => MenuModel.MainMenu;
    }

    public List<Dish> SideMenuView
    {
        get => MenuModel.SideMenu;
    }

    public List<Dish> SoupMenuView
    {
        get => MenuModel.SoupMenu;
    }

    public List<Dish> DessertMenuView
    {
        get => MenuModel.DessertMenu;
    }
    public string[] DatesStringView { get; set; }
    public MenuViewModel(string url = "/gastronomie/speiseplaene/mensa-basilica-hamm/")
    {
        MenuModel = new MenuModel(url);
        DatesStringView = MenuModel.DatesString;
        SettingsModel.UserAllergyIngredientList.CollectionChanged += UserAllergyIngredientList_CollectionChanged;
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

        this.MenuModel = new MenuModel(MenuModel.DatesURL[counter]);
    }
    private void UserAllergyIngredientList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        foreach (var dish in MainMenuView)
        {
            foreach (var ingredient in dish.Ingredients)
            {
                ingredient.AllergyWarningColor = Colors.White;
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

    [RelayCommand]
    public void ChangeSelectedDishes(IList<Dish> selectedDishes)
    {

    }

}

