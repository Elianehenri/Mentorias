using Mentorias.Dtos;
using Mentorias.Models;

namespace Mentorias.Interfaces.Services
{
    public interface IMentorService
    {
        MentorShipDto GetMentorshipById(int id);//ok - obter mentorias por id

        List<MentorShip> GetAllMentorships();//ok - obter todas as mentorias 
        void CreateMentorship(MentorShipDto mentorshipdto);//ok - cadastro de mentoria 
        void UpdateMentorship(MentorShipDto mentorship, int userId);   //ok - atualizar mentoria  //void UpdateMentorship(MentorShipRequestDto mentorshipRquestdto);//ok - atualizar mentoria
        void DeleteMentorship(int id );//ok - deletar mentoria
        
       
    }
}
