using Mentorias.Enums;

namespace Mentorias.Models
{
    public class Teachers
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public string AcademicSpecialization { get; set; }
        public UserType UserType { get; set; }
        public List<MentorShip> MentorshipsAsProfessor { get; set; }
     
    }
}