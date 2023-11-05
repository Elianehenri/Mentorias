using Mentorias.Dtos;
using Mentorias.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAuthController : ControllerBase
    { //controller de log
        private readonly ILogger<StudentAuthController> _logger;
        private readonly IStudentService _studentService;

        public StudentAuthController(ILogger<StudentAuthController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register([FromBody] StudentRegisterDto studentRegisterDto)
        {
            try
            {
                _studentService.CreateStudent(studentRegisterDto);
                return Ok("Estudante cadastrado com sucesso");
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);

            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                if (loginRequestDto == null)
                {
                    return BadRequest("Dados de login inválidos.");
                }

                var student= _studentService.Login(loginRequestDto);
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
