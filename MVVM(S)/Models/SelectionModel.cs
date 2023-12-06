using Mensa_App.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mensa_App.MVVMS.Models;

internal class SelectionModel 
{
    public static ObservableCollection<Dish> SelectedMainDishes { get; set; }
    public static ObservableCollection<Dish> SelectedSideDishes { get; set; }
    public static ObservableCollection<Dish> SelectedSoupDishes { get; set; }
    public static ObservableCollection<Dish> SelectedDessertDishes { get; set; }
    public static double TotalPrice { get; set; }
    public SelectionModel()
    {
        SelectedMainDishes = [];
        SelectedSideDishes = [];
        SelectedSoupDishes = [];
        SelectedDessertDishes = [];
    }

}
