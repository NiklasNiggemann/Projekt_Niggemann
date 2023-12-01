using CommunityToolkit.Mvvm.ComponentModel;

namespace Mensa_App.Classes;

public partial class Ingredient(string name) : ObservableObject
{
    public string Name { get; set; } = name;
    public string Information { get; set; } = "Informationen";
    private bool isAllergic;
    public bool IsAllergic
    {
        get
        {
            return isAllergic;
        }
        set
        {
            isAllergic = value;
            if (isAllergic == true)
                AllergyWarningColor = Colors.Red;
            else
                AllergyWarningColor = Colors.White;
        }
    }
    [ObservableProperty]
    private Color allergyWarningColor = Colors.White;
}
