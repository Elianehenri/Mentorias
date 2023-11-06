using Mentorias.Models;

namespace Mentorias.Interfaces.Repositories
{
    public interface IMentorRepository
    {
        
        MentorShip GetMentorshipById(int id);
        List<MentorShip> GetAllMentorships();
        void CreateMentorship(MentorShip mentorship);
        void UpdateMentorship(MentorShip mentorship);
        void DeleteMentorship(int id);
        List<MentorShip> GetMentorshipsByDateTime(DateTime date);
        
    }
}
