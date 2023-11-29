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
        foreach (var x in e.PreviousSelection)
        {
            SettingsModel.UserAllergyIngredientList.Remove(x.ToString());
        }
        foreach (var x in e.CurrentSelection)
        {
            SettingsModel.UserAllergyIngredientList.Remove(x.ToString());
        }
    }
}