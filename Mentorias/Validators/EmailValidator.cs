using System.Text.RegularExpressions;

namespace Mentorias.Validators
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            // Utilizar uma expressão regular para verificar o formato do email
            string pattern = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
