using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Service
{
    public class TripleDESEncryptService
    {
        #region 3DES加密和解密 DES的第三版本
        #region 得到3DES密钥和矢量
        private static string KeyString = "y9YqkXpDiF9aggfbFnmgzJZ9aKZQu96c";
        private static string IVString = "DuElGyKT8fo=";
        /// <summary>
        /// 重写GenerateKey()方法，生成随机密钥
        /// </summary>
        /// <returns>返回所需要的密钥</returns>
        public static string GenerateKey()
        {
            //构造一个对称算法
            SymmetricAlgorithm mcsp = new TripleDESCryptoServiceProvider();
            byte[] key = mcsp.Key;
            return Convert.ToBase64String(key);
        }
        /// <summary>
        /// 重写GenerateIV()方法，生成随机矢量
        /// </summary>
        /// <returns>返回所需要的矢量</returns>
        public static string GenerateIV()
        {
            //构造一个对称算法
            SymmetricAlgorithm mcsp = new TripleDESCryptoServiceProvider();
            byte[] iv = mcsp.IV;
            return Convert.ToBase64String(iv);
        }
        #endregion

        /// <summary>
        /// 3DES一个加密的方法
        /// </summary>
        /// <param name="M_Text">要加密的字符串</param>
        /// <param name="M_Key">密钥</param>
        /// <param name="M_IV">矢量</param>
        /// <returns></returns>
        public static string Encrypt3DES(string M_Text, string M_Key, string M_IV)
        {
            //构造一个对称算法
            SymmetricAlgorithm mcsp = new TripleDESCryptoServiceProvider();
            mcsp.Key = Convert.FromBase64String(M_Key);
            mcsp.IV = Convert.FromBase64String(M_IV);
            //指定加密的运算模式
            mcsp.Mode = System.Security.Cryptography.CipherMode.CBC;
            //获取或设置加密算法的填充模式
            mcsp.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ICryptoTransform ct = mcsp.CreateEncryptor(mcsp.Key, mcsp.IV);
            byte[] byt = Encoding.UTF8.GetBytes(M_Text);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        /// <summary>
        /// 3DES一个解密的方法
        /// </summary>
        /// <param name="Value">要加密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <param name="sIV">矢量</param>
        /// <returns></returns>
        public static string Decrypt3DES(string M_Text, string M_Key, string M_IV)
        {
            //构造一个对称算法
            try
            {
                SymmetricAlgorithm mcsp = new TripleDESCryptoServiceProvider();
                mcsp.Key = Convert.FromBase64String(M_Key);
                mcsp.IV = Convert.FromBase64String(M_IV);
                mcsp.Mode = System.Security.Cryptography.CipherMode.CBC;
                mcsp.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ICryptoTransform ct = mcsp.CreateDecryptor(mcsp.Key, mcsp.IV);
                byte[] byt = Convert.FromBase64String(M_Text);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (System.FormatException e)
            {
                return e.Message;
            }
        }

        public static string Encrypt3DES(string M_Text)
        {
            //构造一个对称算法
            SymmetricAlgorithm mcsp = new TripleDESCryptoServiceProvider();
            mcsp.Key = Convert.FromBase64String(KeyString);
            mcsp.IV = Convert.FromBase64String(IVString);
            //指定加密的运算模式
            mcsp.Mode = System.Security.Cryptography.CipherMode.CBC;
            //获取或设置加密算法的填充模式
            mcsp.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ICryptoTransform ct = mcsp.CreateEncryptor(mcsp.Key, mcsp.IV);
            byte[] byt = Encoding.UTF8.GetBytes(M_Text);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        /// <summary>
        /// 3DES一个解密的方法
        /// </summary>
        /// <param name="Value">要加密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <param name="sIV">矢量</param>
        /// <returns></returns>
        public static string Decrypt3DES(string M_Text)
        {
            try
            {
                //构造一个对称算法
                SymmetricAlgorithm mcsp = new TripleDESCryptoServiceProvider();
                mcsp.Key = Convert.FromBase64String(KeyString);
                mcsp.IV = Convert.FromBase64String(IVString);
                mcsp.Mode = System.Security.Cryptography.CipherMode.CBC;
                mcsp.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ICryptoTransform ct = mcsp.CreateDecryptor(mcsp.Key, mcsp.IV);
                byte[] byt = Convert.FromBase64String(M_Text);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}
