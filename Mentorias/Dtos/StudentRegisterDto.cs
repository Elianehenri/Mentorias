﻿using Mentorias.Enums;

namespace Mentorias.Dtos
{
    public class StudentRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }

        public StudentRegisterDto()
        {
        }
    }
}
