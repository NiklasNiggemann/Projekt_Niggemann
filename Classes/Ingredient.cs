using CommunityToolkit.Mvvm.ComponentModel;

namespace Mensa_App.Classes;

public partial class Ingredient(string name) : ObservableObject
{
    public string Name { get; set; } = name;
    public string Information { get; set; } = "Informationen";
    [ObservableProperty]
    private Color allergyWarningColor;
}
