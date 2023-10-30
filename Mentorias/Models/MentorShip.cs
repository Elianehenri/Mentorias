namespace Mentorias.Models
{
    public class MentorShip // agendamento de mentorias.
    {
        public int MentorShipId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Subject { get; set; }
        public User Student { get; set; }
        public User Professor { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
