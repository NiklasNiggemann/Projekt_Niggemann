using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mensa_App.MVVMS.Models;

internal partial class SelectionModel 
{
    public static ObservableCollection<Dish> SelectedMainDishes => [];
    public static ObservableCollection<Dish> SelectedSideDishes => [];
    public static ObservableCollection<Dish> SelectedSoupDishes => [];
    public static ObservableCollection<Dish> SelectedDessertDishes => [];
    public static double TotalPrice => 0;

}
