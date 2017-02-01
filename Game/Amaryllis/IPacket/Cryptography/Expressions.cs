namespace Amaryllis.IPacket.Cryptography {
    using CryptoLibrary = System.Security.Cryptography;
    using RegularMethods = System.Text.RegularExpressions;
    class Expressions {
        public static string Replace(string Subject, string Start, string End) {
            return RegularMethods.Regex.Match(Subject, RegularMethods.Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"\s*(((?!" + RegularMethods.Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"|" +
                   RegularMethods.Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0") + @").)+)\s*" + RegularMethods.Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0"), RegularMethods.RegexOptions.IgnoreCase).Value.Replace(Start, "").Replace(End, "");
        }

        public static string MD5(string Input) {
            CryptoLibrary.MD5 MD5 = new CryptoLibrary.MD5CryptoServiceProvider();
            MD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Input));
            System.Text.StringBuilder StringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < MD5.Hash.Length; i++) StringBuilder.Append(MD5.Hash[i].ToString("x2"));
            return StringBuilder.ToString();
        }

        public static string Swap(string Input) {
            try {
                return Input.Substring(16, 16) + Input.Substring(0, 16);
            }
            catch {
                return string.Empty;
            }
        }

        public static bool CheckStringContent(string[] Input, string ArrayNeedle) {
            int Position = System.Array.IndexOf(Input, ArrayNeedle);
            if (Position > -1) return true;
            else return false;
        }

        public static bool CheckCollectionContent(System.Collections.Generic.Dictionary<string, string> Input, string ArrayNeedle) {
            for (int i = 0; i < Input.Count; i++) if (Input[i.ToString()] == ArrayNeedle) return true;
            return false;
        }
    }
}
