using Mentorias.Dtos;
using Mentorias.Enums;
using Mentorias.Interfaces.Repositories;
using Mentorias.Interfaces.Services;
using Mentorias.Models;
using Mentorias.Security;
using Mentorias.Utils;
using Mentorias.Validators;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Mentorias.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;

        public StudentService(IStudentRepository studentRepository, IConfiguration configuration)
        {
            _studentRepository = studentRepository;
            _configuration = configuration;
        }

        public void CreateStudent(StudentRegisterDto studentRegisterDto)
        {
            // Verificar email, nome, senha e CPF vazios
            if (string.IsNullOrEmpty(studentRegisterDto.Email) || string.IsNullOrWhiteSpace(studentRegisterDto.Email) ||
                string.IsNullOrEmpty(studentRegisterDto.Name) || string.IsNullOrWhiteSpace(studentRegisterDto.Name) ||
                string.IsNullOrEmpty(studentRegisterDto.Password) || string.IsNullOrWhiteSpace(studentRegisterDto.Password) ||
                string.IsNullOrEmpty(studentRegisterDto.CPF) || string.IsNullOrWhiteSpace(studentRegisterDto.CPF))
            {
                throw new Exception("Email, nome, senha e CPF são obrigatórios");
            }

            // Verificar se o email está em um formato válido usando a classe EmailValidator
            if (!EmailValidator.IsValidEmail(studentRegisterDto.Email))
            {
                throw new Exception("Formato de email inválido");
            }

            // Verificar se a senha é válida usando a classe PasswordValidator
            if (!PasswordValidator.IsValidPassword(studentRegisterDto.Password))
            {
                throw new Exception("A senha não atende aos critérios de segurança");
            }

            // Verificar se o email já existe no banco de dados
            if (_studentRepository.CheckEmail(studentRegisterDto.Email))
            {
                throw new Exception("Email já cadastrado");
            }

            // Verificar se o CPF já existe no banco de dados
            if (_studentRepository.CheckCPF(studentRegisterDto.CPF))
            {
                throw new Exception("CPF já cadastrado");
            }

            // Verificar se o CPF é válido usando a classe CpfValidator
            if (CpfValidator.IsValidCpf(studentRegisterDto.CPF))
            {
                var student = new Students
                {
                    Name = studentRegisterDto.Name,
                    Email = studentRegisterDto.Email.ToLower(),
                    CPF = studentRegisterDto.CPF,
                    Password = HashUtility.GenerateSHA256Hash(studentRegisterDto.Password),
                    UserType = studentRegisterDto.UserType
                };

                // Salvar no banco
                _studentRepository.CreateStudent(student);
            }
            else
            {
                
                throw new Exception("CPF inválido");
            }
        }



        public void DeleteStudent(int idStudent)
        {
            var student = _studentRepository.GetStudentById(idStudent);
            _studentRepository.DeleteStudent(student);
        }

        public List<Students> GetAllStudents()
        {
            //obter todos os estudantes
            return _studentRepository.GetAllStudents();

        }

        public List<MentorShip> GetMentorShipsByStudentId(int id)
        {
            throw new NotImplementedException();
        }

        public StudentResponseDto GetStudentById(int idStudent)
        {
            // Verificar se id é vazio
            if (idStudent == 0)
            {
                throw new Exception("Id é obrigatório");
            }

            // Verificar se id existe no banco de dados
            Students student = _studentRepository.GetStudentById(idStudent) ?? throw new Exception("Id não encontrado");

            // Retornar um objeto StudentResponseDto
            StudentResponseDto studentResponseDto = new StudentResponseDto
            {
                Name = student.Name,
                Email = student.Email,
                UserType = student.UserType
                //UserType = (UserType)student.UserType
            };

            return studentResponseDto;
        }


        public Students GetStudentLogin(string email, string password)
        {
            //Verificar se student existe no banco de dados
            Students student = _studentRepository.GetStudentLogin(email.ToLower(), HashUtility.GenerateSHA256Hash(password));
            if (student == null)
            {
                throw new Exception("Email ou senha inválidos");
            }
            return student;
        }

        
        public void UpdateStudent(StudentRequestDto studentRequest, int? idStudent)
        {
            // Verifique se o usuário autenticado está atualizando seu próprio perfil.
            var studentDb = _studentRepository.GetStudentById(studentRequest.Id);

            if (studentDb == null)
            {
                throw new Exception("Estudante não encontrado");
            }

            if (studentDb.StudentId != idStudent)
            {
                throw new Exception("Você não tem permissão para atualizar este perfil");
            }

            studentDb.Name = studentRequest.Name;
            studentDb.Email = studentRequest.Email;

            _studentRepository.UpdateStudent(studentDb);
        }

        public bool CheckEmail(string email)
        {
            //verificar se email existe no banco de dados
            return _studentRepository.CheckEmail(email);
        }

        bool IStudentService.CheckCPF(string cpf)
        {
           //verficar se cpf existe no banco de dados
            return _studentRepository.CheckCPF(cpf);
        }

        LoginResponseDto IStudentService.Login(LoginRequestDto loginRequestDto)
        {
            // Verificar se email e senha estão vazios ou inválidos
            if (string.IsNullOrEmpty(loginRequestDto.Email) || string.IsNullOrWhiteSpace(loginRequestDto.Email) ||
                               string.IsNullOrEmpty(loginRequestDto.Password) || string.IsNullOrWhiteSpace(loginRequestDto.Password))
            {
                throw new Exception("Email e senha são obrigatórios");
            }
            // Verificar se o email existe no banco de dados e se as credenciais estão corretas
            Students student = _studentRepository.GetStudentLogin(loginRequestDto.Email.ToLower(), HashUtility.GenerateSHA256Hash(loginRequestDto.Password));
            if (student != null)
            {
                return new LoginResponseDto
                {
                    Name = student.Name,
                    Email = student.Email,
                    Token = TokenService.GenerateToken(student, _configuration["JWT:SecretKey"])
                };
            }
            else
            {
                throw new Exception("Email ou senha inválidos");
            }


        }
    }
}
