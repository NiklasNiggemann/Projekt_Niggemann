using HtmlAgilityPack;
using Mensa_App.Classes.ViewModels;
using Mensa_App.MVVM.ViewModels;
using System.Diagnostics;
using System.Windows.Input;

namespace Mensa_App.Classes.View;

public partial class MenuView : TabbedPage
{
    MenuViewModel MenuViewModel { get; set; }
    public MenuView()
    {
        InitializeComponent();
        MenuViewModel = new MenuViewModel();
        BindingContext = MenuViewModel;
    }
    public static event EventHandler<Dish> MainMenuSelected;
    public void On_MainMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        MainMenuSelected?.Invoke(this, e.SelectedItem as Dish);
    }
    
    private void Button_Clicked_0(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        MenuViewModel.Menu.CleanMenus();
        MenuViewModel.Menu.Document = new HtmlWeb().Load($"https://www.studierendenwerk-pb.de/{MenuViewModel.Menu.DatesURL[0]}");
        MenuViewModel.Menu.GenerateMenus();
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_3(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_4(object sender, EventArgs e)
    {

    }
}