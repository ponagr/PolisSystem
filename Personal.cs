namespace Polisen
{
    public class Personal 
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
    }
}