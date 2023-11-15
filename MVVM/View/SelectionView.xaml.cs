using Mensa_App.MVVM.ViewModels;

namespace Mensa_App.MVVM.View;

public partial class SelectionView : ContentPage
{
	SelectionViewModel SelectionViewModel { get; set; }

    public SelectionView()
	{
		InitializeComponent();
        SelectionViewModel = new SelectionViewModel();
        BindingContext = SelectionViewModel;
    }
}