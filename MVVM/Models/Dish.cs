using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;

namespace Mensa_App.Classes.Models;

public class Dish
{
    public string? Name { get; set; }
    public double? Price { get; set; }
    public string PriceString
    {
        get
        {
            return String.Format("{0:0.00}", Price) + "€";
        }
    }
    public string? Ingredients { get; set; }
    public string? Nutritions { get; set; }
    public Dish(string name, double price, string ingredients, string nutritions)
    {
        Name = name;
        Price = price;
        Ingredients = ingredients;
        Nutritions = nutritions;
    }
    public static List<Dish> DeleteDessertsFromSoupMenu(List<Dish> soupMenu)
    {
        List<Dish> cleanSoupMenu = new List<Dish>();
        foreach (var soup in soupMenu)
        {
            if (soup.Price > 2)
            {
                cleanSoupMenu.Add(soup);
            }
        }
        return cleanSoupMenu;
    }
    public static List<Dish> DivideDessertsFromSoupMenu(List<Dish> soupMenu)
    {
        List<Dish> dessertMenu = new List<Dish>();
        foreach (var soup in soupMenu)
        {
            if (soup.Price < 2)
            {
                dessertMenu.Add(soup);
            }
        }
        return dessertMenu;
    }
    public static Dish Generate(IList<HtmlNode> nameAndPrice, IList<HtmlNode> ingredientsAndNutritions, int index)
    {
        string name = HtmlEntity.DeEntitize(nameAndPrice[index].QuerySelector("h4").InnerText);
        name = CleanUpString("name", name);

        string priceString = HtmlEntity.DeEntitize(nameAndPrice[index].QuerySelector(".price").InnerText);
        priceString = CleanUpString("price", priceString);
        double price = Convert.ToDouble(priceString);

        string ingredientsString = HtmlEntity.DeEntitize(ingredientsAndNutritions[index].QuerySelector(".ingredients").InnerText);
        ingredientsString = CleanUpString("ingredients", ingredientsString);

        string nutritionsString = HtmlEntity.DeEntitize(ingredientsAndNutritions[index].QuerySelector(".nutritions").InnerText);
        nutritionsString = CleanUpString("nutritions", nutritionsString);

        Dish dish = new Dish(name, price / 100, ingredientsString, nutritionsString);
        return dish;
    }
    public static string CleanUpString(string elementType, string element)
    {
        switch (elementType)
        {
            case "name":
                return element.Trim();
            case "price":
                string priceString = element.Substring(element.IndexOf(":") + 2, 4).Replace(",", ".");
                return priceString;
            case "ingredients":
                element = element.Trim();
                element = element.Remove(0, element.IndexOf(" ")).Trim();
                if (element.Contains("mit")) element = element.Remove(element.IndexOf("m"), 4);
                return element;
            case "nutritions":
                element = element.Trim();
                element = element.Remove(0, element.IndexOf(" ")).Trim();
                foreach (var x in element)
                {
                    if (x == ')')
                    {
                        int i = element.IndexOf(x);
                        element = element.Insert(element[i], ",");
                    }
                }
                return element;
            default:
                return "ERROR";
        }
    }
    // DishTypes: .main-dishes, .side-dishes & soups 
    public static List<Dish> GenerateList(HtmlDocument document, string dishType)
    {
        List<Dish> mainDishesList = new List<Dish>();
        IList<HtmlNode> nameAndPrice = document.DocumentNode.QuerySelectorAll($"{dishType} .odd");
        IList<HtmlNode> ingredientsAndNutritions = document.DocumentNode.QuerySelectorAll($"{dishType} .even");
        for (int i = 0; i < nameAndPrice.Count; i++)
        {
            Dish mainDish = Dish.Generate(nameAndPrice, ingredientsAndNutritions, i);
            mainDishesList.Add(mainDish);
        }
        return mainDishesList;
    }
}
