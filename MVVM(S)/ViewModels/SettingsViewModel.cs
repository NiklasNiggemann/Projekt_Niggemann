using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mensa_App.Classes;
using Mensa_App.MVVMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.MVVMS.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    public SettingsViewModel()
    {
        ingredientList = SettingsModel.GeneralIngredientsList;
    }
    [RelayCommand]
    public void AllergyListUpdated(IList<object> selectedIngredients)
    {
        foreach (var ingredients in selectedIngredients)
        {
            SettingsModel.UserAllergyIngredientList.Remove(ingredients.ToString());
        }
        foreach (var ingredients in selectedIngredients)
        {
            SettingsModel.UserAllergyIngredientList.Add(ingredients.ToString());
        }
    }
    [ObservableProperty]
    ObservableCollection<string> ingredientList; 
}
