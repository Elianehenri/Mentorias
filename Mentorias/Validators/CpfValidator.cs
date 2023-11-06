
using System.Text.RegularExpressions;

namespace Mentorias.Validators
{
    public partial class CpfValidator
    {
        public static bool IsValidCpf(string cpf)
        {
            // Remova todos os caracteres não numéricos (exceto dígitos)
            cpf = MyRegex().Replace(cpf, "");

            // Verifique se o CPF tem 11 dígitos
            if (cpf.Length != 11)
            {
                return false;
            }

            // Verifique se todos os dígitos são iguais (CPF inválido)
            if (new string(cpf[0], 11) == cpf)
            {
                return false;
            }

            // Validação do CPF
            int[] weights1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weights2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string partialCpf = cpf[..9];
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(partialCpf[i].ToString()) * weights1[i];
            }

            int remainder1 = sum % 11;
            remainder1 = remainder1 < 2 ? 0 : 11 - remainder1;

            partialCpf += remainder1;

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(partialCpf[i].ToString()) * weights2[i];
            }

            int remainder2 = sum % 11;
            remainder2 = remainder2 < 2 ? 0 : 11 - remainder2;


            // Verifique se o CPF válido é igual ao CPF fornecido
            return cpf.EndsWith(remainder1.ToString() + remainder2.ToString());
        }

        [GeneratedRegex("[^\\d]")]
        private static partial Regex MyRegex();
    }
}
