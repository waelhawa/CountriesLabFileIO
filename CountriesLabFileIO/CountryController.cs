
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CountriesLabFileIO
{
    class CountryController
    {
        const string menuSelectionPrompt = "Select from menu: ";
        const string numberOnlyError = "Use numbers only";
        const string selectionNotInMenu = "This selection is not valid.";
        const string countryExists = "This country is already in the list";
        const string emptyList = "The Country list is empty, please add a country";
        const string displayCountyInfo = "Would you like to know more about a country? (Y/N): ";
        const string yesOrNoError = "Please select Y or N";
        const string emptyFieldError = "Field cannot be empty";
        const string veryShortInputError = "Field is too short";
        const string inputError = "Cannot use special characters";
        const string success = "Action completed";
        const string exitMessage = "Goodbye!";
        const string fileName = "Country List";
        const string fileFound = "There's a save file, would you like to load it? (Y/N): ";
        const string fileNotFoundError = "File not found!";
        const string confirmLoad = "Loading a file will erase any unsaved data. Are you sure? (Y/N): ";
        const string fileNotLoaded = "File was not loaded.";
        const string overrideConfirmation = "Are you sure you want to override existing file? (Y/N): ";
        const string confirmDeletion = "Are you sure you want to delete saved file? (Y/N): ";
        static readonly string[] menu = { "Display Countries", "Add Country", "Remove Country", "Save to file", "Load from file", "Delete saved file", "Exit" };
        static readonly string[] properties = { "Name", "Continent", "Spoken Language" };

        public List<Country> CountryList { get; set; } //end Property

        public void Save(List<Country> list, string fileName)
        {
            StreamWriter writer = new StreamWriter($"../../../{fileName}.txt");
            foreach (Country country in list)
            {
                writer.WriteLine($"{country.Name}|{country.Continent}|{country.LanguageSpoken}");
            }
            writer.Close();
        } //end Save

        public void CheckSavedFile()
        {
            if (File.Exists($"../../../{fileName}.txt"))
            {
                if (Verification(overrideConfirmation, yesOrNoError))
                {
                    Save(CountryList, fileName);
                }
            }
            else
            {
                Save(CountryList, fileName);
            }
        } //end CheckSavedFile

        public StreamReader ReadFile(string fileName)
        {
            try
            {
                StreamReader reader = new StreamReader($"../../../{fileName}.txt");
                return reader;

            }
            catch (FileNotFoundException)
            {
                return null;
            }
        } //end ReadOrCreateFile

        public void Load(string fileName, bool preLoadVerified)
        {
            if (preLoadVerified || Verification(confirmLoad, yesOrNoError))
            {
                CountryList.Clear();
                StreamReader reader = ReadFile(fileName);
                if (reader != null)
                {
                    string text = reader.ReadLine();
                    string[] properties;

                    while (text != null)
                    {
                        properties = text.Split('|');
                        CountryList.Add(new Country(properties[0], properties[1], properties[2]));
                        text = reader.ReadLine();
                    }
                    if (CountryList.Count > 0)
                    {
                        CountryView.DisplayCountries(CountryList);
                    }
                    else
                    {
                        CountryView.EmptyFileError();
                    }
                    reader.Close();
                }
                else
                {
                    CountryView.EmptyFileError();
                }
            }
            else
            {
                CountryView.PromptUser(fileNotLoaded);
            }

        } //end Load

        public void AddToList(Country country)
        {
            CountryList.Add(country);
        } //end AddToList

        public void RemoveFromList(Country country)
        {
            CountryList.Remove(country);
            CountryView.PromptUser(success);
        } //end RemoveFromList

        public void CheckListForEntry(Country country)
        {
            bool exists = false;
            foreach (Country c in CountryList)
            {
                if (c.Name == country.Name)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                AddToList(country);
                CountryView.PromptUser(success);
                StartHere();
            }
            else
            {
                CountryView.PromptUser(countryExists);
                StartHere();
            }
        } //end CheckListForEntry

        public int IntegerEntry(string phrase, string error)
        {
            string text;
            int integerNumber;
            while (true)
            {
                CountryView.PromptUserAndWait(phrase);
                text = Console.ReadLine().Trim().ToLower();
                if (int.TryParse(text, out integerNumber))
                {
                    return integerNumber;
                }
                else
                {
                    CountryView.PromptUser(error);
                }
            }
        } //end IntegerEntry

        public int MenuSelection(int menuStart, int menuEnd)
        {
            int item;
            while (true)
            {
                item = IntegerEntry(menuSelectionPrompt, numberOnlyError);
                if (item >= menuStart && item <= menuEnd)
                {
                    return item;
                }
                else
                {
                    CountryView.PromptUser(selectionNotInMenu);
                }
            }
        } //End MenuSelection

        public bool Verification(string phrase, string error)
        {
            string text;
            while (true)
            {
                CountryView.PromptUserAndWait(phrase);
                text = Console.ReadLine().Trim().ToLower();

                switch (text)
                {
                    case "y":
                    case "yes":
                        return true;


                    case "n":
                    case "no":
                        return false;


                    default:
                        CountryView.PromptUser(error);
                        break;
                }
            }
        } //end Verification

        public void PreLoad()
        {
            if (File.Exists($"../../../{fileName}.txt"))
            {
                if (Verification(fileFound, yesOrNoError))
                {
                    Load(fileName, true);
                    CountryView.PromptUser(success);
                }
            }
            StartHere();
        } //end PreLoad

        public void StartHere()
        {
            CountryView.DisplayMainMenu(menu);
            int selection = MenuSelection(1, menu.Length);
            MainMenu(selection);
        } //end StartHere

        public string UserInput(string property)
        {
            string text;
            bool passed;
            while (true)
            {
                passed = true;
                CountryView.PromptUserAndWait($"Enter {property}: ");
                text = Console.ReadLine();
                if (text.Length > 3)
                {
                    foreach (char letter in text.ToCharArray())
                    {
                        if (!Char.IsLetter(letter) && !Char.IsWhiteSpace(letter))
                        {
                            CountryView.PromptUser(inputError);
                            passed = false;
                            break;
                        }
                    }
                    if (passed)
                    {
                        return MultipleWordIndent(text.Trim().ToLower());
                    }
                }
                else if (text.Length > 0)
                {
                    CountryView.PromptUser(veryShortInputError);
                }
                else
                {
                    CountryView.PromptUser(emptyFieldError);
                }
            }
        } //end UserInput

        public void NewCountry()
        {
            Country newCountry = new Country
            {
                Name = UserInput(properties[0]),
                Continent = UserInput(properties[1]),
                LanguageSpoken = UserInput(properties[2])
            };
            CheckListForEntry(newCountry);
        } //end NewCountry

        public void MainMenu(int selection)
        {
            bool verifyChoice;
            int choice;
            switch (selection)
            {
                case 1:
                    if (CountryList.Count > 0)
                    {
                        CountryView.DisplayCountries(CountryList);
                        verifyChoice = Verification(displayCountyInfo, yesOrNoError);
                        SubMenuOne(verifyChoice);
                    }
                    else
                    {
                        CountryView.PromptUser(emptyList);
                        StartHere();
                    }
                    break;
                case 2:
                    NewCountry();
                    break;
                case 3:
                    if (CountryList.Count > 0)
                    {
                        CountryView.DisplayCountries(CountryList);
                        choice = MenuSelection(1, CountryList.Count);
                        RemoveFromList(CountryList[choice - 1]);
                        StartHere();
                    }
                    else
                    {
                        CountryView.PromptUser(emptyList);
                        StartHere();
                    }
                    break;
                case 4:
                    CheckSavedFile();
                    StartHere();
                    break;
                case 5:
                    Load(fileName, false);
                    StartHere();
                    break;
                case 6:
                    DeleteSavedFile();
                    StartHere();
                    break;
                default:
                    CountryView.PromptUser(exitMessage);
                    break;
            }
        } //end MainMenu

        public void DeleteSavedFile()
        {
            if (File.Exists($"../../../{fileName}.txt"))
            {
                if (Verification(confirmDeletion, yesOrNoError))
                {
                    File.Delete($"../../../{fileName}.txt");
                    CountryView.PromptUser(success);
                }
            }
            else
            {
                CountryView.PromptUser(fileNotFoundError);
            }
        } //end DeleteSavedFile

        public void SubMenuOne(bool choice)
        {
            if (choice)
            {
                int item = MenuSelection(1, CountryList.Count);
                CountryView.DisplayCountry(CountryList[item - 1]);
                MainMenu(1);
            }
            else
            {
                StartHere();
            }
        } //end SubMenuOne

        public string Indent(string text)
        {
            return text.Substring(0, 1).ToUpper() + text.Substring(1).ToLower();
        } //end Indent

        public string MultipleWordIndent(string text)
        {
            string[] words;
            char[] delimiters = { ' ', '.', ':', '-' };
            StringBuilder newText = new StringBuilder();
            words = text.Split(delimiters);
            foreach (string word in words)
            {
                if (word.Length > 0)
                {
                    newText.Append(Indent(word));
                }
            }
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].ToString().ToLower() != newText[i].ToString().ToLower())
                {
                    newText.Insert(i, text[i]);
                }
            }
            return newText.ToString().Trim();
        } //end MultipleWords

    }
}
