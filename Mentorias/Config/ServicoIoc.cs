using Mentorias.Interfaces.Repositories;
using Mentorias.Interfaces.Services;
using Mentorias.Repository;
using Mentorias.Services;

namespace Mentorias.Config
{
    public class ServicoIoc
    {
        public static void RegisterServices(IServiceCollection builder)
        {
            builder.AddScoped<ITeacherService, TeacherService>();
            builder.AddScoped<IStudentService, StudentService>();
            
        }
    }
}
