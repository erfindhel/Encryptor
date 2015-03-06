using System.Text;

namespace CipherLib
{
    /// <summary>
    /// Класс, предоставляющий методы шифрации и дешифрации 
    /// строки с использованием шифра Виженера
    /// </summary>
    public class Vigenere
    {
        string alphabet;

        /// <summary>
        /// Инициализирует новый экземпляр класса Vigenere, используя заданный алфавит
        /// </summary>
        /// <param name="alphabet">Используемый алфавит</param>
        public Vigenere(string alphabet)
        {
            this.alphabet=alphabet;
        }

        /// <summary>
        /// Шифрует текст шифром Виженера
        /// </summary>
        /// <param name="PlainText">Открытый текст</param>
        /// <param name="Keyword">Ключевое слово</param>
        /// <returns>Зашифрованный текст</returns>
        public string Encrypt(string PlainText, string Keyword)
        {
            string Key = LongKey(PlainText.Length, Keyword);                      
            StringBuilder CipherTextBuilder = new StringBuilder(PlainText.Length);
            for (int i = 0; i < PlainText.Length; i++)
                CipherTextBuilder.Append(alphabet[(alphabet.IndexOf(PlainText[i]) + alphabet.IndexOf(Key[i])) 
                    % alphabet.Length]);
            return CipherTextBuilder.ToString();
        }

        /// <summary>
        /// Дешифрация шифра Виженера
        /// </summary>
        /// <param name="CipherText">Зашифрованный текст</param>
        /// <param name="Keyword">Ключевое слово</param>
        /// <returns>Открытый текст</returns>
        public string Decrypt(string CipherText, string Keyword)
        {
            string Key = LongKey(CipherText.Length, Keyword);
            StringBuilder PlainTextBuilder = new StringBuilder(CipherText.Length);
            for (int i = 0; i < CipherText.Length; i++)
                PlainTextBuilder.Append(alphabet[(alphabet.IndexOf(CipherText[i]) - alphabet.IndexOf(Key[i]) + alphabet.Length) 
                    % alphabet.Length]);
            return PlainTextBuilder.ToString();
        }

        /// <summary>
        /// Формирует на основе ключевого слова ключ с длиной
        /// равной длине шифруемого текста
        /// </summary>
        /// <param name="KeyLength">Длина ключа</param>
        /// <param name="Keyword">Ключевое слово</param>
        /// <returns>Ключ шифра</returns>
        private string LongKey(int KeyLength, string Keyword)
        {
            StringBuilder KeywordExt = new StringBuilder(KeyLength);
            for (int i = 0; i < KeyLength; i++)
                KeywordExt.Append(Keyword[i % Keyword.Length]);
            return KeywordExt.ToString();
        }
    }
}
