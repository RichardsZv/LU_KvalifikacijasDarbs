namespace SupervaroniApp.Models
{
    public class Report
    {
        public int Id{ get; set; }
        public DateTime Datums { get; set; }
        public string Ieplanots { get; set; }
        public string Izpildits{ get; set; }
        public string Komentars { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
