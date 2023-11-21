using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.MVVM.Models;
using Mensa_App.Classes;
using CommunityToolkit.Mvvm.Input;

namespace Mensa_App.MVVM.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    public MenuModel MenuModel { get; set; }
    public MenuViewModel()
    {
        MenuModel = new MenuModel();
        MainMenuView = MenuModel.MainMenu;
    }
    [RelayCommand]
    public void ChangeSelectedMain(Dish dish)
    {
        SelectedMain_ViewModel = dish;
        Console.WriteLine();
    }
    public Dish SelectedMain_ViewModel
    {
        get => MenuModel.SelectedMain;
        set
        {
            MenuModel.SelectedMain = value;
            OnPropertyChanged(nameof(MenuModel.SelectedMain));
        }
    }
    public Dish SelectedSide_ViewModel
    {
        get => MenuModel.SelectedSide;
        set
        {
            MenuModel.SelectedSide = value;
            OnPropertyChanged(nameof(MenuModel.SelectedSide));
        }
    }
    public Dish SelectedSoup_ViewModel
    {
        get => MenuModel.SelectedSoup; set
        {
            MenuModel.SelectedSoup = value;
            OnPropertyChanged(nameof(MenuModel.SelectedSoup));
        }
    }
    public Dish SelectedDessert_ViewModel
    {
        get => MenuModel.SelectedDessert; set
        {
            MenuModel.SelectedDessert = value;
            OnPropertyChanged(nameof(MenuModel.SelectedDessert));
        }
    }
    public List<Dish> MainMenuView { get; set; }
}
