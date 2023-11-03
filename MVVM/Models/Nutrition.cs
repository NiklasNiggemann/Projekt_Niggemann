﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensa_App.MVVM.Models;

public class Nutrition
{
    public int Brennwert { get; set; }
    public string BrennwertString { get; set; }
    public int Kalorien { get; set; }
    public string KalorienString { get; set; }

    public double Fett { get; set; }
    public string FettString { get; set; }

    public double Kohlenhydrate { get; set; }
    public string KohlenhydrateString { get; set; }

    public double Eiweiß { get; set; }
    public string EiweißString { get; set; }

}