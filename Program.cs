using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Polisen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu.MainMenu();
            Menu.ExitMenu();

            Console.Clear();
            Console.WriteLine("Välkommen åter!");
            Console.WriteLine("Tryck på valfri tangent för att avsluta programmet...");
            Console.ReadKey();
        }    
    }

    //Presentation Layer. Meny-klass med olika menyer och anrop.
    public static class Menu
    {
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
                        Console.WriteLine(options[i] + " <--");
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

        public static void MainMenu()
        {
            int menuChoice = 0;   
            bool runMenu = true;
            while (runMenu)
            {
                menuChoice = ShowMenu(new string[] { "Lägg till info", "Skriv ut info", "Avsluta" });
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
            Utryckning utryckning = new Utryckning();
            Personal personal = new Personal();
            Rapport rapport = new Rapport();
            int menuChoice = 0;  
            bool runMenu = true;
            while (runMenu)
            {
                menuChoice = ShowMenu(new string[] { "Skriv rapport", "Registrera utryckning", "Lägg till personal", "Gå tillbaka" });
                switch (menuChoice)
                {
                    case 0:
                        rapport.AddInfo();
                        break;
                    case 1:
                        utryckning.AddInfo();
                        break;
                    case 2:
                        personal.AddInfo();
                        break;
                    case 3:
                        runMenu = false;
                        break;
                }
            }
        }

        public static void PrintMenu()
        {
            Utryckning utryckning = new Utryckning();
            Personal personal = new Personal();
            Rapport rapport = new Rapport();
            int menuChoice = 0;  
            bool runMenu = true;
            while (runMenu)
            {
                menuChoice = ShowMenu(new string[] { "Skriv ut Rapporter", "Skriv ut Utryckningar", "Skriv ut Personal-lista", "Gå tillbaka" });
                switch (menuChoice)
                {
                    case 0:
                        rapport.PrintInfo();
                        break;
                    case 1:
                        utryckning.PrintInfo();
                        break;
                    case 2:
                        personal.PrintInfo();
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
            int menuChoice = 0;  
            while (runMenu)
            {                
                Console.WriteLine("Är du säker på att du vill avsluta programmet?\n");
                menuChoice = ShowMenu(new string[] { "Ja, Avsluta", "Nej, Gå tillbaka" });
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

    public class RapportSystem
    {
        public static List<Utryckning> utryckningsLista = new List<Utryckning>();
        public static List<Personal> personalLista = new List<Personal>();
        public static List<Rapport> rapportLista = new List<Rapport>();

        
    }

    public class Utryckning : RapportSystem
    {
        public string brottsTyp;
        public string plats;
        public string tidpunkt;    
        public List<Personal> delaktigaPoliser = new List<Personal>();

        public void AddInfo()
        {
            Utryckning utryckning = new Utryckning();
            Console.Write($"Fyll i brottstyp: ");
            utryckning.brottsTyp = Console.ReadLine();
            Console.Write($"Fyll i plats: ");
            utryckning.plats = Console.ReadLine();
            Console.Write($"Fyll i Tidpunkt: ");
            utryckning.tidpunkt = Console.ReadLine();

            Console.Write("\nHur många Poliser deltog i utryckningen: ");
            int answer = int.Parse(Console.ReadLine());
            Console.WriteLine("\nVälj polisen/poliserna som deltog: ");
            for (int i = 0; i < answer; i++)
            {
                AddPolice(utryckning);
            }
            Console.WriteLine("Poliser tillagda i utryckningen:");
            foreach (var polis in utryckning.delaktigaPoliser)
            {
                Console.WriteLine($"{polis.namn}, {polis.tjänsteNummer}");
            }
            
            utryckningsLista.Add(utryckning);
            Console.ReadKey();
        }

        public void AddPolice(Utryckning utryckning)
        {
            PrintPersonal();
            Console.Write("Skriv in index för polisen du vill lägga till: ");
            int i = int.Parse(Console.ReadLine());
            Console.WriteLine();
            utryckning.delaktigaPoliser.Add(personalLista[i-1]);
            
        }

        public void PrintPersonal()
        {
            for(int i = 0; i < personalLista.Count; i++)
            {
                Console.WriteLine($"{i +1}. {personalLista[i].namn}, {personalLista[i].tjänsteNummer}");
            }
        }

        public void PrintInfo()
        {
            for (int i = 0; i < utryckningsLista.Count; i++)
            {       
                Console.WriteLine($"{i+1}.");    
                Console.WriteLine($"Brottstyp: {utryckningsLista[i].brottsTyp}");
                Console.WriteLine($"Plats: {utryckningsLista[i].plats}");
                Console.WriteLine($"Tidpunkt: {utryckningsLista[i].tidpunkt}");          
                for (int y = 0; y < utryckningsLista[i].delaktigaPoliser.Count; y++)
                {
                    Console.WriteLine($"Personal: {utryckningsLista[i].delaktigaPoliser[y].namn}, {utryckningsLista[i].delaktigaPoliser[y].tjänsteNummer}");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        
    }

    public class Personal : RapportSystem
    {
        public string namn;
        public int tjänsteNummer;

        public Personal(string namn, int tjänsteNummer)
        {
            this.namn = namn;
            this.tjänsteNummer = tjänsteNummer;
        }
        public Personal() {}

        public void AddInfo()
        {            
            Console.Write($"Fyll i namn: ");
            string namn = Console.ReadLine();
            Console.Write($"Fyll i tjänstenummer: ");
            int tjänsteNummer = int.Parse(Console.ReadLine());
            Personal personal = new Personal(namn, tjänsteNummer);
            personalLista.Add(personal);
        }

        public void PrintInfo()
        {
            for (int i = 0; i < personalLista.Count; i++)
            {
                Console.WriteLine($"{i +1}. Namn: {personalLista[i].namn}");
                Console.WriteLine($"   Tjänstenummer: {personalLista[i].tjänsteNummer}\n");
            }
            Console.ReadKey();
        }
    }

    public class Rapport : RapportSystem
    {
        public string station;
        public string beskrivning;
        public string datum;
        public int rapportNummer;

        public void AddInfo()
        {
            Rapport rapport = new Rapport();
            Console.WriteLine("Fyll i rapport");
            Console.Write("Beskrivning: ");
            rapport.beskrivning = Console.ReadLine();
            Console.Write($"Station: ");
            rapport.station = Console.ReadLine();
            Console.Write($"Datum: ");
            rapport.datum = (Console.ReadLine());
            Console.Write($"Rapportnummer: ");
            rapport.rapportNummer = int.Parse(Console.ReadLine());
            rapportLista.Add(rapport);
        }

        public void PrintInfo()
        {
            for (int i = 0; i < rapportLista.Count; i++)
            {
                Console.WriteLine($"{i+1}.");
                Console.WriteLine($"Beskrivning: {rapportLista[i].beskrivning}");
                Console.WriteLine($"Station: {rapportLista[i].station}");
                Console.WriteLine($"Datum: {rapportLista[i].datum}");
                Console.WriteLine($"Rapportnummer: {rapportLista[i].rapportNummer}\n");
            }
            Console.ReadKey();
        }

    }
}
