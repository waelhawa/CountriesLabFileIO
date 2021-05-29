using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesLabFileIO
{
    class Country
    {
        public string Name { get; set; }
        public string Continent { get; set; }
        public string LanguageSpoken { get; set; }

        public Country()
        {

        }

        public Country(string name, string continent, string language)
        {
            Name = name;
            Continent = continent;
            LanguageSpoken = language;
        }
    }
}
