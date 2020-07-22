using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LinuxSecurityBot
{
	public static class AES
	{
        private static string GetPassword
        {
            get
            {
                // This method of encrypting data is not secure, however it adds a needed layer between plain text.
                // This is only so someone does not accidently screenshot or send out the config file with the bot's discord token.
                if (!File.Exists("passwd"))
                    GenerateSecurePassword();

                return File.ReadAllText("passwd");
            }
        }

        private static void GenerateSecurePassword() => File.WriteAllText("passwd", GenerateSecureString(15));
        public static string GenerateSecureString(int length)
        {
            RNGCryptoServiceProvider secureRandom = new RNGCryptoServiceProvider();
            byte[] secureBytes = new byte[length];
            secureRandom.GetNonZeroBytes(secureBytes);
            return Convert.ToBase64String(secureBytes);
        }

        public static string EncryptString(string plainData)
		{
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256
            };
            aesProvider.GenerateIV();

            // We are not saving this hash so we do not need a salt.
            aesProvider.Key = new PasswordDeriveBytes(GetPassword, new byte[] { }).GetBytes(aesProvider.KeySize / 8);

            ICryptoTransform encrypter = aesProvider.CreateEncryptor();

            byte[] data = Encoding.UTF8.GetBytes(plainData);
            using (MemoryStream output = new MemoryStream())
            using (CryptoStream writer = new CryptoStream(output, encrypter, CryptoStreamMode.Write))
            {
                writer.Write(data, 0, data.Length);
                writer.FlushFinalBlock();
                byte[] encrypted = new byte[output.Length + 16];
                Buffer.BlockCopy(aesProvider.IV, 0, encrypted, 0, aesProvider.IV.Length);
                Buffer.BlockCopy(output.ToArray(), 0, encrypted, aesProvider.IV.Length, output.ToArray().Length);
                return Convert.ToBase64String(data);
            }
        }

		public static string DecryptString(string cipherText)
		{
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256
            };

            byte[] data = Convert.FromBase64String(cipherText);
            aesProvider.IV = data.Take(16).ToArray();

            // We are not saving this hash so we do not need a salt.
            aesProvider.Key = new PasswordDeriveBytes(GetPassword, new byte[] { }).GetBytes(aesProvider.KeySize / 8);

            byte[] decrypted = new byte[data.Length - aesProvider.BlockSize / 8];
            ICryptoTransform decrypter = aesProvider.CreateDecryptor();

            using (MemoryStream input = new MemoryStream(data.Skip(16).ToArray()))
            using (CryptoStream reader = new CryptoStream(input, decrypter, CryptoStreamMode.Read))
            {
                int decryptedLength = reader.Read(decrypted, 0, decrypted.Length);
                return Encoding.UTF8.GetString(decrypted.Take(decryptedLength).ToArray());
            }
        }
	}
}
