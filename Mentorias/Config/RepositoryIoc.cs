using Mentorias.Interfaces.Repositories;
using Mentorias.Repository;

namespace Mentorias.Config
{
    public class RepositoryIoc
    {
        public static void RegisterServices(IServiceCollection builder)
        {
            //builder.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.AddScoped<IStudentRepository, StudentRepositoriy>();
        }
           
    }
}
