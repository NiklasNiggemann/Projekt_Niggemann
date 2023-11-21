using Mensa_App.MVVM.ViewModels;

namespace Mensa_App.MVVM.View;

public partial class MenuView : ContentPage
{
    public MenuView() 
    {
        InitializeComponent();
        BindingContext = new MenuViewModel();
    }
}