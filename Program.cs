using System.Net.Http.Headers;
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
            //int menuChoice = 0;   
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
            //Skapar instanser av alla klasser för att kunna anropa rätt AddInfo beroende på menyval
            Utryckning utryckning = new Utryckning();
            Personal personal = new Personal();
            Rapport rapport = new Rapport();
            //int menuChoice = 0;  
            bool runMenu = true;
            while (runMenu)
            {
                int menuChoice = ShowMenu(new string[] { "Skriv rapport", "Registrera utryckning", "Lägg till personal", "Gå tillbaka" });
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
            //int menuChoice = 0;  
            bool runMenu = true;
            while (runMenu)
            {
                int menuChoice = ShowMenu(new string[] { "Skriv ut Rapporter", "Skriv ut Utryckningar", "Skriv ut Personal-lista", "Gå tillbaka" });
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

    
    //Lägg till Logical Layer. Klass som anropas från menyn, Kollar så att Input är korrekt osv, 
    //Ska innehålla try/catch, if-else osv. Som sedan anropar Data Access Layer
    



    //Data Access Layer. Lägger till och Hämtar ut data via dessa klasser.
    public class RapportSystem
    {
        //Statiska listor som är allmänna, inte för specifika objekt.
        //Håller en samling av flera objekt
        public static List<Utryckning> utryckningsLista = new List<Utryckning>();
        public static List<Personal> personalLista = new List<Personal>();
        public static List<Rapport> rapportLista = new List<Rapport>();
       
    }

    public class Utryckning : RapportSystem
    {
        public string BrottsTyp { get; set; }
        public string Plats { get; set; }
        public string Time { get; set; }   

        //Icke statisk lista som är enskild för varje objekt, innehåller en eller flera poliser från klassen Personal
        public List<Personal> DelaktigaPoliser { get; set; } = new List<Personal>();

        public void AddInfo()
        {
            Utryckning utryckning = new Utryckning();   //Skapa nytt objekt att lägga till i listan.
            Console.Write($"\nFyll i brottstyp: ");
            utryckning.BrottsTyp = Console.ReadLine();
            Console.Write($"Fyll i plats: ");
            utryckning.Plats = Console.ReadLine();

            Console.Write("Hur många Poliser deltog i utryckningen: ");
            int answer = int.Parse(Console.ReadLine());     //Lägg in felhantering

            Console.WriteLine("\nVälj polisen/poliserna som deltog: ");
            for (int i = 0; i < answer; i++)    //Anropa metoden så många gånger som användaren skriver in, för möjlighet att lägga till flera poliser
            {
                AddPolice(utryckning);  //Skicka med objektet som argument för att lägga till lista i det specifika objektet
            }
            Console.WriteLine("Poliser tillagda i utryckningen:");
            foreach (var polis in utryckning.DelaktigaPoliser)  //Kontroll för att se så att poliserna faktiskt har lagts till korrekt i listan
            {
                Console.WriteLine($"{polis.Name} {polis.LastName}, {polis.BadgeNumber}");
            }
            utryckning.AddCurrentTime();
            Console.WriteLine($"Utryckningen registrerades klockan: {utryckning.Time}");
            
            utryckningsLista.Add(utryckning);   //Lägg till objektet i Statisk Lista
            Console.ReadKey();
        }

        private void AddCurrentTime()
        {
            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("HH:mm");
            Time = time;
        }

        public void AddPolice(Utryckning utryckning)
        {
            PrintPersonal();    //Anropa metod för att skriva ut Personal-Listan 
            Console.Write("Skriv in index för polisen du vill lägga till: ");
            int i = int.Parse(Console.ReadLine());
            Console.WriteLine();
            utryckning.DelaktigaPoliser.Add(personalLista[i-1]);    //Lägg till element som finns på index i-1, som användaren väljer
            
        }

        public void PrintPersonal()
        {
            for(int i = 0; i < personalLista.Count; i++)
            {
                Console.WriteLine($"{i +1}. {personalLista[i].Name} {personalLista[i].LastName}, {personalLista[i].BadgeNumber}");
            }
        }

        public void PrintInfo()
        {
            for (int i = 0; i < utryckningsLista.Count; i++)
            {       
                Console.WriteLine($"{i+1}.");    
                Console.WriteLine($"Brottstyp: {utryckningsLista[i].BrottsTyp}");
                Console.WriteLine($"Plats: {utryckningsLista[i].Plats}");
                Console.WriteLine($"Tidpunkt: {utryckningsLista[i].Time}");  
                Console.WriteLine("Personal närvarande:");        
                for (int y = 0; y < utryckningsLista[i].DelaktigaPoliser.Count; y++)
                {
                    //Skriv ut listan med poliser för nuvarande objekt
                    Console.WriteLine($"{utryckningsLista[i].DelaktigaPoliser[y].Name}, {utryckningsLista[i].DelaktigaPoliser[y].BadgeNumber}");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        
    }

    public class Personal : RapportSystem
    {
        private string name;
        private string lastName;
        public int BadgeNumber { get; set; }
        public string Name 
        { 
            get { return name; } 
            set { name = char.ToUpper(value[0]) + value.Substring(1).ToLower(); } 
        }
        public string LastName 
        { 
            get { return lastName; } 
            set { lastName = char.ToUpper(value[0]) + value.Substring(1).ToLower(); } 
        }
        
        public void AddInfo()
        {   
            Personal personal = new Personal();
            personal.AddFirstName();
            personal.AddLastName();
            personal.AddBadgeNumber();
            // Console.Write($"\nFyll i namn: ");
            // personal.Name = Console.ReadLine();
            // Console.Write($"Fyll i efternamn: ");
            // personal.LastName = Console.ReadLine();
            // Console.Write($"Fyll i tjänstenummer: ");
            // personal.BadgeNumber = int.Parse(Console.ReadLine());
            AddToList(personal);
            //personalLista.Add(personal);
        }
        public void AddToList(Personal personal)
        {
            if (personalLista.Any(p => p.Name == personal.Name && p.LastName == personal.LastName))
            {
                Console.WriteLine("Det finns redan en personal med samma namn och efternamn");
            }
            else if (personalLista.Any(p => p.BadgeNumber == personal.BadgeNumber))
            {
                Console.WriteLine("Det finns redan en personal med detta tjänstenummer");
            }
            else
            {
                personalLista.Add(personal);
                Console.WriteLine("Personal har lagts till");
            }
            Console.ReadKey();            
        }
        

        //Lägg in felhantering i dessa
        public void AddFirstName()
        {
            while (true)
            {
                Console.Write($"Fyll i förnamn: ");
                string firstname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(firstname))
                {
                    Console.WriteLine("Du måste fylla i ditt förnamn, försök igen");
                }
                else if (firstname.Any(char.IsDigit))
                {
                    Console.WriteLine("Ditt förnamn kan inte innehålla siffror, försök igen");
                }
                else
                {
                    Name = firstname;
                    break;
                }
            }
        }
        public void AddLastName()
        {
            while (true)
            {
                Console.Write($"Fyll i efternamn: ");
                string lastname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(lastname))
                {
                    Console.WriteLine("Du måste fylla i ditt efternamn, försök igen");
                }
                else if (lastname.Any(char.IsDigit))
                {
                    Console.WriteLine("Ditt efternamn kan inte innehålla siffror, försök igen");
                }
                else
                {
                    LastName = lastname;
                    break;
                }
            }
        }
        public void AddBadgeNumber()
        {
            int badgeNumber;
            Console.Write($"Fyll i tjänstenummer: ");
            while (!int.TryParse(Console.ReadLine(), out badgeNumber))
            {
                Console.WriteLine("Ogiltig inmatning. Skriv in ett giltigt tjänstenummer med heltal:");
            }
            BadgeNumber = badgeNumber;
        }

        public void PrintInfo()
        {
            for (int i = 0; i < personalLista.Count; i++)
            {
                Console.WriteLine($"{i +1}. Namn: {personalLista[i].Name} {personalLista[i].LastName}");
                Console.WriteLine($"   Tjänstenummer: {personalLista[i].BadgeNumber}\n");
            }
            Console.ReadKey();
        }
    }

    public class Rapport : RapportSystem
    {
        public string Station { get; set; }
        public string Beskrivning { get; set; }
        public string Date { get; set; }
        private static int nextRapportNummer = 1;
        public int RapportNummer { get; set; }
        public void NewRapportNummer()
        {
            RapportNummer = nextRapportNummer++;         
        }

        public void AddInfo()
        {
            Rapport rapport = new Rapport();
            Console.WriteLine("\nFyll i rapport");
            Console.Write("Beskrivning: ");
            rapport.Beskrivning = Console.ReadLine();
            Console.Write($"Station: ");
            rapport.Station = Console.ReadLine();
            rapport.AddCurrentDate();
            rapport.NewRapportNummer();
            Console.WriteLine($"Rapporten registrerades i systemet den: {rapport.Date}");
            rapportLista.Add(rapport);
            Console.ReadKey();
        }

        private void AddCurrentDate()
        {
            DateTime currentDate = DateTime.Now.Date;
            string dateString = currentDate.ToString("yyyy-MM-dd");
            Date = dateString;
        }

        public void PrintInfo()
        {
            Console.WriteLine();
            for (int i = 0; i < rapportLista.Count; i++)
            {
                Console.WriteLine($"Rapportnummer: {rapportLista[i].RapportNummer}.");
                Console.WriteLine($"Datum: {rapportLista[i].Date}");
                Console.WriteLine($"Beskrivning: {rapportLista[i].Beskrivning}");
                Console.WriteLine($"Station: {rapportLista[i].Station}\n");
            }
            Console.ReadKey();
        }

    }
}
