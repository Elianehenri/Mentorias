using Mentorias.Dtos;
using Mentorias.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mentorias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllTeacher()
        {
            try
            {
                var teachers = _teacherService.GetAllTeachers();
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar professores");
                return StatusCode(500, "Erro ao buscar professores");
            }
        }

        [HttpGet]
        [Route("{idTeacher}")]
        public IActionResult GetTeacherById(int idTeacher)
        {
            try
            {
                var teacher = _teacherService.GetTeacherById(idTeacher);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar professor");
                return StatusCode(500, "Erro ao buscar professor");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("mentorships")]
        public IActionResult GetMentorShipsByTeacherId()
        {
            var idTeacher = GetAuthenticatedUserId();// Obtenha o ID do usuário autenticado.
            if (idTeacher != null)
            {
                try
                {
                    var mentorShips = _teacherService.GetMentorShipsByTeacherId(idTeacher.Value);
                    return Ok(mentorShips);
                }
                catch (Exception e)
                {
                    _logger.LogError("Erro ao obter mentorias do professor" + e.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                    {
                        Description = "Ocorreu um erro ao obter as mentorias do professor.",
                        Status = StatusCodes.Status500InternalServerError
                    });

                }

            }
            return Unauthorized();
        }

        [HttpDelete]
        public IActionResult DeleteTeacher(int idTeacher)
        {
            try
            {
                _teacherService.DeleteTeacher(idTeacher);
                return Ok("Professor excluido com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar professor");
                return StatusCode(500, "Erro ao deletar professor");
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateTeacherProfile(TeacherRequestDto teacherRequestDto)
        {


            var idTeacher = GetAuthenticatedUserId();// Obtenha o ID do usuário autenticado.
            if (idTeacher != null)
            {
                try
                {
                    _teacherService.UpdateTeacher(teacherRequestDto, idTeacher);
                    return Ok("Perfil do professor atualizado com sucesso");
                }
                catch (Exception e)
                {
                    _logger.LogError("Erro ao atualizar perfil do professor" + e.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                    {
                        Description = "Ocorreu um erro ao atualizar o professor.",
                        Status = StatusCodes.Status500InternalServerError
                    });

                }

            }
            return Unauthorized();
        }

        private int? GetAuthenticatedUserId()
        {
            var idTeacher = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return idTeacher != null ? int.Parse(idTeacher) : (int?)null;
        }
    }
}
