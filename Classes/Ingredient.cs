using CommunityToolkit.Mvvm.ComponentModel;

namespace Mensa_App.Classes;

public partial class Ingredient(string name) : ObservableObject
{
    public string Name { get; set; } = name;
    public string Information { get; set; } = "Informationen";
    private bool isAllergic;
    public bool IsAllergic { get; set; }
    [ObservableProperty]
    private Color allergyWarningColor = Colors.White;
}
