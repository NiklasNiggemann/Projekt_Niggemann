using HtmlAgilityPack;
using Mensa_App.Classes;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mensa_App.MVVMS.Services;

public class MenuService
{
    public HtmlDocument Document { get; set; }
    public List<Dish> MainMenu { get; set; }
    public List<Dish> SideMenu { get; set; }
    public List<Dish> SoupMenu { get; set; }
    public List<Dish> DessertMenu { get; set; }
    public string[] DatesString { get; set; }
    public string[] DatesURL { get; set; }
    public MenuService(string url)
    {
        Document = new HtmlWeb().Load("https://www.studierendenwerk-pb.de/" + url);
        MainMenu = [];
        SideMenu = [];
        SoupMenu = [];
        DessertMenu = [];
        DatesString = new string[5];
        DatesURL = new string[5];
        GetDates();
        GenerateMenus();
    }
    public void GenerateMenus()
    {
        GenerateIndividualMenus(Document, ".main-dishes");
        GenerateIndividualMenus(Document, ".side-dishes");
        GenerateIndividualMenus(Document, ".soups");
    }
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
    public void GetDates()
    {
        HtmlWeb web = new();
        HtmlDocument document = web.Load("https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm/");
        DatesString[0] = HtmlEntity.DeEntitize(Document.QuerySelector(".desktop-form .active").InnerText);

        IList<HtmlNode> datesNode = document.DocumentNode.QuerySelectorAll(".desktop-form a");
        string[] dates = new string[datesNode.Count];
        string[] datesURL = new string[datesNode.Count];

        DatesString[0] = DateTime.Today.ToShortDateString();
        DatesURL[0] = "/gastronomie/speiseplaene/mensa-basilica-hamm/";

        for (int i = 0; i < datesNode.Count; i++)
        {
            dates[i] = HtmlEntity.DeEntitize(datesNode[i].InnerText);
            DatesString[i+1] = dates[i].Trim();
            HtmlAttributeCollection getURLCollection = datesNode[i].Attributes;
            datesURL[i] = HtmlEntity.DeEntitize(getURLCollection[0].Value);
            DatesURL[i+1] = datesURL[i].Trim();
        }
    }
}
