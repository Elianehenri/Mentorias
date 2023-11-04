using Mentorias.Dtos;
using Mentorias.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherAuthController : ControllerBase
    {
        private readonly ILogger<TeacherAuthController> _logger;
        private readonly ITeacherService _teacherService;

        public TeacherAuthController(ILogger<TeacherAuthController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register([FromBody] TeacherRegisterDto teacherRegisterDto)
        {
            try
            {
                _teacherService.CreateTeacher(teacherRegisterDto);
                return Ok("Professor cadastrado com sucesso");
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

                var teacher = _teacherService.Login(loginRequestDto);
                return Ok(teacher);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}
