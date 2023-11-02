using Mentorias.Models;

namespace Mentorias.Interfaces.Repositories
{
    public interface IStudentRepository
    {

        //cria os metods para o repositorio de estudantes

        Students GetStudentLogin(string email, string password); //ok -obter de estudantes por email e senha
        Students GetStudentById(int idStudent);//ok - obter de estudantes por id
        void CreateStudent(Students student);//ok - criar estudante
        void UpdateStudent(Students student);// ok - atualizar estudante
        void DeleteStudent(Students student);//ok - deletar estudante
        bool CheckEmail(string email);//ok - cadastro
        bool CheckCPF(string cpf);//ok - cadastro
        List<Students> GetAllStudents();//ok - obter todos os estudantes
      
        List<MentorShip> GetMentorShipsByStudentId(int id);//obter todas as mentorias por id de estudante

    }
}
