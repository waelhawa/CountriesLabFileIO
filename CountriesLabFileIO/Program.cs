using System;
using System.Collections.Generic;

namespace CountriesLabFileIO
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] menu = { "Display Countries", "Add Country", "Remove Country", "Save to file", "Load from file", "Exit" };
            //CountryView.DisplayMainMenu(menu);

            CountryController c = new CountryController();
            c.CountryList = new List<Country>();
            c.PreLoad();
        }
    }
}
