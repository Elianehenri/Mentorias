using Mentorias.Data;
using Mentorias.Interfaces.Repositories;
using Mentorias.Models;

namespace Mentorias.Repository
{
    public class TeacherRepository : ITeacherRepository
    {

        private readonly MentorShipContext _context;

        public TeacherRepository(MentorShipContext context)
        {
            _context = context;
        }

        public bool CheckCPF(string cpf)
        {
            return _context.Teachers.Any(x => x.CPF == cpf);
        }

        public bool CheckEmail(string email)
        {
            return _context.Teachers.Any(x => x.Email == email);
        }

        public void CreateTeacher(Teachers teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        public void DeleteTeacher(Teachers teacher)
        {
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
        }

        public Teachers GetTeacherById(int idTeacher)
        {
            return _context.Teachers.FirstOrDefault(x => x.TeacherId == idTeacher);
        }

        public Teachers GetTeacherLogin(string email, string password)
        {
            return _context.Teachers.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public void UpdateTeacher(Teachers teacher)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
        }

        List<Teachers> ITeacherRepository.GetAllTeachers()
        {
           return _context.Teachers.ToList();
        }

        List<MentorShip> ITeacherRepository.GetMentorShipsByTeacherId(int idTeacher)
        {
            throw new NotImplementedException();
        }
    }
}
