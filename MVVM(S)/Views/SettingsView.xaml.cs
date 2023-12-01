using Mensa_App.MVVMS.Models;
using Mensa_App.MVVMS.ViewModels;

namespace Mensa_App.MVVMS.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
		BindingContext = new SettingsViewModel();
	}
    private void SettingsIngredientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (var ingredients in e.PreviousSelection)
        {
            SettingsModel.UserAllergyIngredientList.Remove(ingredients.ToString());
        }
        foreach (var ingredients in e.CurrentSelection)
        {
            SettingsModel.UserAllergyIngredientList.Add(ingredients.ToString());
        }
    }
}