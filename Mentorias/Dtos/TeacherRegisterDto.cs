using Mentorias.Enums;

namespace Mentorias.Dtos
{
    public class TeacherRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; } // TODO: Criar validação de CPF
        public string Password { get; set; }
        public string AcademicSpecialization { get; set; }
        public UserType UserType { get; set; }

    }
}
