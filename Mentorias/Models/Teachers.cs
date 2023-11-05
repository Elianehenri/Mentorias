using Mentorias.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mentorias.Models
{
    public class Teachers
    {
        public int TeacherId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        [JsonIgnore]
        public string CPF { get; set; }
        [Required]
        public string AcademicSpecialization { get; set; }
        public UserType UserType { get; set; }
        public List<MentorShip> MentorshipsAsProfessor { get; set; }
     
    }
}