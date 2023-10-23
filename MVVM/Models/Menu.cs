using HtmlAgilityPack;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.Classes.Models;

public class Menu
{
    public string? Date { get; set; }
    public DateTime CurrentTime { get; set; }
    public string CurrentTimeString
    {
        get
        {
            return "Es ist " + CurrentTime.ToString("t") + " Uhr, ";
        }
    }
    public DateTime OpeningTime
    {
        get
        {
            DateTime openingTime = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, 11, 30, 0);
            return openingTime;
        }
    }
    public DateTime ClosingTime
    {
        get
        {
            DateTime openingTime = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, 13, 0, 0);
            return openingTime;
        }
    }
    public string IsOpenString
    {
        get
        {
            if (CurrentTime.DayOfWeek == DayOfWeek.Saturday || CurrentTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return " die Mensa öffnet erst am Montag wieder.";
            }
            if (CurrentTime.Hour == 11)
            {
                if (CurrentTime.Minute >= 30)
                {
                    return " die Mensa ist geöffnet.";
                }
            }
            else if (CurrentTime.Hour > OpeningTime.Hour && CurrentTime.Hour < ClosingTime.Hour)
            {
                return " die Mensa ist geöffnet.";
            }
            return " die Mensa ist geschlossen.";
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
        CurrentTime = new DateTime();
        CurrentTime = DateTime.Now;
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
