using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace LightFramework.Util
{
    /// <summary>
    /// Cryptography Helper class that contains static methods for data encryption and decryption.
    /// </summary>
    public class CryptographyHelper
    {
        private CryptographyHelper()
        {
        }

        /// <summary>
        /// 获取加密后的字符串。
        /// </summary>
        /// <param name="al">加密算法名称,取值为:DES,TripleDES.如果为空则不加密</param>
        /// <param name="inputString">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetEncryptString(EncryptAlgorithm al, string inputString)
        {
            return GetEncryptString(al, inputString, string.Empty);
        }

        /// <summary>
        /// 获取加密后的字符串。
        /// </summary>
        /// <param name="al">加密算法名称,取值为:DES,TripleDES.如果为空则不加密</param>
        /// <param name="inputString">需要加密的字符串</param>
        /// <param name="privacyKey">密钥,如果为空则采用系统默认密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string GetEncryptString(EncryptAlgorithm al, string inputString, string privacyKey)
        {
            if (al == EncryptAlgorithm.DES)
            {
                return DESEncrypt(inputString, privacyKey);
            }

            if(al == EncryptAlgorithm.TripleDES)
            {
                return TripleDESEncrypt(inputString,privacyKey);
            }

            return inputString;
        }

        /// <summary>
        /// 获取加密后的字符串。
        /// </summary>
        /// <param name="al">加密算法名称,取值为:DES,TripleDES.如果为空则不加密</param>
        /// <param name="inputString">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetDecryptString(EncryptAlgorithm al, string inputString)
        {
            return GetDecryptString(al, inputString, string.Empty);
        }

        /// <summary>
        /// 获取加密后的字符串。
        /// </summary>
        /// <param name="al">加密算法,取值为:DES,TripleDES.如果为空则不加密</param>
        /// <param name="inputString">需要加密的字符串</param>
        /// <param name="privacyKey">密钥,如果为空则采用系统默认密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string GetDecryptString(EncryptAlgorithm al, string inputString, string privacyKey)
        {
            if (al == EncryptAlgorithm.DES)
            {
                return DESDecrypt(inputString, privacyKey);
            }

            if (al == EncryptAlgorithm.TripleDES)
            {
                return TripleDESDecrypt(inputString, privacyKey);
            }

            return inputString;
        }

        #region MD5 加密

        public static string MD5(string input)
        {
            using (System.Security.Cryptography.MD5CryptoServiceProvider provider = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", string.Empty).ToLower(System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Calculates the MD5 of a given string,defalut Encoding is UTF8.
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <returns>The (hexadecimal) string representatation of the MD5 hash.</returns>
        public static string StringToMD5Hash(string inputString)
        {
            return StringToMD5Hash(inputString, Encoding.UTF8);
        }

        /// <summary>
        /// Calculates the MD5 of a given string.
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <param name="encoding">System.Text.Encoding</param>
        /// <returns>The (hexadecimal) string representatation of the MD5 hash.</returns>
        public static string StringToMD5Hash(string inputString,Encoding encoding)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] encryptedBytes = md5.ComputeHash(encoding.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calculates the MD5 of a given bytes.
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <param name="encoding">System.Text.Encoding</param>
        /// <returns>The (hexadecimal) string representatation of the MD5 hash bytes.</returns>
        public static byte[] StringToMD5HashBytes(string inputString, Encoding encoding)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            return md5.ComputeHash(encoding.GetBytes(inputString));
        }

        #endregion

        #region DES 加密与解密

        /// <summary>
        /// DES加密方法,并采用系统默认密钥加密。
        /// </summary>
        /// <param name="inputString">输入要加密的文本</param>
        /// <returns>DES加密后的字符串</returns>
        public static string DESEncrypt(string inputString)
        {
            return DESEncrypt(inputString, string.Empty);
        }

        /// <summary>
        /// DES加密方法
        /// </summary>
        /// <param name="inputString">输入要加密的文本</param>
        /// <param name="privacyKey">DES密钥,长度必须等于64bit(即8字节),如果为空则采用系统默认密钥</param>
        /// <returns>DES加密后的字符串</returns>
        public static string DESEncrypt(string inputString, string privacyKey)
        {
            //设置默认密钥
            if (string.IsNullOrEmpty(privacyKey))
            {
                privacyKey = "owfg1dwz";
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(privacyKey);
            des.IV = Convert.FromBase64String("rZzTz4kly80=");

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] inBytes = Encoding.UTF8.GetBytes(inputString);
            cs.Write(inBytes, 0, inBytes.Length);
            cs.FlushFinalBlock();

            byte[] buff = ms.ToArray();

            cs.Close();
            ms.Close();

            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// DES解密方法,并采用系统默认密钥解密。
        /// </summary>
        /// <param name="inputString">输入经过DES加密的文本</param>
        /// <returns>DES解密后的字符串</returns>
        public static string DESDecrypt(string inputString)
        {
            return DESDecrypt(inputString, string.Empty);
        }

        /// <summary>
        /// DES解密方法
        /// </summary>
        /// <param name="inputString">输入经过DES加密的文本</param>
        /// <param name="privacyKey">DES 密钥,长度必须等于64bit(即8字节),该值必须与对应的加密密钥一致,如果为空则采用系统默认密钥</param>
        /// <returns>DES解密后的字符串</returns>
        public static string DESDecrypt(string inputString, string privacyKey)
        {
            //设置默认密钥
            if (string.IsNullOrEmpty(privacyKey))
            {
                privacyKey = "owfg1dwz";
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(privacyKey);
            des.IV = Convert.FromBase64String("rZzTz4kly80=");

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            byte[] inBytes = Convert.FromBase64String(inputString);
            cs.Write(inBytes, 0, inBytes.Length);
            cs.FlushFinalBlock();

            byte[] buff = ms.ToArray();

            cs.Close();
            ms.Close();

            return Encoding.UTF8.GetString(buff);
        }

        #endregion

        #region TripleDES 加密与解密方法

        /// <summary>
        /// TripleDES加密方法,并采用系统默认密钥加密。
        /// </summary>
        /// <param name="inputString">输入要加密的文本</param>
        /// <returns>TripleDES加密后的字符串<</returns>
        public static string TripleDESEncrypt(string inputString)
        {
            return TripleDESEncrypt(inputString, string.Empty);
        }

        /// <summary>
        /// TripleDES 加密方法
        /// </summary>
        /// <param name="inputString">输入要加密的文本</param>
        /// <param name="privacyKey">TripleDES 密钥,如果为空则采用系统默认密钥</param>
        /// <returns>TripleDES加密后的字符串</returns>
        public static string TripleDESEncrypt(string inputString,string privacyKey)
        {
            //设置默认密钥
            if (string.IsNullOrEmpty(privacyKey))
            {
                privacyKey = "owfg1ZYQQJLGGxY00OXC4qCFfop1qdzw";
            }

            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = Convert.FromBase64String(privacyKey);
            tripleDES.IV = Convert.FromBase64String("rZzTz4kly80=");

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, tripleDES.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] inBytes = Encoding.UTF8.GetBytes(inputString);
            cs.Write(inBytes, 0, inBytes.Length);
            cs.FlushFinalBlock();

            byte[] buff = ms.ToArray();

            cs.Close();
            ms.Close();

            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// TripleDES解密方法,并采用系统默认密钥解密。
        /// </summary>
        /// <param name="inputString">输入经过TripleDES加密的文本</param>
        /// <returns>TripleDES解密后的字符串<</returns>
        public static string TripleDESDecrypt(string inputString)
        {
            return TripleDESDecrypt(inputString, string.Empty);
        }

        /// <summary>
        /// TripleDES 解密方法
        /// </summary>
        /// <param name="inputString">经过TripleDES算法加密的文本</param>
        /// <param name="privacyKey">TripleDES 密钥,该值必须与对应的加密密钥一致,如果为空则采用系统默认密钥</param>
        /// <returns>TripleDES 解密字符串</returns>
        public static string TripleDESDecrypt(string inputString, string privacyKey)
        {
            //设置默认密钥
            if (string.IsNullOrEmpty(privacyKey))
            {
                privacyKey = "owfg1ZYQQJLGGxY00OXC4qCFfop1qdzw";
            }

            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = Convert.FromBase64String(privacyKey);
            tripleDES.IV = Convert.FromBase64String("rZzTz4kly80=");

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, tripleDES.CreateDecryptor(), CryptoStreamMode.Write);

            byte[] inBytes = Convert.FromBase64String(inputString);
            cs.Write(inBytes, 0, inBytes.Length);
            cs.FlushFinalBlock();

            byte[] buff = ms.ToArray();

            cs.Close();
            ms.Close();

            return Encoding.UTF8.GetString(buff);
        }

        #endregion

    }

    /// <summary>
    /// 加密算法枚举.
    /// </summary>
    public enum EncryptAlgorithm
    {
        /// <summary>
        /// DES算法。
        /// </summary>
        DES,

        /// <summary>
        /// TripleDES算法。
        /// </summary>
        TripleDES,

        /// <summary>
        /// 不采用加密算法。
        /// </summary>
        NotEncrypt
    }
}
