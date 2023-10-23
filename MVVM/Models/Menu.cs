using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.Classes.Models;

public class Menu
{
    public string? Date { get; set; }
    public DateTime UserTimeDateTime { get; set; }
    public string UserTimeString
    {
        get
        {
            return this.UserTimeDateTime.ToString("t") + " Uhr, ";
        }
    }
    public string CheckIfOpen
    {
        get
        {
            if (UserTimeDateTime.Hour < ClosingTime.Hour)
            {
                if (UserTimeDateTime.Hour > OpeningTime.Hour)
                {
                    if (UserTimeDateTime.Hour == 11)
                    {
                        if (UserTimeDateTime.Minute >= OpeningTime.Minute)
                            return " die Mensa ist geöffnet.";
                        else
                            return " die Mensa ist geschlossen.";
                    }
                    return " die Mensa ist geöffnet.";
                }
                return " die Mensa ist geschlossen.";
            }
            return " die Mensa ist geschlossen.";
        }
    }
    public DateTime OpeningTime
    {
        get
        {
            DateTime dateTime = new DateTime(2000, 01, 01, 11, 30, 0, 0);
            return dateTime;
        }
    }
    public DateTime ClosingTime
    {
        get
        {
            DateTime dateTime = new DateTime(2000, 01, 01, 13, 00, 0, 0);
            return dateTime;
        }
    }
    public List<Dish>? MainMenu { get; set; }
    public List<Dish>? SideMenu { get; set; }
    public List<Dish>? SoupMenu { get; set; }
    public List<Dish>? DessertMenu { get; set; }
    public Menu()
    {
        MainMenu = new List<Dish>();
        SideMenu = new List<Dish>();
        SoupMenu = new List<Dish>();
        DessertMenu = new List<Dish>();
        HtmlWeb web = new HtmlWeb();
        UserTimeDateTime = new DateTime();
        UserTimeDateTime = DateTime.Now;
        HtmlDocument document = web.Load("https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm/");
        GenerateIndividualMenus(document, ".main-dishes");
        GenerateIndividualMenus(document, ".side-dishes");
        GenerateIndividualMenus(document, ".soups");
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
}
