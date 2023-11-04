using Mentorias.Dtos;
using Mentorias.Models;

namespace Mentorias.Interfaces.Services
{
    public interface ITeacherService : IValidaService
    {
        public Teachers GetTeacherLogin(string email, string password);
        public LoginResponseDto Login(LoginRequestDto loginRequestDto);
        TeacherResponseDto GetTeacherById(int idTeacher);
        void CreateTeacher(TeacherRegisterDto teacher);
        void UpdateTeacher(TeacherRequestDto teacherRequest, int? idTeacher);
        void DeleteTeacher(int idTeacher);
        List<Teachers> GetAllTeachers();
        List<MentorShip> GetMentorShipsByTeacherId(int idTeacher);

    }
}





 