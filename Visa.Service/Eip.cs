using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Service
{
    public class Eip
    {
        #region 将字符串使用base64算法加密
        /// <summary> 
        /// 将字符串使用base64算法加密 
        /// </summary> 
        /// <param name="M_CodeType">编码类型（编码名称） 
        /// * 代码页 名称 
        /// * 1200 "UTF-16LE"、"utf-16"、"ucs-2"、"unicode"或"ISO-10646-UCS-2" 
        /// * 1201 "UTF-16BE"或"unicodeFFFE" 
        /// * 1252 "windows-1252" 
        /// * 65000 "utf-7"、"csUnicode11UTF7"、"unicode-1-1-utf-7"、"unicode-2-0-utf-7"、"x-unicode-1-1-utf-7"或"x-unicode-2-0-utf-7" 
        /// * 65001 "utf-8"、"unicode-1-1-utf-8"、"unicode-2-0-utf-8"、"x-unicode-1-1-utf-8"或"x-unicode-2-0-utf-8" 
        /// * 20127 "us-ascii"、"us"、"ascii"、"ANSI_X3.4-1968"、"ANSI_X3.4-1986"、"cp367"、"csASCII"、"IBM367"、"iso-ir-6"、"ISO646-US"或"ISO_646.irv:1991" 
        /// * 54936 "GB18030" 
        /// </param> 
        /// <param name="M_Text">待加密的字符串</param> 
        /// <returns>加密后的字符串</returns> 
        public static string EncodeBase64(string M_Text)
        {
            string Encode = string.Empty;
            //将一组字符编码为一个字节序列. 
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(M_Text);
                Encode = Convert.ToBase64String(bytes); //将8位无符号整数数组的子集转换为其等效的,以64为基的数字编码的字符串形式. 
            }
            catch
            {
                Encode = M_Text;
            }
            return Encode;
        }

        /// <summary> 
        /// 将字符串使用base64算法解密 
        /// </summary> 
        /// <param name="M_CodeType">编码类型</param> 
        /// <param name="M_Text">已用base64算法加密的字符串</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DecodeBase64(string M_Text)
        {
            string Decode = string.Empty;
            try
            {
                byte[] bytes = Convert.FromBase64String(M_Text); //将2进制编码转换为8位无符号整数数组. 
                Decode = Encoding.UTF8.GetString(bytes); //将指定字节数组中的一个字节序列解码为一个字符串。 
            }
            catch
            {
                Decode = "";
            }
            return Decode;
        }
        #endregion
    }
}
