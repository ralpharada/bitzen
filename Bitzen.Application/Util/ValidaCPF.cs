using System.Text.RegularExpressions;

namespace Bitzen.Application.Util
{
    public class ValidaCPF
    {
        public static bool Validar(string val)
        {
            // Remove todos os caracteres não numéricos
            string cpfLimpo = Regex.Replace(val, "[^0-9]", "");

            // Verifica se o CPF tem 11 dígitos
            if (cpfLimpo.Length != 11)
            {
                return false;
            }

            // Verifica se todos os dígitos são iguais
            if (new string(cpfLimpo[0], 11) == cpfLimpo)
            {
                return false;
            }

            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (cpfLimpo[i] - '0') * (10 - i);
            }
            int digito1 = 11 - (soma % 11);
            if (digito1 > 9)
            {
                digito1 = 0;
            }

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (cpfLimpo[i] - '0') * (11 - i);
            }
            int digito2 = 11 - (soma % 11);
            if (digito2 > 9)
            {
                digito2 = 0;
            }

            // Verifica se os dígitos verificadores calculados correspondem aos dígitos no CPF
            return digito1 == (cpfLimpo[9] - '0') && digito2 == (cpfLimpo[10] - '0');
        }
    }
}
