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
        this.SelectedDishes = [];
        SelectionModel.SelectedDishes.CollectionChanged += SelectedDishes_CollectionChanged;
    }

    private void SelectedDishes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        this.SelectedDishes = SelectionModel.SelectedDishes;
        TotalPrice = SelectionModel.TotalPrice;
    }

    [ObservableProperty]
    private ObservableCollection<Dish> selectedDishes;
    [ObservableProperty]
    private double totalPrice; 
}
