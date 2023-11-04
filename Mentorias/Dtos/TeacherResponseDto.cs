using Mentorias.Enums;

namespace Mentorias.Dtos
{
    public class TeacherResponseDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public UserType UserType { get; set; }
        public TeacherResponseDto() { }

    }
}
