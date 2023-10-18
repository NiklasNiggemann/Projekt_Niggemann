using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;

namespace Mensa_App.Classes
{
    internal class Dish
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Ingredients { get; set; }
        public string Nutritions { get; set; }
        public Dish(string name, string price, string ingredients, string nutritions)
        {
            Name = name;
            Price = price;
            Ingredients = ingredients;
            Nutritions = nutritions;
        }
        public static Dish Generate(IList<HtmlNode> nameAndPrice, IList<HtmlNode> ingredientsAndNutritions, int index)
        {
            string name = HtmlEntity.DeEntitize(nameAndPrice[index].QuerySelector("h4").InnerText);
            string price = HtmlEntity.DeEntitize(nameAndPrice[index].QuerySelector(".price").InnerText);
            string ingredients = HtmlEntity.DeEntitize(ingredientsAndNutritions[index].QuerySelector(".ingredients").InnerText);
            string nutritions = HtmlEntity.DeEntitize(ingredientsAndNutritions[index].QuerySelector(".nutritions").InnerText);
            Dish dish = new Dish(name, price, ingredients, nutritions);
            return dish;
        }
        // DishTypes: .main-dishes, .side-dishes & soups 
        public static List<Dish> List(HtmlDocument document, string dishType)
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
}
