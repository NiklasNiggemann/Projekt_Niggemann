using Mensa_App.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mensa_App.MVVMS.Models;

internal class SelectionModel 
{
    public static ObservableCollection<Dish> SelectedDishes => [];
    public static double TotalPrice { get; set; }
}
