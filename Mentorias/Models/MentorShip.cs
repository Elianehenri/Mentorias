namespace Mentorias.Models
{
    public class MentorShip // agendamento de mentorias.
    {
        public int MentorshipId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Subject { get; set; }//descriçao
        public int TeacherId { get; set; } // Chave estrangeira para o professor (Teachers)
        public int StudentId { get; set; } // Chave estrangeira para o aluno (Students)

        public Students Student { get; set; }
        public Teachers Teacher { get; set; }
    }
}
