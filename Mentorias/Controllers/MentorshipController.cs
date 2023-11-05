using Mentorias.Dtos;
using Mentorias.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentorshipController : ControllerBase
    {
        private readonly IMentorService _mentorService;
        private readonly ILogger<MentorshipController> _logger;

        public MentorshipController(IMentorService mentorService, ILogger<MentorshipController> logger)
        {
            _mentorService = mentorService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var mentorships = _mentorService.GetAllMentorships();
                return Ok(mentorships);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar mentoria");
                return StatusCode(500, "Erro ao buscar mentoria");
            }
          
        }

        [HttpGet]
        [Route("id")]
        public IActionResult Get(int id)
        {
            try
            {
                var mentorship = _mentorService.GetMentorshipById(id);
                return Ok(mentorship);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar mentoria");
                return StatusCode(500, "Erro ao buscar mentoria");
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] MentorShipDto mentorshipdto)
        {
            try
            {
                _mentorService.CreateMentorship(mentorshipdto);
                return Ok("Mentoria agendada com sucesso.");
            }
            catch (Exception ex)
            {
              
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] MentorShipDto mentorship)
        {
            try
            {
                _mentorService.UpdateMentorship(mentorship, 1);
                return Ok("Atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar mentoria");
                return StatusCode(500, "Erro ao atualizar mentoria");
            }
           
        }

        
        [HttpDelete ]
        [Authorize]
        public IActionResult Delete(int id)
        {
           
           try
            {
                _mentorService.DeleteMentorship(id);
                return Ok("Deletado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar mentoria");
                return StatusCode(500, "Erro ao deletar mentoria");
            }
        }
       
        
    }
}
