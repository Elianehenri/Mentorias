using Mentorias.Enums;

namespace Mentorias.Models
{
    public class Students
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public  string CPF { get; set; }
        public UserType UserType { get; set; }
        public List<MentorShip> MentorshipsAsStudent { get; set; }



    }

}

