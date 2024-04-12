using ServerSide.BusinessLogic.Interfaces;

namespace ServerSide.PalindromeValidator
{
    public class PalindromeValidator : IPalindromeValidator
    {
        public bool IsValid(string value)
        {
            value = PrepareString(value);
            bool isValid = true;
            for (int i = 0; i < value.Length / 2; i++)
            {
                char startChar = value[i];
                char endChar = value[value.Length - 1 - i];
                if (startChar != endChar)
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private string PrepareString(string value)
        {
            string result = "";
            // удаляем лишние пробелы и приводим к строчным буквам
            value = value.Trim().ToLower();
            // удаляем все символы, кроме букв и чисел
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLetterOrDigit(value[i]))
                {
                    result += value[i];
                }
            }
            return result;
        }
    }
}
