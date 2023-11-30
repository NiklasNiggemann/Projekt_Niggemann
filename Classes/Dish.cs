﻿using System.Text;
using System.Linq;
using HtmlAgilityPack;

namespace Mensa_App.Classes;

public class Dish(string name, double price, string[] ingredients, string nutritions, string imgURL)
{
    public string ImgURL { get; set; } = imgURL;
    public string Name { get; set; } = name;
    public double Price { get; set; } = price;
    public string[] Ingredients { get; set; } = ingredients;
    public List<Ingredient> IngredientList
    {
        get
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            foreach (var ingredient in Ingredients)
            {
                if (ingredient.Trim() != "")
                {
                    ingredientList.Add(new Ingredient(ingredient.Trim()));
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
    public string NutritionsString { get; set; } = nutritions;
    public Nutrition Nutrition
    {
        get
        {
            string[] nutritionsArray = NutritionsString.Split(" ");
            List<string> nutritionsList = new List<string>();
            foreach (var nutrition in nutritionsArray)
            {
                if (nutrition != "")
                    nutritionsList.Add(nutrition);
            }
            Nutrition nutritions = new Nutrition();
            if (nutritionsList.Count == 0)
                return nutritions;
            nutritions.Brennwert = Convert.ToInt32(nutritionsList[0].Trim());
            nutritions.Kalorien = Convert.ToInt32(nutritionsList[1].Trim());
            nutritions.Fett = Convert.ToDouble(nutritionsList[2].Trim());
            nutritions.Kohlenhydrate = Convert.ToDouble(nutritionsList[3].Trim());
            nutritions.Eiweiß = Convert.ToDouble(nutritionsList[4].Trim());
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

        string imgURL = "";
        try
        {
            var imgURLAttributes = nameAndPrice[index].QuerySelector("img").Attributes;
            StringBuilder imgURLstringBuilder = new StringBuilder();
            imgURLstringBuilder.Append("https://www.studierendenwerk-pb.de/");
            imgURLstringBuilder.Append(HtmlEntity.DeEntitize(imgURLAttributes[1].Value));
            imgURL = imgURLstringBuilder.ToString();

        }
        catch (NullReferenceException ex)
        {
            imgURL = "Kein Bild verfügbar!";
        }


        Dish dish = new Dish(name, price / 100, ingredientsArray, nutritionsString, imgURL);
        return dish;
    }
    public static string PrepareIngredients(string element)
    {
        element = element.Trim();
        element = element.Remove(0, element.IndexOf(" ")).Trim();
        element = element.Replace("mit ", "");
        element = element.Replace("und", "");
        int startIndex = element.IndexOf("(");
        while (startIndex != -1)
        {
            int endIndex = element.IndexOf(")", startIndex);
            element = element.Remove(startIndex, endIndex - startIndex + 1);
            startIndex = element.IndexOf("(", startIndex);
        }
        element = element.Replace("sowie", "");
        return element;
    }
    public static string PrepareNutritions(string element)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in element)
        {
            if (char.IsDigit(c) || c == ',' || c == ' ')
            {
                stringBuilder.Append(c);
            }
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
            Dish mainDish = Generate(nameAndPrice, ingredientsAndNutritions, i);
            mainDishesList.Add(mainDish);
        }
        return mainDishesList;
    }
}
