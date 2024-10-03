namespace Polisen
{
    public class Utryckning 
    {
        private string brottsTyp { get; set; }
        private string plats { get; set; }
        public string Time { get; set; }   

        //Icke statisk lista som är enskild för varje objekt, innehåller en eller flera poliser från klassen Personal
        public List<Personal> DelaktigaPoliser { get; set; } = new List<Personal>();

        public string BrottsTyp 
        { 
            get { return brottsTyp; }
            set { brottsTyp = char.ToUpper(value[0]) + value.Substring(1).ToLower(); }
        }
        public string Plats 
        { 
            get { return plats; }
            set { plats = char.ToUpper(value[0]) + value.Substring(1).ToLower(); }
        }
        public void AddCurrentTime()
        {
            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("HH:mm");
            Time = time;
        }
    }
}