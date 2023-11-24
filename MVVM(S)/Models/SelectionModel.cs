using Mensa_App.Classes;
using System.ComponentModel;

namespace Mensa_App.MVVMS.Models;

internal static class SelectionModel 
{
    private static Dish selectedMain;
    public static Dish SelectedMain
    {
        get => selectedMain;
        set
        {
            selectedMain = value;
            PropertyChanged?.Invoke(SelectedMain, new PropertyChangedEventArgs(nameof(SelectedMain)));
        }
    }
    private static Dish selectedSide;
    public static Dish SelectedSide
    {
        get => selectedSide;
        set
        {
            selectedSide = value;
            PropertyChanged?.Invoke(SelectedSide, new PropertyChangedEventArgs(nameof(SelectedSide)));
        }
    }
    private static Dish selectedSoup;
    public static Dish SelectedSoup
    {
        get => selectedSoup;
        set
        {
            selectedSoup = value;
            PropertyChanged?.Invoke(SelectedSoup, new PropertyChangedEventArgs(nameof(SelectedSoup)));
        }
    }
    private static Dish selectedDessert;
    public static Dish SelectedDessert
    {
        get => selectedDessert;
        set
        {
            selectedDessert = value;
            PropertyChanged?.Invoke(SelectedDessert, new PropertyChangedEventArgs(nameof(SelectedDessert)));
        }
    }
    private static double totalPrice;
    private static List<Dish> selectedDishes 
    public static double TotalPrice
    {
        get => totalPrice;
        set
        {

        }
    }

    public static event PropertyChangedEventHandler PropertyChanged;
}
