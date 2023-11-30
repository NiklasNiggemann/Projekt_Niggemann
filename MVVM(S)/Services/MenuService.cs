﻿using HtmlAgilityPack;
using Mensa_App.Classes;

namespace Mensa_App.MVVMS.Services;

public class MenuService
{
    public HtmlDocument Document = new HtmlWeb().Load("https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm/");
    public MenuService()
    {
        MainMenu = new List<Dish>();
        SideMenu = new List<Dish>();
        SoupMenu = new List<Dish>();
        DessertMenu = new List<Dish>();
        DatesString = new string[5];
        DatesURL = new string[4];
        GetDates();
        GenerateMenus();
    }
    public void CleanMenus()
    {
        MainMenu.Clear();
        SideMenu.Clear();
        SoupMenu.Clear();
        DessertMenu.Clear();
    }
    public void GenerateMenus()
    {
        GenerateIndividualMenus(Document, ".main-dishes");
        GenerateIndividualMenus(Document, ".side-dishes");
        GenerateIndividualMenus(Document, ".soups");
    }
    public List<Dish> MainMenu { get; set; }
    public List<Dish> SideMenu { get; set; }
    public List<Dish> SoupMenu { get; set; }
    public List<Dish> DessertMenu { get; set; }
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
        HtmlWeb web = new();
        HtmlDocument document = web.Load("https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm/");
        DatesString[0] = HtmlEntity.DeEntitize(document.QuerySelector(".desktop-form .active").InnerText);

        IList<HtmlNode> datesNode = document.DocumentNode.QuerySelectorAll(".desktop-form a");
        string[] dates = new string[datesNode.Count];
        int i = 0;
        foreach (var node in datesNode)
        {
            dates[i] = HtmlEntity.DeEntitize(node.InnerText);
            i++;
        }
        i = 1;
        foreach (var date in dates)
        {
            DatesString[i] = date.Trim();
            i++;
        }
        string[] datesURL = new string[datesNode.Count];
        i = 0;
        foreach (var node in datesNode)
        {
            HtmlAttributeCollection getURLCollection = node.Attributes;
            datesURL[i] = HtmlEntity.DeEntitize(getURLCollection[0].Value);
            i++;
        }
        i = 0;
        foreach (var url in datesURL)
        {
            DatesURL[i] = url.Trim();
            i++;
        }
    }
}
