using Mensa_App.Classes.Models;
using Mensa_App.Classes.ViewModels;
using System.Diagnostics;
using System.Windows.Input;

namespace Mensa_App.Classes.View;

public partial class MenuView : ContentPage
{
    MenuViewModel MenuViewModel {  get; set; }
    public List<object> SelectedMains { get; set; } = new List<object>();
    public double? SelectedMainsSum { get; set; }
    public MenuView()
	{
		InitializeComponent();
        MenuViewModel = new MenuViewModel();
        BindingContext = MenuViewModel;
	}

    public ICommand SelectedMainsCommand => new Command(() => 
    {
        var selectedList = SelectedMains;
        foreach (var x in SelectedMains)
        {
            Dish y = x as Dish;
            SelectedMainsSum += y.Price;
        }
    });

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var element = e.CurrentSelection;
    }
}