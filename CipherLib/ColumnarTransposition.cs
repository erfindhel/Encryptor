using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherLib
{
    public class ColumnarTransposition : ColumnarAbstract
    {        
        int ColCount, RowCount;
        public ColumnarTransposition():base(){ }
        
        public override string Encrypt(string PlainText, string Key, out char[,] CodeTable)
        {
            // заполнение матрицы
            int k=0;
            CodeTable = CodeTableAssign(PlainText, Key.Length);
            for (int i = 0; i < CodeTable.GetLength(0); i++)
            {
                for (int j = 0; j < CodeTable.GetLength(1); j++)
                {
                    if (k < PlainText.Length)
                    {
                        CodeTable[i, j] = PlainText[k];
                        k++;
                    }
                    else goto label1; // текст закончился
                }
            }
            label1: //шифруем
            for (int keypos = 1; keypos <= Key.Length; keypos++)
            { // текущий знак ключа: 1,2,..,Length
                for (int j = 0; j < Key.Length; j++)
                { // ищем позицию текущего знака в действительном ключе
                    if (Key[j].ToString() == keypos.ToString())
                    {
                        for (int i = 0; i < CodeTable.GetLength(0) && '\0' != CodeTable[i, j]; i++)
                        { // меняем строки для найденного столбца
                            CipherText += CodeTable[i, j].ToString();
                        }
                        break;
                    }
                }
            }
            return CipherText;
        }

        public override string Decrypt(string CipherText, string Key, out char[,] CodeTable)
        {
            int k=0;
            int LongColsCount = CipherText.Length % Key.Length;
            CodeTable = CodeTableAssign(CipherText, Key.Length);
            for (int keynum = 1; keynum <= Key.Length; keynum++)
            { // текущий знак ключа: 1,2,..,Length
                for (int j = 0; j < Key.Length; j++)
                { // ищем позицию текущего знака в действительном ключе 
                    if (Key[j].ToString() == keynum.ToString()) // 
                    {
                        if (j < LongColsCount || 0 == LongColsCount) // если столбец "длинный"
                        {
                            for (int i = 0; i < CodeTable.GetLength(0); i++)
                            {
                                CodeTable[i,j] = CipherText[k];
                                k++;
                            }
                        }
                        else // если "короткий" - не доходим до конца
                        {
                            for (int i = 0; i < CodeTable.GetLength(0) - 1; i++)
                            {
                                CodeTable[i,j] = CipherText[k];
                                k++;
                            }
                        }
                    }
                }
            }
            foreach (char c in CodeTable) PlainText += '\0' != c ? c.ToString() : "";
            return PlainText;
        }
        
        private char[,] CodeTableAssign(string PlainText, int KeyLength)
        {
            char[,] CodeTable;
            ColCount = KeyLength;
            RowCount = (0 != PlainText.Length % ColCount) ? PlainText.Length / ColCount + 1 : PlainText.Length / ColCount; // округляем к большему целому
            CodeTable = new char[RowCount, ColCount]; //массив необходимой размерности
            return CodeTable;
        }
    }
}
    
   
