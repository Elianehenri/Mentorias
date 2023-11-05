using Mentorias.Dtos;
using Mentorias.Enums;
using Mentorias.Interfaces.Repositories;
using Mentorias.Interfaces.Services;
using Mentorias.Models;
using Mentorias.Utils;
using Mentorias.Validators;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Mentorias.Services
{
    public class TeacherService : ITeacherService
    {

        private readonly ITeacherRepository _teacherRepository;
        private readonly IConfiguration _configuration;

        public TeacherService(ITeacherRepository teacherRepository, IConfiguration configuration)
        {
            _teacherRepository = teacherRepository;
            _configuration = configuration;
        }
        public bool CheckCPF(string cpf)
        {
            //verficar se cpf existe no banco de dados
            return _teacherRepository.CheckCPF(cpf);
        }

        public bool CheckEmail(string email)
        {
            //verificar se email existe no banco de dados
            return _teacherRepository.CheckEmail(email);
        }


       public void CreateTeacher(TeacherRegisterDto teacherRegisterDto)
        {
            // Verificar email, nome, senha e CPF vazios
            if (string.IsNullOrEmpty(teacherRegisterDto.Email) || string.IsNullOrWhiteSpace(teacherRegisterDto.Email) ||
                string.IsNullOrEmpty(teacherRegisterDto.Name) || string.IsNullOrWhiteSpace(teacherRegisterDto.Name) ||
                string.IsNullOrEmpty(teacherRegisterDto.Password) || string.IsNullOrWhiteSpace(teacherRegisterDto.Password) ||
                string.IsNullOrEmpty(teacherRegisterDto.CPF) || string.IsNullOrWhiteSpace(teacherRegisterDto.CPF))
            {
                throw new Exception("Email, nome, senha e CPF são obrigatórios");
            }

            // Verificar se o email está em um formato válido usando a classe EmailValidator
            if (!EmailValidator.IsValidEmail(teacherRegisterDto.Email))
            {
                throw new Exception("Formato de email inválido");
            }

            // Verificar se a senha é válida usando a classe PasswordValidator
            if (!PasswordValidator.IsValidPassword(teacherRegisterDto.Password))
            {
                throw new Exception("A senha não atende aos critérios de segurança");
            }

            // Verificar se o email já existe no banco de dados
            if (_teacherRepository.CheckEmail(teacherRegisterDto.Email))
            {
                throw new Exception("Email já cadastrado");
            }

            // Verificar se o CPF já existe no banco de dados
            if (_teacherRepository.CheckCPF(teacherRegisterDto.CPF))
            {
                throw new Exception("CPF já cadastrado");
            }
            // Verificar se o CPF é válido usando a classe CpfValidator
            if (CpfValidator.IsValidCpf(teacherRegisterDto.CPF))
            {
                var teacher = new Teachers
                {
                    Name = teacherRegisterDto.Name,
                    Email = teacherRegisterDto.Email,
                    Password = HashUtility.GenerateSHA256Hash(teacherRegisterDto.Password),
                    CPF = teacherRegisterDto.CPF,
                    AcademicSpecialization = teacherRegisterDto.AcademicSpecialization,
                    UserType = UserType.Professor

                };
               
                // Salvar no banco
                _teacherRepository.CreateTeacher(teacher);
            }
            else
            {
                throw new Exception("CPF já cadastrado");
            }
        }



        public void DeleteTeacher(int idTeacher)
        {
            var teacher = _teacherRepository.GetTeacherById(idTeacher);
            if (teacher == null)
            {
                throw new Exception("Professor não encontrado");
            }
            _teacherRepository.DeleteTeacher(teacher);
        }

        public List<Teachers> GetAllTeachers()
        {
            return _teacherRepository.GetAllTeachers();
        }

        public List<MentorShip> GetMentorShipsByTeacherId(int idTeacher)
        {
      
            return _teacherRepository.GetMentorShipsByTeacherId(idTeacher);
            
        }

        public TeacherResponseDto GetTeacherById(int idTeacher)
        {
            // Verificar se id é vazio
            if (idTeacher == 0)
            {
                throw new Exception("Id é obrigatório");
            }
            // Verificar se id existe no banco de dados
            Teachers teacher = _teacherRepository.GetTeacherById(idTeacher) ?? throw new Exception("ID não encontrado");

            // Retornar um objeto TeacherResponseDto
            return new TeacherResponseDto
            {
                //Ver se vai funcianar
                Name = teacher.Name,
                Email = teacher.Email,
                UserType = teacher.UserType
            };

        }

        public Teachers GetTeacherLogin(string email, string password)
        {
            //Verificar se teacher existe no banco de dados
            Teachers teacher = _teacherRepository.GetTeacherLogin(email.ToLower(), HashUtility.GenerateSHA256Hash(password));
            if (teacher == null)
            {
                throw new Exception("Email ou senha inválidos");
            }
            return teacher;
        }

         LoginResponseDto ITeacherService.Login(LoginRequestDto loginRequestDto)
        {
            // Verificar se email e senha estão vazios ou inválidos
            if (string.IsNullOrEmpty(loginRequestDto.Email) || string.IsNullOrWhiteSpace(loginRequestDto.Email) ||
                               string.IsNullOrEmpty(loginRequestDto.Password) || string.IsNullOrWhiteSpace(loginRequestDto.Password))
            {
                throw new Exception("Email e senha são obrigatórios");
            }
            // Verificar se o email existe no banco de dados e se as credenciais estão corretas
            Teachers teacher = _teacherRepository.GetTeacherLogin(loginRequestDto.Email.ToLower(), HashUtility.GenerateSHA256Hash(loginRequestDto.Password));
            if (teacher != null)
            {
                return new LoginResponseDto
                {

                    Name = teacher.Name,
                    Email = teacher.Email,
                    Token = TokenService.GenerateToken(teacher, _configuration["JWT:SecretKey"])
                };

            }
            else
            {
                throw new Exception("Email ou senha inválidos");
            }

        }

        public void UpdateTeacher(TeacherRequestDto teacherRequest, int? idTeacher)
        {
            // Verifique se o usuário autenticado está atualizando seu próprio perfil.
            var teacherDb = _teacherRepository.GetTeacherById(teacherRequest.Id);
            if (teacherDb == null)
            {
                throw new Exception("Professor não encontrado");
            }
            if (teacherDb.TeacherId != idTeacher)
            {
                throw new Exception("Você não tem permissão para atualizar este perfil");
            }
            teacherDb.Name = teacherRequest.Name;
            teacherDb.Email = teacherRequest.Email;

            _teacherRepository.UpdateTeacher(teacherDb);
        }

       
    }
}
