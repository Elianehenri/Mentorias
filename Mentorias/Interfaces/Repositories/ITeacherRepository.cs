using Mentorias.Dtos;
using Mentorias.Models;

namespace Mentorias.Interfaces.Repositories
{
    public interface ITeacherRepository : IBaseRepository
    {

        //cria os metods para o repositorio de teacher

        Teachers GetTeacherLogin(string email, string password);
        Teachers GetTeacherById(int idTeacher);

        void CreateTeacher(Teachers  teacher );
        void UpdateTeacher(Teachers teacher);
        void DeleteTeacher(Teachers teacher);

        List<Teachers> GetAllTeachers();
        List<MentorShip> GetMentorShipsByTeacherId(int idTeacher);   




    }
}
