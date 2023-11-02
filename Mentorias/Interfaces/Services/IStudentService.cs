﻿using Mentorias.Dtos;
using Mentorias.Models;

namespace Mentorias.Interfaces.Services
{
    public interface IStudentService
    {
       
        public Students GetStudentLogin(string email, string password); //ok -obter de estudantes por email e senha
        public LoginResponseDto Login(LoginRequestDto loginRequestDto);//ok - login
        StudentResponseDto GetStudentById(int idStudent);//ok - studant
        void CreateStudent( StudentRegisterDto student);//ok -cadastro de estudante

        void UpdateStudent(StudentRequestDto studentRequest);//ok - atualizar estudante
        void DeleteStudent(int idStudent);//ok -deletar estudante
        bool CheckEmail(string email);//ok - cadastro
        bool CheckCPF(string cpf);//ok - cadastro
        List<Students> GetAllStudents();//ok - obter todos os estudantes

        List<MentorShip> GetMentorShipsByStudentId(int id);//obter todas as mentorias por id de estudante

    }
}