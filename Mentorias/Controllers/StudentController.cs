using Mentorias.Dtos;
using Mentorias.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Roles = "Student")]

        public IActionResult UpdateStudent( StudentRequestDto studentRequest)
        {
            try
            {
                _studentService.UpdateStudent(studentRequest);
                return Ok("Estudante atualizado com sucesso");
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

    }
}
