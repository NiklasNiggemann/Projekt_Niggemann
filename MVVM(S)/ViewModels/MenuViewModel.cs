﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.MVVMS.Models;
using Mensa_App.Classes;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mensa_App.MVVMS.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    private MenuModel menuModel;
    public List<Dish> MainMenuView { get; set; }
    public List<Dish> SideMenuView { get; set; }
    public List<Dish> SoupMenuView { get; set; }
    public List<Dish> DessertMenuView { get; set; }


    public string[] DatesStringView => menuModel.DatesString;
    public MenuViewModel(string url = "/gastronomie/speiseplaene/mensa-basilica-hamm/")
    {
        menuModel = new(url);
        GetMenus();
        SettingsModel.UserAllergyIngredientList.CollectionChanged += UserAllergyIngredientList_CollectionChanged;

    }
    public void GetMenus()
    {
        MainMenuView = menuModel.MainMenu;
        SideMenuView = menuModel.SideMenu;
        SoupMenuView = menuModel.SoupMenu;
        DessertMenuView = menuModel.DessertMenu;
    }
    [RelayCommand]
    public void ChangeSelectedDate(object date)
    {
        int counter = 0;
        foreach (var dateString in menuModel.DatesString)
        {
            if (dateString == date.ToString())
                break;
            counter++;
        }
        menuModel = new MenuModel(menuModel.DatesURL[counter]);
        GetMenus();
        UpdateAllergies();
    }
    private void UserAllergyIngredientList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateAllergies();
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
    public static void ChangeSelectedDishes(Dish dish)
    {
        SelectionModel.SelectedDishes.Add(dish);
    }
}

