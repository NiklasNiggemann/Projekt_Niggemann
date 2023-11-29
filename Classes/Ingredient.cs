namespace Mensa_App.Classes;

public class Ingredient(string name)
{
    public string Name { get; set; } = name;
    public bool IsAllergic { get; set; }
    public string Information { get; set; } = "Informationen";
}
