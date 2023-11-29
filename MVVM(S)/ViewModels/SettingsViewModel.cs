using CommunityToolkit.Mvvm.ComponentModel;
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
    [ObservableProperty]
    ObservableCollection<string> ingredientList; 
}
