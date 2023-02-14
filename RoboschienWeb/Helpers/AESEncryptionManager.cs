using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RoboschienWeb.Models
{
    public class AESEncryptionManager
    {
        private AesCryptoServiceProvider _provider;

        private byte[] key { get; set; }
        private byte[] iv { get; set; }

        public AESEncryptionManager(string AESKey)
        {
            _provider = new AesCryptoServiceProvider();
            _provider.BlockSize = 128;
            _provider.KeySize = 256;
            _provider.Mode = CipherMode.CBC;
            _provider.Padding = PaddingMode.PKCS7;
            string encodedKey = AESKey;

            iv = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(encodedKey)).Split(',')[0]);
            key = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(encodedKey)).Split(',')[1]);
        }

        //public string EncryptData(string data)
        //{
        //    byte[] plainText = ASCIIEncoding.UTF8.GetBytes(data);
        //    ICryptoTransform crypto = _provider.CreateEncryptor();
        //    byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
        //    string encryptText = Convert.ToBase64String(cipherText);
        //}


        public string DecryptData(string encryptText)
        {
            ICryptoTransform decrypto = _provider.CreateDecryptor(key,iv);
            byte[] encryptedBytes = Convert.FromBase64String(encryptText);
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(encryptedBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypto, CryptoStreamMode.Read);
            byte[] output = new byte[encryptedBytes.Length];
            int readBytes = cryptoStream.Read(output, 0, output.Length);
            string decryptText = Encoding.UTF8.GetString(output, 0, readBytes);
            return decryptText;

        }
        public Stream DecryptData(Stream stream)
        {
            ICryptoTransform decrypto = _provider.CreateDecryptor(key, iv);
            byte[] salt = GenerateRandomSalt();


            stream.Read(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(stream, decrypto, CryptoStreamMode.Read);
            return cs;
        }

        

        public void EncriptFile(string filePath)
        //    return encryptText;
        {
            byte[] salt = GenerateRandomSalt();

            using (FileStream fs = new FileStream(filePath + ".enc", FileMode.Create))
            {
                fs.Write(salt, 0, salt.Length);
                using (CryptoStream cs = new CryptoStream(fs, _provider.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    using (FileStream fsIn = new FileStream(filePath, FileMode.Open))
                    {

                        byte[] buffer = new byte[1];
                        int read;
                        try
                        {

                            while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                            {

                                cs.Write(buffer, 0, read);

                            }
                            cs.Close();
                            fs.Close();
                            fsIn.Close();
                        }
                        catch 
                        {
                        }

                    }
                }
            }
        }

        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }
    }
}