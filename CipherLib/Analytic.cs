using System;
using System.Text;
using System.IO;
using MatrixOperations;

namespace CipherLib
{
    public class Analytic : Encryption
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса Analytic, используя заданный алфавит
        /// </summary>
        /// <param name="alphabet">Используемый алфавит</param>
        /// <param name="FilePath">Путь к кодовой таблице</param>
        public Analytic(string alphabet, string FilePath) : base(alphabet, FilePath) { }

        /// <summary>
        /// Шифрует текст методом матричных преобразований
        /// </summary>
        /// <param name="PlainText">Открытый текст</param>
        /// <returns>Шифротекст в виде числовой последовательности разделённой пробелами</returns>
        public override string Encrypt(string PlainText)
        {
            int[,] CodeTable;
            int[][] TextArray;
            FillCodeTableFromFile(out CodeTable); // initialize and fill Codetable
            int CodeTableDim = CodeTable.GetLength(0);
            TextArray = PlainTextArray(PlainText, CodeTableDim); // initialize TextArray            
            // multiplicate Codetable with text
            for (int i = 0; i < TextArray.GetLength(0); i++)
                TextArray[i] = Matrix.MultMatr(CodeTable, TextArray[i]);

            // convert 2D array to string with spaces as separator
            StringBuilder sb = new StringBuilder(PlainText.Length);
            foreach (int[] SymArr in TextArray)
                foreach(int s in SymArr)
                    sb.Append(s.ToString() + " ");

            return sb.ToString().TrimEnd(' ');
        }

        /// <summary>
        /// Заносит открытый текст в ступенчатый массив
        /// </summary>
        /// <param name="PlainText">Открытый текст</param>
        /// <param name="CodeTableDim">Размерность кодовой таблицы</param>
        /// <returns>Ступенчатый массив, содержащий открытый текст,
        /// разбитый на строки</returns>
        private int[][] PlainTextArray(string PlainText, int CodeTableDim)
        {
            int LongRowCount = PlainText.Length / CodeTableDim;
            int[][] TextArray = AllocateMem(CodeTableDim, LongRowCount);
            // Fill TextArray with order numbers of appropriate letters
            int i, j, k = 0;
            int CountOfArrays = TextArray.GetLength(0);
            for (i = 0; i < CountOfArrays; i++)
            {
                for (j = 0; j < TextArray[i].GetLength(0); j++, k++)
                {
                    TextArray[i][j] = alphabet.IndexOf(PlainText[k]) + 1;
                }
            }
            return TextArray;
        }

        /// <summary>
        /// Заполняет кодовую таблицу числами
        /// </summary>
        /// <param name="CodeTable">Кодовая таблица</param>
        void FillCodeTableFromFile(out int[,] CodeTable)
        {
            // For square matrix only
            StreamReader reader = new StreamReader(this.FilePath, Encoding.Default);
            char[] separator = new char[] { '\r', '\n' };
            string[] lines = reader.ReadToEnd().Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
            string[] line; //последовательность без пробелов
            int RowCount = lines.Length;
            CodeTable = new int[RowCount, RowCount];
            for (int i = 0; i < RowCount; i++)
            {
                line = lines[i].Split(' ');
                for (int j = 0; j < RowCount; j++)
                {
                    CodeTable[i,j] = Int32.Parse(line[j]);
                }
            }
        }

        /// <summary>
        /// Выделяет память под ступенчатый массив
        /// </summary>
        /// <param name="CodeTableDim">Размерность кодовой таблицы</param>
        /// <param name="LongRowCount">Количество "полных" строк массива</param>
        /// <returns>Ступенчатый массив</returns>
        private static int[][] AllocateMem(int CodeTableDim, int LongRowCount)
        {
            int[][] TextArray;
            TextArray = new int[LongRowCount][];
            for (int i = 0; i < LongRowCount; i++)
            {
                TextArray[i] = new int[CodeTableDim];
            }
            return TextArray;
        }

        /// <summary>
        /// Дешифрует текст, зашифрованный методом матричных преобразований
        /// </summary>
        /// <param name="CipherText">Шифротекст</param>
        /// <returns>Открытый текст</returns>
        public override string Decrypt(string CipherText)
        {
            int[,] CodeTable;
            int[][] TextArray;
            // initialize and fill Codetable
            FillCodeTableFromFile(out CodeTable);
            int CodeTableDim = CodeTable.GetLength(0);
            // initialize TextArray
            TextArray = CipherTextArray(CipherText, CodeTableDim);

            //double[,] CodeTableD=new double[CodeTableDim,CodeTableDim];
            //double[,] CodeTableInv=new double[CodeTableDim,CodeTableDim];
            //for (int i = 0; i < CodeTableDim; i++)
            //{
            //    for (int j = 0; j < CodeTableDim; j++)
            //    {
            //        CodeTableD[i, j] = Convert.ToDouble(CodeTable[i, j]);
            //    }
            //}
            //bool inverse = Matrix.Invert(CodeTableD, CodeTableInv);

            return "Doesn't work yet";
        }

        /// <summary>
        /// Заносит шифротекст в ступенчатый массив
        /// </summary>
        /// <param name="PlainText">Шифротекст текст</param>
        /// <param name="CodeTableDim">Размерность кодовой таблицы</param>
        /// <returns>Ступенчатый массив, содержащий шифротекст,
        /// разбитый на строки</returns>
        private int[][] CipherTextArray(string CipherText, int CodeTableDim)
        {
            string[] line = CipherText.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            int LongRowCount = line.Length / CodeTableDim;
            int[][] TextArray = AllocateMem(CodeTableDim, LongRowCount);

            int i, j, k = 0;
            int CountOfArrays = TextArray.GetLength(0);
            for (i = 0; i < CountOfArrays; i++)
            {
                for (j = 0; j < TextArray[i].GetLength(0); j++, k++)
                {
                    TextArray[i][j] =Int32.Parse(line[k]);
                }
            }
            return TextArray;
        }
    }
}
