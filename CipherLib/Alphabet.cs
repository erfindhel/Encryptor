using System;

namespace CipherLib
{
    /// <summary>
    /// Класс, предоставляющий методы 
    /// для получения различных алфавитов
    /// </summary>
    public abstract class Alphabet
    {
        static string alphabet;

        /// <summary>
        /// Заглавные буквы английского алфавита
        /// </summary>
        public static string EngU
        {
            get
            {
                alphabet = "";
                for (char ch = 'A'; ch <= 'Z'; ch++)
                {
                    alphabet += ch.ToString();
                }
                return alphabet;
            }
        }

        /// <summary>
        /// Строчные буквы английского алфавита
        /// </summary>
        public static string EngL
        {
            get
            {
                alphabet = "";
                for (char ch = 'a'; ch <= 'z'; ch++)
                {
                    alphabet += ch.ToString();
                }
                return alphabet;
            }
        }

        /// <summary>
        /// Заглавные буквы русского алфавита
        /// </summary>
        public static string RusU
        {
            get
            {
                for (char ch = 'А'; ch <= 'Я'; ch++)
                {
                    alphabet += ch.ToString();
                }
                return alphabet;
            }
        }

        /// <summary>
        /// Строчные буквы русского алфавита
        /// </summary>
        public static string RusL
        {
            get
            {
                for (char ch = 'а'; ch <= 'я'; ch++)
                {
                    alphabet += ch.ToString();
                }
                return alphabet;
            }
        }

        /// <summary>
        /// Числа
        /// </summary>
        public static string Numbers
        {
            get
            {
                for (char ch = '0'; ch <= '9'; ch++)
                {
                    alphabet += ch.ToString();
                }
                return alphabet;
            }
        }

        /// <summary>
        /// Скобки и знаки препинания
        /// </summary>
        public static string SpecialCharacters
        {
            get
            {
                for (char ch = ' '; ch <= '/'; ch++)
                {
                    alphabet += ch.ToString();
                }
                for (char ch = ':'; ch <= '?'; ch++)
                {
                    alphabet += ch.ToString();
                }
                return alphabet;
            }
        }

        /// <summary>
        /// Все доступные символы
        /// </summary>
        public static string All
        {
            get
            {
                alphabet = RusL + RusU + EngL + EngU + Numbers + SpecialCharacters;
                return alphabet;
            }
        }
    }
}
