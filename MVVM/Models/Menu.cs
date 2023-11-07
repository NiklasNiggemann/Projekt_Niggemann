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
    ~Menu() { }
    public Menu()
    {
        MainMenu = new List<Dish>();
        SideMenu = new List<Dish>();
        SoupMenu = new List<Dish>();
        DessertMenu = new List<Dish>();
        UserMenu = new Dish[4];
        DatesString = new string[5];
        DatesURL = new string[4];
        HtmlWeb web = new HtmlWeb();
        try
        {
            HtmlDocument document = web.Load("https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm/");
            GetDates();
            GenerateIndividualMenus(document, ".main-dishes");
            GenerateIndividualMenus(document, ".side-dishes");
            GenerateIndividualMenus(document, ".soups");
        }
        catch (System.Net.WebException ex)
        {

        }
    }
    public Menu(string URL)
    {
        MainMenu = new List<Dish>();
        SideMenu = new List<Dish>();
        SoupMenu = new List<Dish>();
        DessertMenu = new List<Dish>();
        UserMenu = new Dish[4];
        DatesString = new string[5];
        DatesURL = new string[4];
        HtmlWeb web = new HtmlWeb();
        try
        {
            HtmlDocument document = web.Load($"https://www.studierendenwerk-pb.de/{URL}");
            GetDates();
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

    public string[] DatesString { get; set; }
    public string[] DatesURL { get; set; }
    public void GetDates()
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = web.Load("https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm/");
        DatesString[0] = HtmlEntity.DeEntitize(document.QuerySelector(".desktop-form .active").InnerText);

        IList<HtmlNode> datesNode = document.DocumentNode.QuerySelectorAll(".desktop-form a");
        string[] dates = new string[datesNode.Count];
        int i = 0;
        foreach (var x in datesNode)
        {
            dates[i] = HtmlEntity.DeEntitize(x.InnerText);
            i++;
        }
        i = 1;
        foreach (var x in dates)
        {
            DatesString[i] = x.Trim();
            i++;
        }

        string[] datesURL = new string[datesNode.Count];
        i = 0;
        foreach (var x in datesNode)
        {
            HtmlAttributeCollection getURLCollection = x.Attributes;
            datesURL[i] = HtmlEntity.DeEntitize(getURLCollection[0].Value);
            i++;
        }
        i = 0;
        foreach (var x in datesURL)
        {
            DatesURL[i] = x.Trim();
            i++;
        }
    }
}
