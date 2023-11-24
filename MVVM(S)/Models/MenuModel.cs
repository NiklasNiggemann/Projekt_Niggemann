using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.Classes;
using Mensa_App.MVVMS.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mensa_App.MVVMS.Models;

public partial class MenuModel 
{
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
