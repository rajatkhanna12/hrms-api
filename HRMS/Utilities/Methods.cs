using HRMS.Models.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace HRMS.Utilities
{
    public class Methods
    {
       
        public class AESEncryption
        {
            public static string EncryptString(string str)
            {
                byte[] encryptBytes = null;
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(str);
                encryptBytes = AESEncrypt(bytesToBeEncrypted);

                return Convert.ToBase64String(encryptBytes);

            }

            public static string DecryptString(string str)
            {
                byte[] decryptedBytes = null;
                byte[] bytesToBeDecrypted = Convert.FromBase64String(str);
                decryptedBytes = AESDecrypt(bytesToBeDecrypted);

                return Encoding.UTF8.GetString(decryptedBytes);
            }

            public static string EncryptWithRandomSalt(string str)
            {
                byte[] baText = Encoding.UTF8.GetBytes(str);
                byte[] baSalt = GetRandomBytes();
                byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

                //Combine Salt + Text
                for (int i = 0; i < baSalt.Length; i++)
                {
                    baEncrypted[i] = baSalt[i];
                }
                for (int i = 0; i < baText.Length; i++)
                {
                    baEncrypted[i + baSalt.Length] = baText[i];
                }
                baEncrypted = AESEncrypt(baEncrypted);
                return Convert.ToBase64String(baEncrypted);
            }

            public static string DecryptWithRandomSalt(string str)
            {
                byte[] baText = Convert.FromBase64String(str);
                byte[] baDecrypted = AESDecrypt(baText);

                //Remove Salt
                int saltLenght = GetSaltLength();
                byte[] baResult = new byte[baDecrypted.Length - saltLenght];
                for (int i = 0; i < baResult.Length; i++)
                {
                    baResult[i] = baDecrypted[i + saltLenght];
                }
                return Encoding.UTF8.GetString(baResult);
            }

            private static byte[] AESEncrypt(byte[] bytesToBeEncrypted)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes("MyPasswordAsSalt");
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new())
                {
                    using (RijndaelManaged AES = new())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }
                        bytesToBeEncrypted = ms.ToArray();
                    }
                }
                return bytesToBeEncrypted;
            }

            private static byte[] AESDecrypt(byte[] bytesToBeDecrypted)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes("MyPasswordAsSalt");
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                using (MemoryStream ms = new())
                {
                    using (RijndaelManaged AES = new())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        bytesToBeDecrypted = ms.ToArray();
                    }
                }

                return bytesToBeDecrypted;
            }

            private static byte[] GetRandomBytes()
            {
                int saltLenght = GetSaltLength();
                byte[] ba = new byte[saltLenght];
                RandomNumberGenerator.Create().GetBytes(ba);
                return ba;
            }

            private static int GetSaltLength()
            {
                return 8;
            }
        }
    }

    public class MethodsGen<T>
    {
        public static JsonResultModel<T> GetObject(bool isError, T data, string message = "")
        {
            return new JsonResultModel<T>()
            {
                IsError = isError,
                Data = data,
                Message = message
            };
        }
    }
}
