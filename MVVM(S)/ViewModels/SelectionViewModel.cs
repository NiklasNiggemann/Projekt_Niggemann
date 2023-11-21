using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mensa_App.Classes;
using Mensa_App.MVVM.Models;

namespace Mensa_App.MVVM.ViewModels;

public partial class SelectionViewModel : ObservableObject
{
    public MenuModel MenuModel { get; set; }
    public SelectionViewModel()
    {
        MenuModel = new MenuModel();
        MenuModel.PropertyChanged += MenuModel_PropertyChanged; 
    }
    private void MenuModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SelectedMain)) 
            OnPropertyChanged(nameof(SelectedMain));
    }
    public Dish SelectedMain
    {
        get => MenuModel.SelectedMain;
    }
    public Dish SelectedSide
    {
        get => MenuModel.SelectedSide;
    }
    public Dish SelectedSoup
    {
        get => MenuModel.SelectedSoup;
    }
    public Dish SelectedDessert
    {
        get => MenuModel.SelectedDessert;
    }
}
