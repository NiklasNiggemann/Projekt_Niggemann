using Mensa_App.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.Classes.ViewModels;

public class MenuViewModel
{
    public Menu Menu { get; set; }
    public MenuViewModel()
    {
        Menu = new Menu();
    }
    public MenuViewModel(string URL)
    {
        Menu = new Menu(URL);
    }
    ~MenuViewModel() { }
}
