using System;

namespace CipherLib
{
    /// <summary>
    /// Шифрует текст с использованием шифра Цезаря
    /// </summary>
    public class Caesar
    {
        string alphabet; // Алфавит, используемый для шифрования

        /// <summary>
        /// Инициализирует новый экземпляр класса Caesar, используя заданный алфавит
        /// </summary>
        /// <param name="alphabet">Используемый алфавит</param>
        public Caesar(string alphabet)
        {
            this.alphabet = alphabet;
        }

        /// <summary>
        /// Шифрует текст шифром Цезаря
        /// </summary>
        /// <param name="PlainText">Открытый текст</param>
        /// <param name="position">Величина сдвига</param>
        /// <returns>Зашифрованный текст</returns>
        public string Encrypt(string PlainText, int position)
        {
            string CipherText = "";
            for (int i = 0; i < PlainText.Length; i++)
            {
                CipherText += alphabet[(alphabet.IndexOf(PlainText[i]) + position) % alphabet.Length].ToString();
            }
            return CipherText;
        }

        /// <summary>
        /// Дешифрует текст шифром Цезаря
        /// </summary>
        /// <param name="CipherText">Зашифрованный текст</param>
        /// <param name="position">Величина сдвига</param>
        /// <returns>Открытый текст</returns>
        public string Decrypt(string CipherText, int position)
        {
            string PlainText = "";
            for (int i = 0; i < CipherText.Length; i++)
            {
                if (alphabet.IndexOf(CipherText[i]) - position < 0)
                {
                    PlainText += alphabet[alphabet.Length - 1 + (alphabet.IndexOf(CipherText[i]) - position + 1) % alphabet.Length];
                }
                else
                {
                    PlainText += alphabet[(alphabet.IndexOf(CipherText[i]) - position) % alphabet.Length];
                }
            }
            return PlainText;
        }
    }
}
