using System.Text.RegularExpressions;
namespace Canducci.MVC.Validation
{
    internal class AssertionConcern
    {

        internal static bool ValidateCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            Regex reg = new Regex(@"[^0-9]");
            cpf = reg.Replace(cpf, string.Empty);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool equals = true;
            for (int i = 1; i < 11 && equals; i++)
                if (cpf[i] != cpf[0])
                    equals = false;

            if (equals || cpf == "12345678909")
                return false;

            int[] numbers = new int[11];

            for (int i = 0; i < 11; i++)
                numbers[i] = int.Parse(cpf[i].ToString());

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (10 - i) * numbers[i];

            int result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != 11 - result)
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (11 - i) * numbers[i];

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }
            else if (numbers[10] != 11 - result)
                return false;
            return true;
        }

        internal static bool ValidateCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return false;
            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            int remainder = 0;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            var tempCnpj = cnpj.Substring(0, 12);
            sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;
            var digit = remainder.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier[i];
            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;
            digit = digit + remainder;
            return cnpj.EndsWith(digit);
        }

    }
}
