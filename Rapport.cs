namespace Polisen
{
    public class Rapport 
    {
        private string station;
        private string beskrivning;
        public string Date { get; set; }
        private static int nextRapportNummer = 1;
        public int RapportNummer { get; set; }
        public void NewRapportNummer()
        {
            RapportNummer = nextRapportNummer++;         
        }
        public string Station 
        { 
            get { return station; }
            set { station = char.ToUpper(value[0]) + value.Substring(1).ToLower(); }
        }
        public string Beskrivning
        { 
            get { return beskrivning; }
            set { beskrivning = char.ToUpper(value[0]) + value.Substring(1).ToLower(); }
        }
        public void AddCurrentDate()
        {
            DateTime currentDate = DateTime.Now.Date;
            string dateString = currentDate.ToString("yyyy-MM-dd");
            Date = dateString;
        }
    }
}