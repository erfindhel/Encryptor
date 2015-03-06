using System.IO;
using System.Security.Cryptography;

namespace CipherLib
{
    public class FeistelCipher
    {
        /// <summary>
        /// Шифрует файл алгоритмом TripleDES
        /// </summary>
        /// <param name="NonCryptedFilePath">Полный путь к файлу, подлежащему шифрованию</param>
        /// <param name="KeyFilePath">Полное имя файла, в котором будет сохранён ключ</param>
        /// <param name="CryptedFilePath">Полное имя зашифрованного файла</param>
        static public void Encrypt(string NonCryptedFilePath, string KeyFilePath, string CryptedFilePath)
        {
            FileStream fsInputFile = File.OpenRead(NonCryptedFilePath);
            FileStream fsOutputFile = File.Create(CryptedFilePath);
            // The chryptographic service provider we're going to use
            TripleDESCryptoServiceProvider TDES = new TripleDESCryptoServiceProvider();
            // This object links data streams to cryptographic values
            CryptoStream csEncrypt = new CryptoStream(fsOutputFile, TDES.CreateEncryptor(), CryptoStreamMode.Write);
            // This stream writer will write the new file
            BinaryReader brNonCrypted = new BinaryReader(fsInputFile);
            BinaryWriter bwEncrypted = new BinaryWriter(csEncrypt);
            while (true)
            {
                try
                {
                    bwEncrypted.Write(brNonCrypted.ReadByte());
                }
                catch (EndOfStreamException) { break; }
            }
            bwEncrypted.Close();
            // Create the key file
            FileStream fsFileKey = File.Create(KeyFilePath);
            BinaryWriter bwKeyFile = new BinaryWriter(fsFileKey);
            bwKeyFile.Write(TDES.Key);
            bwKeyFile.Write(TDES.IV);
            bwKeyFile.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CryptedFilePath">Полный путь к зашифрованному файлу</param>
        /// <param name="KeyFilePath">Полный путь к ключу</param>
        /// <param name="DecryptedFilePath">Полное имя дешифрованного файла</param>
        static public void Decrypt(string CryptedFilePath, string KeyFilePath, string DecryptedFilePath)
        {
            FileStream fsInputFile = File.OpenRead(CryptedFilePath);
            FileStream fsKeyFile = File.OpenRead(KeyFilePath);
            FileStream fsOutputFile = File.Create(DecryptedFilePath);
            // Prepare the encryption algorithm and read the key from the key file
            TripleDESCryptoServiceProvider TDES = new TripleDESCryptoServiceProvider();
            BinaryReader brKeyFile = new BinaryReader(fsKeyFile);
            TDES.Key = brKeyFile.ReadBytes(24);
            TDES.IV = brKeyFile.ReadBytes(8);
            // The cryptographic stream takes in the unecrypted file
            CryptoStream csDecrypt = new CryptoStream(fsInputFile, TDES.CreateDecryptor(), CryptoStreamMode.Read);
            // Write the new unecrypted file
            BinaryReader srCryptedStream = new BinaryReader(csDecrypt);
            BinaryWriter swDecryptedStream = new BinaryWriter(fsOutputFile);
            while (true)
            {
                try
                {
                    swDecryptedStream.Write(srCryptedStream.ReadByte());
                }
                catch (EndOfStreamException) { break; }
            }
            swDecryptedStream.Close();
            fsOutputFile.Close();
            srCryptedStream.Close();
        }
    }
}
