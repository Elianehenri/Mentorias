namespace Mentorias.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        bool CheckEmail(string email);//ok - cadastro
        bool CheckCPF(string cpf);//ok - cadastro
    }
}
