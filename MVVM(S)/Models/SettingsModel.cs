using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.MVVMS.Models;

public static class SettingsModel
{
    public static ObservableCollection<string> GeneralIngredientsList { get; set; } = [ "Milch", "Farbstoff", "Weizen", "Gluten", "Eier", "Antioxidationsmittel", "Schwefeldioxid", "Sulfite", "Soja", "Sellerie", "Rind", "Senf", "Schalenfrüchte", "Sesamsamen", "Hafer", "Geflügel", "Schwein", "Konservierungsstoff", "Gerste", "Roggen", "Phosphat" ];
    public static ObservableCollection<string> UserAllergyIngredientList { get; set; } = [];
}
