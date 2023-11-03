using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.MVVM.Models;

public class Ingredient
{
    public string Name { get; set; }
    public bool IsAllergic { get; set; }
    public Ingredient(string name)
    {
        Name = name;
        IsAllergic = false;
    }
}
