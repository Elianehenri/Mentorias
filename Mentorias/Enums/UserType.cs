using System.ComponentModel.DataAnnotations;

namespace Mentorias.Enums
{
    public enum UserType
    {
        [Display(Name = "Student")]
        Student= 1,
        [Display(Name = "Teacher")]
        Professor = 2
    }
}
