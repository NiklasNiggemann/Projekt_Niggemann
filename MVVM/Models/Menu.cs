using HtmlAgilityPack;
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
    public string? Date { get; set; }
    public DateTime CurrentTime { get; set; }
    public string CurrentDate
    {
        get
        {
            var culture = new CultureInfo("de-DE");
            return culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek) + ", der " + CurrentTime.ToString("dd.MM.yyyy") + ", ";
        }
    }
    public string CurrentTimeString
    {
        get
        {
            return " " + CurrentTime.ToString("t") + " Uhr.";
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
            DateTime openingTime = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, 14, 0, 0);
            return openingTime;
        }
    }
    private bool willOpen = false;
    private bool willClose = false;
    private bool isWeekend = false;
    public string IsOpenString
    {
        get
        {
            if (CurrentTime.DayOfWeek == DayOfWeek.Saturday || CurrentTime.DayOfWeek == DayOfWeek.Sunday)
            {
                willOpen = false;
                willClose = false;
                isWeekend = true;
                return "Die Mensa öffnet erst am Montag wieder.";
            }
            if (CurrentTime.Hour == 11)
            {
                if (CurrentTime.Minute >= 30)
                {
                    willOpen = false;
                    willClose = true;
                    isWeekend = false;
                    return "Die Mensa ist geöffnet.";
                }
                else
                {
                    willOpen = true;
                    willClose = false;
                    isWeekend = false;
                    return "Die Mensa ist geschlossen";
                }
            }
            else if (CurrentTime.Hour > OpeningTime.Hour && CurrentTime.Hour < ClosingTime.Hour)
            {
                willOpen = false;
                willClose = true;
                isWeekend = false;
                return "Die Mensa ist geöffnet.";
            }
            else
            {
                willOpen = true;
                willClose = false;
                isWeekend = false;
                if (CurrentTime.Hour > ClosingTime.Hour)
                {
                    willOpen = false;
                    willClose = false;
                    isWeekend = false;
                }
            }
            return "Die Mensa ist geschlossen.";
        }
    }
    public string TimeTillOpenOrClosed
    {
        get
        {
            if (willOpen && !isWeekend)
            {
                TimeSpan ts = OpeningTime - CurrentTime;
                return "Sie wird in " + ts.ToFormattedString("t") + "h öffnen.";
            }
            else if (willClose && !isWeekend)
            {
                TimeSpan ts = ClosingTime - CurrentTime;
                return "Sie wird in " + ts.ToFormattedString("t") + "h schließen.";
            }
            else if (!willClose && !isWeekend)
                return "Sie wird morgen wieder öffnen.";
            return "";
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
