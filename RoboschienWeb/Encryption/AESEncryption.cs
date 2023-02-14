using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RoboschienWeb.Encryption
{
    public class AESEncryption
    {

        static string ivArrStr = "RobertBoschBanga"; //{82,111,98,101,114,116,66,111,115,99,104,66,97,110,103,97}
        static byte[] ivArr = Encoding.ASCII.GetBytes(ivArrStr);
         byte[] key;
        public AESEncryption()
        {

        }
        public  byte[] GenerateKey()
        {
            //var random = new RNGCryptoServiceProvider();
            //var b = new byte[32];
            //  random.GetBytes(b);

            string randomString = RandomString(32);
            key = Encoding.ASCII.GetBytes(randomString);

            return key;
        }

        public  string Encrypt(byte[] key,string PlainText)
        {

            RijndaelManaged aes = new RijndaelManaged();
            aes.BlockSize = 128;
            aes.KeySize = 256;

                
            aes.Mode = CipherMode.ECB;
           
            // Initialization vector.   
            // It could be any value or generated using a random number generator.
           // byte[] ivArr = { 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1, 7, 7, 7, 7 };
            //byte[] ivArr = { 1, 2, 3, 4, 5, 8, 9, 5, 4, 3, 2, 1, 7, 5, 5, 7 };

            byte[] IVBytes16Value = new byte[16];
            Array.Copy(ivArr, IVBytes16Value, 16);

            aes.Key = key;
            aes.IV = IVBytes16Value;

            ICryptoTransform encrypto = aes.CreateEncryptor();

            byte[] plainTextByte = ASCIIEncoding.UTF8.GetBytes(PlainText);
            byte[] CipherText = encrypto.TransformFinalBlock(plainTextByte, 0, plainTextByte.Length);
            return Convert.ToBase64String(CipherText);

        }

        public  string Decrypt(byte[] key,string CipherText)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.BlockSize = 128;
            aes.KeySize = 256;

            aes.Mode = CipherMode.ECB;
           
            // Initialization vector.   
            // It could be any value or generated using a random number generator.
            //byte[] ivArr = { 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1, 7, 7, 7, 7 };
            byte[] IVBytes16Value = new byte[16];
            Array.Copy(ivArr, IVBytes16Value, 16);

            aes.Key = key;
            aes.IV = IVBytes16Value;

            ICryptoTransform decrypto = aes.CreateDecryptor();

            
            byte[] encryptedBytes = Convert.FromBase64CharArray(CipherText.ToCharArray(), 0, CipherText.Length);
            byte[] decryptedData = decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return ASCIIEncoding.UTF8.GetString(decryptedData);
        }

        public DataTable Encrypt(DataTable table, byte[] key)
        {

            try
            {
                //Rows
                for (int k = 0; k < table.Rows.Count; k++)
                {

                    //Columns
                    for (int j = 0; j < table.Rows[k].ItemArray.Length; j++)
                    {
                        table.Rows[k].SetField(j, Encrypt(key, table.Rows[k].ItemArray[j].ToString()));
                    }

                }

                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable Decrypt(DataTable table, byte[] key)
        {
            try
            {
                //Rows
                for (int k = 0; k < table.Rows.Count; k++)
                {

                    //Columns
                    for (int j = 0; j < table.Rows[k].ItemArray.Length; j++)
                    {
                        table.Rows[k].SetField(j, Decrypt(key, table.Rows[k].ItemArray[j].ToString()));
                    }

                }

                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public byte[] ConvertStringToByteArray(string key)
        {
            byte[] keyArr = Convert.FromBase64String(key);
            byte[] KeyArrBytes32Value = new byte[32];
            Array.Copy(keyArr, KeyArrBytes32Value, 32);
            return KeyArrBytes32Value;
        }
        public string ConvertByteArrayToString(byte[] key)
        {
            return Convert.ToBase64String(key);
        }

        private string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_+ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var builder = new StringBuilder();
            Random random = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }

        public byte[] DecryptBytes(byte[] encryptedBytes, byte[] key)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.ECB;
            RijndaelCipher.BlockSize = 128;
            RijndaelCipher.KeySize = 256;
            // byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            // PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            try
            {
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(key, ivArr);

                MemoryStream memoryStream = new MemoryStream(encryptedBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
                byte[] plainBytes = new byte[encryptedBytes.Length];

                int DecryptedCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                return plainBytes;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Decryption Exception  : " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception : " + ex.InnerException);

                }
                return null;
            }
        }

        public byte[] EncryptBytes(byte[] inputBytes,byte[] key) 
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.ECB;
            RijndaelCipher.BlockSize = 128;
            RijndaelCipher.KeySize = 256;

            // byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            //  PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);
            try
            {
                ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(key, ivArr);

                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] CipherBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                return CipherBytes;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Encryption Exception  : " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception : " + ex.InnerException);

                }
                return null;
            }
        }

    }
}
