namespace Bitzen.Application.Util
{
    public class ValidaData
    {
        public static bool Validar(string val)
        {
            if (string.IsNullOrEmpty(val))
                return false;
            return DateTime.TryParse(val, out _);
        }
    }
}
