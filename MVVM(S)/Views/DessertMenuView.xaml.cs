using Mensa_App.Classes;
using Mensa_App.MVVMS.Models;
using Mensa_App.MVVMS.ViewModels;

namespace Mensa_App.MVVMS.Views;

public partial class DessertMenuView : ContentPage
{
	public DessertMenuView()
	{
		InitializeComponent();
		BindingContext = new MenuViewModel();
	}
}