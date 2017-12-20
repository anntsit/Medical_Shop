using System;
using System.Security.Cryptography;
using System.Text;

namespace MedicaleShop.Utils
{
    public class MathUtil
    {
        public static decimal RoundUp(decimal number, int digits)
        {
            var factor = Convert.ToDecimal(Math.Pow(10, digits));
            return Math.Ceiling(number*factor)/factor;
        }

        public static string Md5(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var t in hash)
                sb.Append(t.ToString("X2"));
            return sb.ToString().ToLower();
        }
    }
}