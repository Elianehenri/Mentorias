//using Mentorias.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Mentorias.Data
//{
//    public class MentorShipContext : DbContext
//    {
//        public MentorShipContext(DbContextOptions<MentorShipContext> options) : base(options)
//        {
//        }

//        public DbSet<MentorShip> Mentorships { get; set; }
//        public DbSet<Students> Students { get; set; }
//        public DbSet<Teachers> Teachers { get; set; }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<MentorShip>().ToTable("Mentorship");
//            modelBuilder.Entity<Students>()
//                .ToTable("Students")
//                .HasKey(s => s.StudentId);
//            modelBuilder.Entity<Teachers>()
//                .ToTable("Teachers")
//                .HasKey(t => t.TeacherId); // Define TeacherId como a chave primária


//            modelBuilder.Entity<MentorShip>()
//                .HasOne(m => m.Student)
//                .WithMany(s => s.MentorshipsAsStudent)
//                .HasForeignKey(m => m.StudentId);

//            modelBuilder.Entity<MentorShip>()
//                .HasOne(m => m.Teacher)
//                .WithMany(t => t.MentorshipsAsProfessor)
//                .HasForeignKey(m => m.TeacherId);


//        }


//    }

//}
using Mentorias.Models;
using Microsoft.EntityFrameworkCore;

namespace Mentorias.Data
{
    public class MentorShipContext : DbContext
    {
        public MentorShipContext(DbContextOptions<MentorShipContext> options) : base(options)
        {
        }

        public DbSet<MentorShip> Mentorships { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Teachers> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MentorShip>().ToTable("Mentorship");

            modelBuilder.Entity<Students>()
                .ToTable("Students")
                .HasKey(s => s.StudentId);

            modelBuilder.Entity<Teachers>()
                .ToTable("Teachers")
                .HasKey(t => t.TeacherId);

            modelBuilder.Entity<Students>()
                .HasMany(s => s.MentorshipsAsStudent)
                .WithOne(m => m.Student)
                .HasForeignKey(m => m.StudentId);

            modelBuilder.Entity<Teachers>()
                .HasMany(t => t.MentorshipsAsProfessor)
                .WithOne(m => m.Teacher)
                .HasForeignKey(m => m.TeacherId);
        }
    }
}
