using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mensa_App.MVVMS.Models;

internal partial class SelectionModel 
{
    public static ObservableCollection<Dish> SelectedDishes { get; set; }
    public static double TotalPrice { get; set; }
    public SelectionModel()
    {
        SelectedDishes = [];
        SelectedDishes.CollectionChanged += SelectedDishes_CollectionChanged;
    }

    private void SelectedDishes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        TotalPrice = 0;
        foreach (var dish in SelectedDishes)
        {
            if (dish is not null)
                TotalPrice += dish.Price;
        }
    }
}
