using Mensa_App.Classes.Models;
using Mensa_App.Classes.ViewModels;
using System.Diagnostics;
using System.Windows.Input;

namespace Mensa_App.Classes.View;

public partial class MenuView : ContentPage
{
    MenuViewModel MenuViewModel {  get; set; }
    public MenuView()
	{
		InitializeComponent();
        MenuViewModel = new MenuViewModel();
        BindingContext = MenuViewModel;
	}
}