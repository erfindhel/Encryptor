namespace CipherLib
{
    abstract public class ColumnarAbstract
    {
        public string PlainText, CipherText;
        public abstract string Encrypt(string PlainText, string Key, out char[,] CodeTable);
        public abstract string Decrypt(string CipherText, string Key, out char[,] CodeTable);
    }
}
