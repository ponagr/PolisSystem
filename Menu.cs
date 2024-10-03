using System.Reflection.Metadata;

namespace Polisen
{
    //Presentation Layer. Meny-klass med olika menyer och anrop till HandleSystem.
    public static class Menu
    {
        //Metod som kan användas av alla andra menyer för att minska Redundans.
        //Används för att kunna göra val i menyn via Piltangenter, Enter och Backspace
        public static int ShowMenu(string[] options)
        {
            int menuChoice = 0;
            bool runMenu = true;

            while (runMenu)
            {
                Console.Clear();
                Console.CursorVisible = false;

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == menuChoice)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;    //För att HighLighta nuvarande menyval med pil och textfärg
                        Console.WriteLine(options[i] + " <--");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(options[i]);
                    }
                }

                var keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.DownArrow && menuChoice < options.Length - 1)
                {
                    menuChoice++;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow && menuChoice > 0)
                {
                    menuChoice--;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    return menuChoice;
                }
                else if (keyPressed.Key == ConsoleKey.Backspace)
                {
                    return options.Length - 1;
                }
            }
            return -1;
        }

        //Meny-metoder som anropar olika metoder baserat på menyval, anropar först ShowMenu() för själva gränssnittet.
        public static void MainMenu()
        { 
            bool runMenu = true;
            while (runMenu)
            {
                int menuChoice = ShowMenu(new string[] { "Lägg till info", "Skriv ut info", "Avsluta" });
                switch (menuChoice)
                {
                    case 0:
                        AddMenu();
                        break;
                    case 1:
                        PrintMenu();
                        break;
                    case 2:
                        runMenu = false;
                        break;
                }
            }
        }

        public static void AddMenu()
        {
            bool runMenu = true;
            while (runMenu)
            {
                int menuChoice = ShowMenu(new string[] { "Skriv rapport", "Registrera utryckning", "Lägg till personal", "Gå tillbaka" });
                switch (menuChoice)
                {
                    case 0:
                        HandleSystem.AddRapport();
                        break;
                    case 1:
                        HandleSystem.AddUtryckning();
                        break;
                    case 2:
                        HandleSystem.AddPersonal();
                        break;
                    case 3:
                        runMenu = false;
                        break;
                }
            }
        }

        public static void PrintMenu()
        {
            bool runMenu = true;
            while (runMenu)
            {
                int menuChoice = ShowMenu(new string[] { "Skriv ut Rapporter", "Skriv ut Utryckningar", "Skriv ut Personal-lista", "Gå tillbaka" });
                switch (menuChoice)
                {
                    case 0:
                        HandleSystem.PrintRapport();
                        break;
                    case 1:
                        HandleSystem.PrintUtryckning();
                        break;
                    case 2:
                        HandleSystem.PrintPersonal();
                        Console.ReadKey();
                        break;
                    case 3:
                        runMenu = false;
                        break;
                }
            }
        }
        public static void ExitMenu()
        {
            bool runMenu = true;
            while (runMenu)
            {
                Console.WriteLine("Är du säker på att du vill avsluta programmet?\n");
                int menuChoice = ShowMenu(new string[] { "Ja, Avsluta", "Nej, Gå tillbaka" });
                switch (menuChoice)
                {
                    case 0:
                        runMenu = false;
                        break;
                    case 1:
                        MainMenu();
                        break;
                }
            }
        }
    }
}