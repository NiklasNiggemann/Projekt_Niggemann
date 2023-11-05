using HtmlAgilityPack;
using Mensa_App.MVVM.Models;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.Classes.Models;

public class Menu
{
    public Menu()
    {
        MainMenu = new List<Dish>();
        SideMenu = new List<Dish>();
        SoupMenu = new List<Dish>();
        DessertMenu = new List<Dish>();
        UserMenu = new Dish[4];
        HtmlWeb web = new HtmlWeb();
        try
        {
            HtmlDocument document = web.Load("https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm/");
            GenerateIndividualMenus(document, ".main-dishes");
            GenerateIndividualMenus(document, ".side-dishes");
            GenerateIndividualMenus(document, ".soups");
        }
        catch (System.Net.WebException ex)
        {

        }
    }
    
    public List<Dish> MainMenu { get; set; }
    public List<Dish> SideMenu { get; set; }
    public List<Dish> SoupMenu { get; set; }
    public List<Dish> DessertMenu { get; set; }
    public Dish UserMainMenu { get; set; }
    public Dish[] UserMenu { get; set; }
    
    public void GenerateIndividualMenus(HtmlDocument document, string dishType)
    {
        switch (dishType)
        {
            case ".main-dishes":
                MainMenu = Dish.GenerateList(document, dishType);
                break;
            case ".side-dishes":
                SideMenu = Dish.GenerateList(document, dishType);
                break;
            case ".soups":
                SoupMenu = Dish.GenerateList(document, dishType);
                DessertMenu = Dish.DivideDessertsFromSoupMenu(SoupMenu);
                SoupMenu = Dish.DeleteDessertsFromSoupMenu(SoupMenu);
                break;
        }
    }
}
