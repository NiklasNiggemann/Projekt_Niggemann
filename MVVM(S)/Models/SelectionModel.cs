﻿using Mensa_App.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mensa_App.MVVMS.Models;

internal class SelectionModel 
{
    public static ObservableCollection<Dish> SelectedDishes { get; set; }
    public static double TotalPrice { get; set; }
    public SelectionModel()
    {
        SelectedDishes = new ObservableCollection<Dish>();
        SelectedDishes.CollectionChanged += SelectedDishes_CollectionChanged;
    }

    private void SelectedDishes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        TotalPrice = 0;
        foreach (var x in SelectedDishes)
        {
            if (x is not null)
                TotalPrice += x.Price;
        }
    }

    public static event PropertyChangedEventHandler PropertyChanged;
}
