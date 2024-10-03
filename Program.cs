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
            //Ladda data från json-fil till Listor i RapportSystem vid program start
            HandleSystem.LoadLists();

            Menu.MainMenu();
            Menu.ExitMenu();

            //Vid avslut av program, Spara/Lägg till all ny data till json-fil från Listor i RapportSystem
            HandleSystem.SaveLists();

            Console.Clear();
            Console.WriteLine("Välkommen åter!");
            Console.WriteLine("Tryck på valfri tangent för att avsluta programmet...");
            Console.ReadKey();
        }    
    }
}
