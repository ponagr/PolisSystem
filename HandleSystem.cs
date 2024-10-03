using System.Text.Json;

namespace Polisen
{
    //Hanterar alla klasser, anropas från meny, och anropar sedan gällande klass och gör något med klassen(skapar objekt osv)
    class HandleSystem
    {
        public static List<Utryckning> utryckningsLista = new List<Utryckning>();
        public static List<Personal> personalLista = new List<Personal>();
        public static List<Rapport> rapportLista = new List<Rapport>();
        
        
        //Hantera Personal-Klass
        public static void AddPersonal()
        {
            Personal personal = new Personal();
            Console.WriteLine();
            personal.AddFirstName();
            personal.AddLastName();
            personal.AddBadgeNumber();

            AddToList(personal);
        }        
        public static void PrintPersonal()
        {
            Console.WriteLine();
            if (personalLista.Count != 0)
            {
                for (int i = 0; i < personalLista.Count; i++)
                {
                Console.WriteLine($"{i +1}. Namn: {personalLista[i].Name} {personalLista[i].LastName}");
                Console.WriteLine($"   Tjänstenummer: {personalLista[i].BadgeNumber}\n");
                }     
            }
            else
            {
                Console.WriteLine("Det finns ingen personal tillagd i listan");
            }                   
        }
        public static void AddToList(Personal personal)
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

        //Hantera Rapport-Klass
        public static void AddRapport()
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
        public static void PrintRapport()
        {
            Console.WriteLine();
            if (rapportLista.Count != 0)
            {
                for (int i = 0; i < rapportLista.Count; i++)
                {
                    Console.WriteLine($"Rapportnummer: {rapportLista[i].RapportNummer}.");
                    Console.WriteLine($"Datum: {rapportLista[i].Date}");
                    Console.WriteLine($"Beskrivning: {rapportLista[i].Beskrivning}");
                    Console.WriteLine($"Station: {rapportLista[i].Station}\n");
                }
            }
            else
            {
                Console.WriteLine("Det finns inga rapporter tillagda i listan");
            }
            Console.ReadKey();
        }

        //Hantera Utrycknings-Klass
        public static void AddUtryckning()
        {
            int answer;
            Utryckning utryckning = new Utryckning();   //Skapa nytt objekt att lägga till i listan.
            Console.Write($"\nFyll i brottstyp: ");
            utryckning.BrottsTyp = Console.ReadLine();
            Console.Write($"Fyll i plats: ");
            utryckning.Plats = Console.ReadLine();
            while (true)
            {
                try
                {
                    Console.Write("Hur många Poliser deltog i utryckningen: ");
                    answer = int.Parse(Console.ReadLine());
                    break;
                }
                catch 
                {
                    Console.WriteLine("\nSvaret måste vara ett heltal.");
                }
            }

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
        public static void AddPolice(Utryckning utryckning)
        {
            PrintPersonal();    //Anropa metod för att skriva ut Personal-Listan 
            Console.Write("Skriv in index för polisen du vill lägga till: ");
            int i = int.Parse(Console.ReadLine());
            Console.WriteLine();
            utryckning.DelaktigaPoliser.Add(personalLista[i-1]);    //Lägg till element som finns på index i-1, som användaren väljer
            
        }
        public static void PrintUtryckning()
        {
            Console.WriteLine();
            if (utryckningsLista.Count != 0)
            {
                for (int i = 0; i < utryckningsLista.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.");
                    Console.WriteLine($"Brottstyp: {utryckningsLista[i].BrottsTyp}");
                    Console.WriteLine($"Plats: {utryckningsLista[i].Plats}");
                    Console.WriteLine($"Tidpunkt: {utryckningsLista[i].Time}");
                    Console.WriteLine("Personal närvarande:");
                    for (int y = 0; y < utryckningsLista[i].DelaktigaPoliser.Count; y++)
                    {
                        //Skriv ut listan med poliser för nuvarande objekt
                        Console.WriteLine($"{utryckningsLista[i].DelaktigaPoliser[y].Name} {utryckningsLista[i].DelaktigaPoliser[y].LastName}, {utryckningsLista[i].DelaktigaPoliser[y].BadgeNumber}");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Det finns inga utryckningar tillagda i listan");
            }
            Console.ReadKey();
        }

        //Anropa metod vid avslut av program för att spara alla listor i separata json-filer
        public static void SaveLists()
        {
            string fileName = "Utryckning.json";
            string fileName1 = "Personal.json";
            string fileName2 = "Rapport.json";
            string utryckningsListaJson = JsonSerializer.Serialize(utryckningsLista);
            string personalListaJson = JsonSerializer.Serialize(personalLista);
            string rapportListaJson = JsonSerializer.Serialize(rapportLista);
            File.WriteAllText(fileName, utryckningsListaJson);
            File.WriteAllText(fileName1, personalListaJson);
            File.WriteAllText(fileName2, rapportListaJson);
            Console.WriteLine(utryckningsListaJson);
            Console.WriteLine(personalListaJson);
            Console.WriteLine(rapportListaJson);

        }

        //Anropa metod vid start av program för att ladda alla listor från separata json-filer
        public static void LoadLists()
        {
            string fileName = "Utryckning.json";
            string fileName1 = "Personal.json";
            string fileName2 = "Rapport.json";
            string jsonString = File.ReadAllText(fileName);
            string jsonString1 = File.ReadAllText(fileName1);
            string jsonString2 = File.ReadAllText(fileName2);
            utryckningsLista = JsonSerializer.Deserialize<List<Utryckning>>(jsonString);
            personalLista = JsonSerializer.Deserialize<List<Personal>>(jsonString1);
            rapportLista = JsonSerializer.Deserialize<List<Rapport>>(jsonString2);
        }

    }
}