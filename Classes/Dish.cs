using System.Text;
using System.Linq;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Xml.Linq;

namespace Mensa_App.Classes;

public class Dish()
{
    public string ImgURL { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public Nutrition Nutrition { get; set; }
    public static List<Dish> DeleteDessertsFromSoupMenu(List<Dish> menu)
    {
        return menu.Where(dish => dish.Price > 2).ToList();
    }

    public static List<Dish> DivideDessertsFromSoupMenu(List<Dish> menu)
    {
        return menu.Where(dish => dish.Price < 2).ToList();
    }

    public static Dish Generate(IList<HtmlNode> nameAndPrice, IList<HtmlNode> ingredientsAndNutritions, int index)
    {
        string name = CleanUpString("name", nameAndPrice[index].QuerySelector("h4").InnerText);
        double price = Convert.ToDouble(CleanUpString("price", nameAndPrice[index].QuerySelector(".price").InnerText));

        List<Ingredient> ingredientList = [];
        string ingredientsString = CleanUpString("ingredients", ingredientsAndNutritions[index].QuerySelector(".ingredients").InnerText);
        if (ingredientsString[..3] == "Für")
        {
            ingredientList.Add(new Ingredient(ingredientsString[..ingredientsString.IndexOf('.')]));
        }
        else
        {
            string[] ingredientsArray = ingredientsString.Split(' ');
            Array.Sort(ingredientsArray);
            foreach (var ingredient in ingredientsArray)
            {
                if (!string.IsNullOrWhiteSpace(ingredient))
                    ingredientList.Add(new Ingredient(ingredient));
            }
        }

        string nutritionsString = CleanUpString("nutritions", ingredientsAndNutritions[index].QuerySelector(".nutritions").InnerText);

        string imgURL;
        try
        {
            var imgURLAttributes = nameAndPrice[index].QuerySelector("img").Attributes;
            imgURL = $"https://www.studierendenwerk-pb.de/{HtmlEntity.DeEntitize(imgURLAttributes[1].Value)}";
        }
        catch (NullReferenceException)
        {
            imgURL = "Kein Bild verfügbar!";
        }

        return new Dish
        {
            Name = name,
            Price = price / 100,
            Ingredients = ingredientList,
            Nutrition = CreateNutrition(nutritionsString),
            ImgURL = imgURL
        };
    }
    private static Nutrition CreateNutrition(string nutritionsString)
    {
        string[] nutritionsArray = nutritionsString.Split(" ");
        List<string> nutritionsList = nutritionsArray.Where(nutrition => !string.IsNullOrEmpty(nutrition)).ToList();
        if (nutritionsList.Count == 0)
        {
            return new Nutrition();
        }
        return new Nutrition
        {
            Brennwert = Convert.ToInt32(nutritionsList[0].Trim()),
            Kalorien = Convert.ToInt32(nutritionsList[1].Trim()),
            Fett = Convert.ToDouble(nutritionsList[2].Trim()),
            Kohlenhydrate = Convert.ToDouble(nutritionsList[3].Trim()),
            Eiweiß = Convert.ToDouble(nutritionsList[4].Trim())
        };
    }
    private static string PrepareIngredients(string element)
    {
        element = element.Trim();
        element = element.Remove(0, element.IndexOf(" ")).Trim();
        element = element.Replace("mit ", "");
        element = element.Replace("und", "");
        int startIndex = element.IndexOf('(');
        while (startIndex != -1)
        {
            int endIndex = element.IndexOf(')', startIndex);
            element = element.Remove(startIndex, endIndex - startIndex + 1);
            startIndex = element.IndexOf('(', startIndex);
        }
        element = element.Replace("sowie", "");
        return element;
    }
    private static string PrepareNutritions(string element)
    {
        StringBuilder stringBuilder = new();
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
    private static string CleanUpString(string elementType, string element)
    {
        switch (elementType)
        {
            case "name":
                return element.Trim();
            case "price":
                return element.Substring(element.IndexOf(':') + 2, 4).Replace(",", ".");
            case "ingredients":
                return PrepareIngredients(element);
            case "nutritions":
                element = element.Trim();
                element = element.Remove(0, element.IndexOf(' ')).Trim();
                return PrepareNutritions(element);
            default:
                return "ERROR";
        }
    }
    public static List<Dish> GenerateList(HtmlDocument document, string dishType)
    {
        List<Dish> mainDishesList = [];
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
