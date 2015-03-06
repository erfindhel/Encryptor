using System.Text;
using System.IO;

namespace CipherLib
{
    /// <summary>
    /// Класс, предоставляющий методы шифрации и дешифрации 
    /// текста методом монофонической замены
    /// </summary>
    public class Homophonic : Encryption
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса Homophonic, используя заданный алфавит
        /// </summary>
        /// <param name="alphabet">Используемый алфавит</param>
        /// <param name="FilePath">Путь к кодовой таблице</param>
        public Homophonic(string alphabet, string FilePath) : base(alphabet, FilePath) { }

        /// <summary>
        /// Шифрует текст методом монофонической замены
        /// </summary>
        /// <param name="PlainText">Открытый текст</param>
        /// <returns>Шифротекст в виде числовой последовательности</returns>
        public override string Encrypt(string PlainText)
        {
            string[][] CodeTable;
            StringBuilder Cipher = new StringBuilder(PlainText.Length * 3);
            CodetableInit(PlainText, out CodeTable);
            //теперь CodeTable - ступенчатый массив на 26 строк по шаблону текстового файла
            int[] CurrentLetterCount = new int[alphabet.Length]; // номер вхождения текущего символа (для навигации по столбцам)
            int CurrentLettenIndex; // номер текущего символа в алфавите
            for (int i = 0; i < PlainText.Length; i++)
            {
                CurrentLettenIndex = alphabet.IndexOf(PlainText[i]);
                Cipher.Append(CodeTable[CurrentLettenIndex][CurrentLetterCount[CurrentLettenIndex]++]);
            }
            return Cipher.ToString();
        }

        /// <summary>
        /// Дешифрует текст, зашифрованный методом
        /// монофонической замены
        /// </summary>
        /// <param name="CipherText">Шифротекст</param>
        /// <returns>Открытый текст</returns>
        public override string Decrypt(string CipherText)
        {
            string[][] CodeTable = new string[alphabet.Length][];
            StringBuilder PlainText = new StringBuilder(CipherText.Length / 3);
            FillFromFile(ref CodeTable);
            string SubString = "";
            for (int k = 0; k < CipherText.Length - 2; k += 3)
            {
                SubString = CipherText.Substring(k, 3);
                for (int i = 0; i < CodeTable.GetLength(0); i++)
                {
                    for (int j = 0; j < CodeTable[i].GetLength(0); j++)
                    {
                        if (CodeTable[i][j] == SubString)
                        {
                            PlainText.Append(alphabet[i]);
                        }
                    }
                }       
            }
            return PlainText.ToString();
        }

        //
        /// <summary>
        /// Выполняет частотный анализ принимаемого текста и
        /// выделяет память под ступенчатый массив
        /// </summary>
        /// <param name="PlainText">Открытый текст</param>
        /// <param name="CodeTable">Таблица замен</param>
        private void CodetableInit(string PlainText, out string[][] CodeTable)
        {
            // jagged array
            CodeTable = new string[alphabet.Length][];
            // frequency analysys
            int CharFrequency;
            for (int i = 0; i < alphabet.Length; i++)
            {
                CharFrequency = 0;
                for (int j = 0; j < PlainText.Length; j++)
                {
                    if (PlainText[j] == alphabet[i])
                    {
                        CharFrequency++;
                    }
                }
                int ColCount = (0 != CharFrequency) ? CharFrequency : 1;
                CodeTable[i] = new string[ColCount];
            }
            FillFromFile(ref CodeTable);                
        }

        /// <summary>
        /// Заполняет принимаемый ступенчатый массив значениями
        /// из текстового файла
        /// </summary>
        /// <param name="CodeTable">Таблица замены</param>
        private void FillFromFile(ref string[][] CodeTable)
        {
            StreamReader reader = new StreamReader(this.FilePath, Encoding.Default);
            char[] separator = new char[] { '\r', '\n' };
            string[] lines = reader.ReadToEnd().Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
            string[] line; //последовательность без пробелов
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split(' ');
                if (null == CodeTable[i]) CodeTable[i] = new string[line.Length]; // только для дешифрации
                for (int j = 0; j < line.Length; j++)
                {
                    CodeTable[i][j] = line[j];
                }
            }
        }
    }
}
