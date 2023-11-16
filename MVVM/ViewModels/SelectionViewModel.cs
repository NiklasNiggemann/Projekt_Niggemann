using Mensa_App.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Mensa_App.Classes;
using Mensa_App.Classes.View;

namespace Mensa_App.MVVM.ViewModels;

public class SelectionViewModel : INotifyPropertyChanged
{
    public static Selection Selection { get; set; }
    public SelectionViewModel()
    {
        Selection = new Selection();
    }
    public string MainDishName
    {
        get
        {
            if (Selection.UserMain is not null)
                return Selection.UserMain.Name;
            return "";
        }
        set
        {
            Selection.UserMain.Name = value;
            OnPropertyChanged(nameof(MainDishName));
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
