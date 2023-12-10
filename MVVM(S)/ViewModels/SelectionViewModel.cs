using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.Classes;
using Mensa_App.MVVMS.Models;
using Mensa_App.MVVMS.Views;
using System.Collections.ObjectModel;

namespace Mensa_App.MVVMS.ViewModels;

public partial class SelectionViewModel : ObservableObject
{
    public SelectionViewModel()
    {
        SelectionModel selectionModel = new();
        SelectedMainDishes = [];
        SelectedSideDishes = SelectionModel.SelectedSideDishes;
        SelectedSoupDishes = SelectionModel.SelectedSoupDishes;
        SelectedDessertDishes = SelectionModel.SelectedDessertDishes;
    }
    public static ObservableCollection<Dish> SelectedMainDishes { get; set; }
    public static ObservableCollection<Dish> SelectedSideDishes { get; set; }
    public static ObservableCollection<Dish> SelectedSoupDishes { get; set; }
    public static ObservableCollection<Dish> SelectedDessertDishes { get; set; }

    [ObservableProperty]
    private double totalPrice;
}
