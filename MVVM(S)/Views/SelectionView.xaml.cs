using Mensa_App.MVVM.ViewModels;

namespace Mensa_App.MVVM.View;

public partial class SelectionView : ContentPage
{
    public SelectionView()
    {
        InitializeComponent();
        BindingContext = new SelectionViewModel();
    }
}