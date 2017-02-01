using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Amaryllis.IPacket.Cryptography
{
    class Expressions
    {
        public static String Replace(String Subject, String Start, String End)
        {
            return Regex.Match(Subject, Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"\s*(((?!" + Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"|" +
                   Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0") + @").)+)\s*" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0"), RegexOptions.IgnoreCase).Value.Replace(Start, "").Replace(End, "");
        }

        public static String MD5(String Input)
        {
            MD5 MD5 = new MD5CryptoServiceProvider();
            MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Input));
            StringBuilder StringBuilder = new StringBuilder();
            for (Int32 i = 0; i < MD5.Hash.Length; i++) StringBuilder.Append(MD5.Hash[i].ToString("x2"));
            return StringBuilder.ToString();
        }

        public static String Swap(String Input)
        {
            try
            {
                return Input.Substring(16, 16) + Input.Substring(0, 16);
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}
