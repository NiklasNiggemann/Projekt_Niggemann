using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.Classes
{
    internal class Menu
    {
        public string? Date { get; set; }
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
                    MainMenu = Dish.List(document, dishType);
                    break;
                case ".side-dishes":
                    SideMenu = Dish.List(document, dishType);
                    break;
                case ".soups":
                    SoupMenu = Dish.List(document, dishType);
                    break;
            }
        }
    }
}
