using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class Criptografo
    {
        public static string TextoCriptografado(string textoSemFormatação)
      {
        MD5 MD5Hasher = MD5.Create();

        byte[] By = Encoding.Default.GetBytes(textoSemFormatação);
        byte[] bytesCriptografo = MD5Hasher.ComputeHash(By);

        StringBuilder SB = new StringBuilder();
        foreach (byte b in bytesCriptografo)
        {
            string DebugB = b.ToString("x2");
            SB.Append(DebugB);
        }

        return SB.ToString();
      } 
    }
}