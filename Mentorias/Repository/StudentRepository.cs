using Mentorias.Data;
using Mentorias.Interfaces.Repositories;
using Mentorias.Models;

namespace Mentorias.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MentorShipContext _context;

        public StudentRepository(MentorShipContext context)
        {
            _context = context;
        }

        //criar student
        public void CreateStudent(Students student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
             
        }

        //excluir student
        public void DeleteStudent(Students student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        //obter todos os students
        public List<Students> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public List<MentorShip> GetMentorShipsByStudentId(int id)
        {
            return _context.Mentorships.Where(x => x.StudentId == id).ToList();
            
        }

        //buscar por id
        public Students GetStudentById(int idStudent)
        {
           return _context.Students.FirstOrDefault(x => x.StudentId == idStudent);
        }

        public Students GetStudentLogin(string email, string password)
        {
            return _context.Students.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public bool CheckEmail(string email)
        {
            return _context.Students.Any(x => x.Email == email);
        }

        public void UpdateStudent(Students student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public bool  CheckCPF(string cpf)
        {
            return _context.Students.Any(x => x.CPF == cpf);
        }
        

    }
}
