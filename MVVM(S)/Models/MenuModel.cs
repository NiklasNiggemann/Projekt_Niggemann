using CommunityToolkit.Mvvm.ComponentModel;
using HtmlAgilityPack;
using Mensa_App.Classes;
using Mensa_App.MVVM.Services;
using Mensa_App.MVVM.ViewModels;

namespace Mensa_App.MVVM.Models;

public partial class MenuModel : ObservableObject
{
    [ObservableProperty]
    private Dish selectedMain;
    [ObservableProperty]
    private Dish selectedSide;
    [ObservableProperty]
    private Dish selectedSoup;
    [ObservableProperty]
    private Dish selectedDessert;
    public MenuModel()
    {
        MenuService = new MenuService();
        MainMenu = MenuService.MainMenu;
        SideMenu = MenuService.SideMenu;
        SoupMenu = MenuService.SoupMenu;
        DessertMenu = MenuService.DessertMenu;
        DatesString = MenuService.DatesString;
        DatesURL = MenuService.DatesURL;
    }
    public MenuService MenuService { get; set; }
    public List<Dish> MainMenu { get; set; }
    public List<Dish> SideMenu { get; set; }
    public List<Dish> SoupMenu { get; set; }
    public List<Dish> DessertMenu { get; set; }
    public string[] DatesString { get; set; }
    public string[] DatesURL { get; set; }
}
