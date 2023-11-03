using Mentorias.Dtos;
using Mentorias.Interfaces.Repositories;
using Mentorias.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mentorias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }


        [HttpGet]
        //somente os professores
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _studentService.GetAllStudents();
                return Ok(students);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao obter todos os estudantes: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = "Ocorreu um erro ao obter todos os estudantes",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpGet]
        [Route("idStudent")]
        //somente os professores
        public IActionResult GetStudentById(int idStudent)
        {
            try
            {
                var student = _studentService.GetStudentById(idStudent);
                return Ok(student);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao obter o estudante: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = "Ocorreu um erro ao obter o estudante",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpDelete]
        //somente os professores

        public IActionResult DeleteStudent(int idStudent)
        {
            try
            {
                _studentService.DeleteStudent(idStudent);
                return Ok("Estudante excluido com sucesso");
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao deletar o estudante: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = "Ocorreu um erro ao deletar o estudante",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        

        [HttpPut]
        [Authorize]
        public IActionResult UpdateStudentProfile(StudentRequestDto studentRequest)
        {
            var idStudent = GetAuthenticatedUserId(); // Obtenha o ID do usuário autenticado.

            if (idStudent != null)
            {
                try
                {
                    _studentService.UpdateStudent(studentRequest, idStudent);
                    return Ok("Perfil do estudante atualizado com sucesso.");
                }
                catch (Exception e)
                {
                    _logger.LogError("Ocorreu um erro ao atualizar o estudante: " + e.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                    {
                        Description = "Ocorreu um erro ao atualizar o estudante",
                        Status = StatusCodes.Status500InternalServerError
                    });
                }
            }

            return Forbid(); //retorna HTTP 403 
            

        }

        private int? GetAuthenticatedUserId()
        {
            var idStudent = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return idStudent != null ? int.Parse(idStudent) : (int?)null;
        }

    }
}
