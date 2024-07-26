using System.Security.Cryptography;
using System.Text;

namespace Aspnet_mvc.Helpers
{
    public static class Hash
    {
        public static string Crypt(this string val)
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(val);

            array = hash.ComputeHash(array);

            var strHex = new StringBuilder();

            foreach(var item in array)
            {
                strHex.Append(item.ToString("x2"));
            }

            return strHex.ToString();
        }
    }
}
