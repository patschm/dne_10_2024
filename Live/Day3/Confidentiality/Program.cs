using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;

namespace Confidentiality;

internal class Program
{
    static byte[] key;
    static byte[] iv;

    static void Main(string[] args)
    {
        //byte[] crypt = EncryptSymmetric();
        //Console.WriteLine(Encoding.UTF8.GetString(crypt));
        //DecryptSymmetric(crypt);

        // De ontvanger 'koopt' een certificaat.
        RSA rsa = RSA.Create();
        string pubKey = rsa.ToXmlString(false);
        string pubprivKey = rsa.ToXmlString(true);

        byte[] crypt = EncryptAsymmetric(pubKey);
        Console.WriteLine(Encoding.UTF8.GetString(crypt));
        DecryptAsymmetric(crypt, pubprivKey);
    }

    private static void DecryptAsymmetric(byte[] crypt, string cert)
    {
        RSA rsa = RSA.Create();
        rsa.FromXmlString(cert);
        byte[] original = rsa.Decrypt(crypt, RSAEncryptionPadding.Pkcs1);
        Console.WriteLine(Encoding.UTF8.GetString(original  ));
    }

    private static byte[] EncryptAsymmetric(string cert)
    {
        string message = "Hello World";
        RSA rsa = RSA.Create();
        rsa.FromXmlString(cert);
        byte[] cipher = rsa.Encrypt(Encoding.UTF8.GetBytes(message), RSAEncryptionPadding.Pkcs1);
        return cipher;
    }

    private static void DecryptSymmetric(byte[] crypt)
    {
        var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Key = key;
        aes.IV = iv;

        using MemoryStream ms = new MemoryStream(crypt);
        using var cryptstr = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new StreamReader(cryptstr);
        string data = sr.ReadToEnd();
        Console.WriteLine(data);
    }

    private static byte[] EncryptSymmetric()
    {
        string message = "Hello World";
        var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        key = aes.Key;
        iv = aes.IV;

        using MemoryStream memstr = new MemoryStream();
        using var crypt = new CryptoStream(memstr, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using (StreamWriter writer = new StreamWriter(crypt))
        {
            writer.Write(message);
        }
        return memstr.ToArray();
    }
}
