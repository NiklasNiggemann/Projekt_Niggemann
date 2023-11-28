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
        foreach (var x in e.PreviousSelection)
        {
            SelectionModel.SelectedDishes.Remove(x as Dish);
        }
        foreach (var x in e.CurrentSelection)
        {
            SelectionModel.SelectedDishes.Add(x as Dish);
        }
    }
}