using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.Classes;
using Mensa_App.MVVMS.Models;
using Mensa_App.MVVMS.Views;

namespace Mensa_App.MVVMS.ViewModels;

public partial class SelectionViewModel : ObservableObject
{
    public SelectionViewModel()
    {
        SelectionModel.PropertyChanged += MenuModel_PropertyChanged;
    }

    private void MenuModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch(e.PropertyName)
        {
            case "SelectedMain":
                SelectedMain = SelectionModel.SelectedMain;
                break;
            case "SelectedSide":
                SelectedSide = SelectionModel.SelectedSide;
                break;
            case "SelectedSoup":
                SelectedSoup = SelectionModel.SelectedSoup;
                break;
            case "SelectedDessert":
                SelectedDessert = SelectionModel.SelectedDessert;
                break;
            default:
                break;
        }
        
    }
    [ObservableProperty]
    private Dish selectedMain;
    [ObservableProperty]
    private Dish selectedSide;
    [ObservableProperty]
    private Dish selectedSoup;
    [ObservableProperty]
    private Dish selectedDessert;
    [ObservableProperty]
    private double totalPrice;
}
