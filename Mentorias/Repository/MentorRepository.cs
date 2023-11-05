using Mentorias.Data;
using Mentorias.Interfaces.Repositories;
using Mentorias.Models;

namespace Mentorias.Repository
{
    public class MentorRepository : IMentorRepository
    {
        private readonly MentorShipContext _context;

        public MentorRepository(MentorShipContext context)
        {
            _context = context;
        }

       
        public MentorShip GetMentorshipById(int id)
        {
            return _context.Mentorships.Find(id);
        }

        public List<MentorShip> GetAllMentorships()
        {

            return _context.Mentorships.ToList();
            
        }

        public void CreateMentorship(MentorShip mentorship)
        {
   
            _context.Mentorships.Add(mentorship);
            _context.SaveChanges();
        }

        public void UpdateMentorship(MentorShip mentorship)
        {
            _context.Mentorships.Update(mentorship);
            _context.SaveChanges();
        }

        public void DeleteMentorship(int id)
        {
            var mentorship = _context.Mentorships.Find(id);
            _context.Mentorships.Remove(mentorship);
            _context.SaveChanges();
        }
    }
}
