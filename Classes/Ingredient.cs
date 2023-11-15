using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.Classes;

public class Ingredient
{
    public string Name { get; set; }
    public bool IsAllergic { get; set; }
    public string Information { get; set; }
    public Ingredient(string name)
    {
        Name = name;
        CheckIfAllergic();
        Information = "tödlich";
    }
    public void CheckIfAllergic()
    {
        if (UserData.Allergies is not null)
        {
            foreach (var x in UserData.Allergies)
            {
                if (x == Name)
                    IsAllergic = true;
            }
        }
    }
}
