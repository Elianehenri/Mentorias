namespace Mentorias.Dtos
{
    public class MentorShipDto
    {
        public int MentorshipId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Subject { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
    }
}
