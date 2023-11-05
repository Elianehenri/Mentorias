using Mentorias.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mentorias.Models
{
    public class Students
    {
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        [JsonIgnore]
        public  string CPF { get; set; }
        public UserType UserType { get; set; }
        public List<MentorShip> MentorshipsAsStudent { get; set; }



    }

}

