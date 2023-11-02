//namespace Mentorias.Validators
//{
//    //public class CpfValidator
//{
//    public static bool IsValidCpf(string cpf)
//    {
//        // Remova qualquer formatação do CPF, como pontos e traços
//        cpf = new string(cpf.Where(char.IsDigit).ToArray());

//        // Verifique se o CPF tem 11 dígitos
//        if (cpf.Length != 11)
//        {
//            return false;
//        }

//        // Verifique se todos os dígitos são iguais (CPF inválido se todos forem iguais)
//        if (cpf.All(digit => digit == cpf[0]))
//        {
//            return false;
//        }

//        // Calcula o primeiro dígito verificador
//        int sum = 0;
//        for (int i = 0; i < 9; i++)
//        {
//            sum += (int)char.GetNumericValue(cpf[i]) * (10 - i);
//        }
//        int remainder = 11 - (sum % 11);
//        int firstVerifier = remainder >= 10 ? 0 : remainder;

//        // Verifique se o primeiro dígito verificador é igual ao décimo dígito do CPF
//        if (firstVerifier != (int)char.GetNumericValue(cpf[9]))
//        {
//            return false;
//        }

//        // Calcula o segundo dígito verificador
//        sum = 0;
//        for (int i = 0; i < 10; i++)
//        {
//            sum += (int)char.GetNumericValue(cpf[i]) * (11 - i);
//        }
//        remainder = 11 - (sum % 11);
//        int secondVerifier = remainder >= 10 ? 0 : remainder;

//        // Verifique se o segundo dígito verificador é igual ao décimo primeiro dígito do CPF
//        return secondVerifier == (int)char.GetNumericValue(cpf[10]);
//    }

//}

//}
using System;
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
