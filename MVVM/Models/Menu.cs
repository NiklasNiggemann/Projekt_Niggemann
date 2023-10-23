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
    public DateTime UserTimeDateTime { get; set; }
    private bool IsOpen = false;
    private bool WillOpen = false;
    private bool WillClose = false;
    private bool IsClosedForToday = false;
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
                if (UserTimeDateTime.Hour >= OpeningTime.Hour)
                {
                    IsOpen = true;
                    WillClose = true;
                    if (UserTimeDateTime.Hour == 11)
                    {
                        if (UserTimeDateTime.Minute >= OpeningTime.Minute)
                        {
                            WillClose = true;
                            IsOpen = true;
                        }
                        else
                        {
                            WillClose = false;
                            IsOpen = false;
                            IsClosedForToday = true;
                        }
                    }
                }
                IsOpen = false;
                WillOpen = true;
            }
            else
            {
                IsOpen = false;
                WillOpen = false;
                IsClosedForToday = true;
            }

            if (IsOpen)
                return " die Mensa ist geöffnet.";
            else
                return " die Mensa ist geschlossen.";
        }
    }
    public TimeSpan TimeTillOpen
    {
        get
        {
            TimeSpan span = new TimeSpan();
            if (WillOpen)
            {
                span = OpeningTime - UserTimeDateTime;
            }
            return span;
        }
    }
    public string TimeTillOpenString
    {
        get
        {
            if (WillOpen)
                return "Sie öffnet in " + TimeTillOpen.ToFormattedString("t") + "h.";
            return "";
        }
    }
    public TimeSpan TimeTillClosed
    {
        get
        {
            TimeSpan span = new TimeSpan();
            if (WillClose)
            {
                span = UserTimeDateTime - ClosingTime;
            }
            return span;
        }
    }
    public string TimeTillClosedString
    {
        get
        {
            if (WillClose)
                return "Sie schließt in " + TimeTillOpen.ToFormattedString("t") + "h.";
            else
                return "Die Mensa öffnet erst morgen wieder."
        }
    }
    public DateTime OpeningTime
    {
        get
        {
            DateTime dateTime = new DateTime(2023, 10, 23, 11, 30, 0, 0);
            return dateTime;
        }
    }
    public DateTime ClosingTime
    {
        get
        {
            DateTime dateTime = new DateTime(2023, 10, 23, 13, 00, 0, 0);
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
