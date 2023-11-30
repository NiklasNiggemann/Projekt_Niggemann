using Mensa_App.Classes;
using Mensa_App.MVVMS.Models;
using Mensa_App.MVVMS.ViewModels;

namespace Mensa_App.MVVMS.Views;

public partial class MainMenuView : ContentPage
{
    public MainMenuView()
    {
        InitializeComponent();
        BindingContext = new MenuViewModel();
    }
    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (var selectedDish in e.PreviousSelection)
        {
            SelectionModel.SelectedDishes.Remove(selectedDish as Dish);
        }
        foreach (var selectedDish in e.CurrentSelection)
        {
            SelectionModel.SelectedDishes.Add(selectedDish as Dish);
        }
    }
}