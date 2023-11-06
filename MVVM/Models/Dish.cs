using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using Mensa_App.MVVM.Models;
using Microsoft.Maui.Controls;

namespace Mensa_App.Classes.Models;

public class Dish
{
    public string ImgURL { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string PriceString
    {
        get
        {
            return String.Format("{0:0.00}", Price) + " €";
        }
    }
    public string[] Ingredients { get; set; }
    public List<Ingredient> IngredientList
    {
        get
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            foreach (var x in Ingredients)
            {
                if (x.Trim() != "")
                {
                    ingredientList.Add(new Ingredient(x.Trim()));
                }
            }
            if (ingredientList[0].Name == "Für")
            {
                ingredientList = new List<Ingredient>();
                ingredientList.Add(new Ingredient("Keine Inhaltsstoffe angegeben."));
            }
            return ingredientList;
        }
    }
    public Nutrition Nutrition
    {
        get
        {
            string[] nutritionsArray = NutritionsString.Split(" ");
            List<string> nutritionsList = new List<string>();
            foreach (var x in nutritionsArray)
            {
                if (x != "")
                    nutritionsList.Add(x);
            }
            Nutrition nutritions = new Nutrition();
            nutritions.BrennwertString = nutritionsList[0].Trim();
            nutritions.KalorienString = nutritionsList[1].Trim();
            nutritions.FettString = nutritionsList[2].Trim();
            nutritions.KohlenhydrateString = nutritionsList[3].Trim();
            nutritions.EiweißString = nutritionsList[4].Trim();
            return nutritions;
        }
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
        string[] ingredientsArray = ingredientsString.Split(' ');

        string nutritionsString = HtmlEntity.DeEntitize(ingredientsAndNutritions[index].QuerySelector(".nutritions").InnerText);
        nutritionsString = CleanUpString("nutritions", nutritionsString);

        string imgURL = HtmlEntity.DeEntitize(nameAndPrice[index].QuerySelector("img").InnerText);

        Dish dish = new Dish(name, price / 100, ingredientsArray, nutritionsString, imgURL);
        return dish;
    }

    public static string PrepareIngredients(string element)
    {
        element = element.Trim();
        element = element.Remove(0, element.IndexOf(" ")).Trim();

        bool containsMit = true;
        while (containsMit == true)
        {
            int prevLength = element.Length;
            if (element.Contains("mit "))
            {
                element = element.Remove(element.IndexOf("mit "), 4);
            }
            if (element.Length == prevLength)
                containsMit = false;
        }

        bool containsUnd = true;
        while (containsUnd == true)
        {
            int prevLength = element.Length;
            if (element.Contains("und"))
            {
                element = element.Remove(element.IndexOf("und"), 4);
            }
            if (element.Length == prevLength)
                containsUnd = false;
        }

        bool containsKlammern = true;
        while (containsKlammern == true)
        {
            int prevLength = element.Length;
            if (element.Contains("("))
            {
                element = element.Remove(element.IndexOf("("), element.IndexOf(")") - element.IndexOf("(") + 1);
            }
            if (element.Length == prevLength)
                containsKlammern = false;
        }

        bool containsSowie = true;
        while (containsSowie == true)
        {
            int prevLength = element.Length;
            if (element.Contains("sowie"))
            {
                element = element.Remove(element.IndexOf("sowie"), element.IndexOf(".") - element.IndexOf("sowie") + 1);
            }
            if (element.Length == prevLength)
                containsSowie = false;
        }
        return element;
    }
    public static string PrepareNutritions(string element)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < element.Length; i++)
        {
            if (element[i] == '1' || element[i] == '2' || element[i] == '3' || element[i] == '4' || element[i] == '5' ||
                element[i] == '6' || element[i] == '7' || element[i] == '8' || element[i] == '9' || element[i] == '0' || element[i] == ',')
            {
                stringBuilder.Append(element[i]);
            }
            if (element[i] == ' ')
                stringBuilder.Append(' ');
        }
        element = stringBuilder.ToString().Trim();
        return element;
    }
    public static string CleanUpString(string elementType, string element)
    {
        switch (elementType)
        {
            case "name":
                return element.Trim();
            case "price":
                return element.Substring(element.IndexOf(":") + 2, 4).Replace(",", ".");
            case "ingredients":
                return PrepareIngredients(element);
            case "nutritions":
                element = element.Trim();
                element = element.Remove(0, element.IndexOf(" ")).Trim();
                return PrepareNutritions(element);
            default:
                return "ERROR";
        }
    }
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
    public Dish(string name, double price, string[] ingredients, string nutritions, string imgURL)
    {
        Name = name;
        Price = price;
        Ingredients = ingredients;
        NutritionsString = nutritions;
        ImgURL = imgURL;
    }
    public string NutritionsString { get; set; }
}
