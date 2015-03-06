namespace CipherLib
{
    /// <summary>
    /// Абстрактный класс, предоставляющий интерфейс методов шифрования
    /// </summary>
    public abstract class Encryption
    {
        /// <summary>
        /// Используемый алфавит
        /// </summary>
        protected string alphabet = "";

        /// <summary>
        /// Путь к кодовой таблице
        /// </summary>
        protected string FilePath = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alphabet">Используемый алфавит
        /// в виде строковой последовательности (абв...)
        /// </param>
        /// <param name="FilePath">Путь к кодовой таблице</param>
        public Encryption(string alphabet, string FilePath)
        {
            this.alphabet = alphabet;
            this.FilePath = FilePath;
        }

        /// <summary>
        /// Шифрует текст методом, определённым в производном классе
        /// </summary>
        /// <param name="PlainText">Открытый текст</param>
        /// <returns>Шифротекст в виде числовой последовательности</returns>
        public abstract string Encrypt(string PlainText);

        /// <summary>
        /// Дешифрует текст, зашифрованный методом, 
        /// определённым в производном классе
        /// </summary>
        /// <param name="CipherText">Шифротекст</param>
        /// <returns>Открытый текст</returns>
        public abstract string Decrypt(string CipherText);
    }
}
