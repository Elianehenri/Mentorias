using Mentorias.Dtos;
using Mentorias.Interfaces.Repositories;
using Mentorias.Interfaces.Services;
using Mentorias.Models;
using System.Globalization;

namespace Mentorias.Services
{
    public class MentorService : IMentorService
    {
        private readonly IMentorRepository _mentorRepository;
        private readonly IConfiguration _configuration;

        public MentorService(IMentorRepository mentorRepository, IConfiguration configuration)
        {
            _mentorRepository = mentorRepository;
            _configuration = configuration;
        }

        public MentorShipDto GetMentorshipById(int id)
        {
            
            // Verificar se id existe no banco de dados
            MentorShip mentorship = _mentorRepository.GetMentorshipById(id);
            if (mentorship == null)
            {
                throw new Exception("Mentoria não encontrada.");
            }

             MentorShipDto mentorShipDto = new ()
            {
                MentorshipId = mentorship.MentorshipId,
                Date = mentorship.Date,
                StartTime = mentorship.StartTime,
                EndTime = mentorship.EndTime,
                Subject = mentorship.Subject,
                TeacherId = mentorship.TeacherId,
                StudentId = mentorship.StudentId
            };
            return mentorShipDto;


        }


        public List<MentorShip> GetAllMentorships()
        {
            return _mentorRepository.GetAllMentorships();

        }

        public void CreateMentorship(MentorShipDto mentorshipDto)
        {
            // Regra 1: Não pode cadastrar agendamentos com horários em conflito
            if (!IsTimeConflict(mentorshipDto))
            {
                // Regra 2: Se não for informado horário de fim, assumir agendamento de 1h
                if (mentorshipDto.EndTime == default)
                {
                    mentorshipDto.EndTime = mentorshipDto.StartTime.Add(TimeSpan.FromHours(1));
                }

                // Convertemos o MentorShipDto de volta para MentorShip
                var mentorship = new MentorShip
                {
                    Date = mentorshipDto.Date,
                    StartTime = mentorshipDto.StartTime,
                    EndTime = mentorshipDto.EndTime,
                    Subject = mentorshipDto.Subject,
                    TeacherId = mentorshipDto.TeacherId,
                    StudentId = mentorshipDto.StudentId
                };

                _mentorRepository.CreateMentorship(mentorship);
            }
            else
            {
                throw new Exception("Já existe uma mentoria agendada para este horário.");
            }
        }
    




        public void UpdateMentorship(MentorShipDto mentorship, int userId)
        {
            // Verifique se o agendamento pertence ao usuário autenticado.
            var mentorshipDb = _mentorRepository.GetMentorshipById(mentorship.MentorshipId) ?? throw new Exception("Mentoria não encontrada");

            if (mentorshipDb.TeacherId != userId && mentorshipDb.StudentId != userId)
            {
                throw new Exception("Você não tem permissão para alterar esta mentoria.");
            }

            // Atualize os campos do objeto mentorshipDb com os dados do mentorshipRequest.
            mentorshipDb.Date = mentorship.Date;
            mentorshipDb.StartTime = mentorship.StartTime;
            mentorshipDb.EndTime = mentorship.EndTime;

            // Realize a atualização da mentoria no repositório
            _mentorRepository.UpdateMentorship(mentorshipDb);

        }


        public void  DeleteMentorship(int id)
        {
           
           var mentorship = _mentorRepository.GetMentorshipById(id);
            _mentorRepository.DeleteMentorship(id);

        }

        public List<MentorShip>  GetMentorshipsByDateTime(DateTime date)
        {
          
            return _mentorRepository.GetMentorshipsByDateTime(date);

        }

        private bool IsTimeConflict(MentorShipDto mentorshipDto)
        {
            // Verifique se já existe uma mentoria com horários em conflito
            var mentorships = _mentorRepository.GetAllMentorships();
            return mentorships.Any(existingMentorship =>
                existingMentorship.TeacherId == mentorshipDto.TeacherId &&
                existingMentorship.Date.Date == mentorshipDto.Date.Date &&
                existingMentorship.MentorshipId != mentorshipDto.MentorshipId &&
                TimeOverlap(existingMentorship.StartTime, existingMentorship.EndTime, mentorshipDto.StartTime, mentorshipDto.EndTime));
        }


        private bool TimeOverlap(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
        {
            return start1 < end2 && end1 > start2;
        }

       
    }
}
