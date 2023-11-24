using Mensa_App.MVVMS.ViewModels;

namespace Mensa_App.MVVMS.Views;

public partial class SelectionView : ContentPage
{
    public SelectionView()
    {
        InitializeComponent();
        BindingContext = new SelectionViewModel();
    }
}