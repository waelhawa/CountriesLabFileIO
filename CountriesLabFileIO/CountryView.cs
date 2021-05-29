using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesLabFileIO
{
    class CountryView
    {
        public static void DisplayCountries(List<Country> list)
        {
            Console.WriteLine();
            for (int i = 0; i < list.Count; i++)
            {
                DisplayMenu(i + 1, list[i].Name); 
            }
            Console.WriteLine();
        }

        public static void DisplayCountry(Country country)
        {
            Console.WriteLine();
            Console.WriteLine($"{country.Name} is in {country.Continent} and its people speak {country.LanguageSpoken}");
        }

        public static void DisplayMenu (int count, string listing)
        {
            Console.WriteLine($"{count}- {listing}");
        }

        public static void DisplayMainMenu(string [] menu)
        {
            Console.WriteLine();
            for (int i = 0; i < menu.Length; i++)
            {
                CountryView.DisplayMenu(i + 1, menu[i]);
            }
            Console.WriteLine();

        }

        public static void EmptyFileError ()
        {
            Console.WriteLine("File is empty");
        }

        public static void PromptUser (string message)
        {
            Console.WriteLine(message);
        }

        public static void PromptUserAndWait (string message)
        {
            Console.Write(message);
        }

    }
}
