using Mentorias.Interfaces.Services;
using Mentorias.Services;

namespace Mentorias.Config
{
    public class ServicoIoc
    {
        public static void RegisterServices(IServiceCollection builder)
        {
           //builder.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.AddScoped<IStudentService, StudentService>();
        }
    }
}
