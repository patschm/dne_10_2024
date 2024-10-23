
using System.Security.Cryptography;
using System.Text;

namespace Integrity;

internal class Program
{
    static void Main(string[] args)
    {
        //TestHash();
        //TestHash();
        //(string Msg, byte[] Hash) pakkertje = HMACSender();
        //HMACOntvanger(pakkertje);

        (string Msg, byte[] Sign, string Pubkey) pakket = DSASender();
        //pakket.Msg += ".";
        DSAOntvanger(pakket);
    }

    private static void DSAOntvanger((string Msg, byte[] Sign, string Pubkey) pakket)
    {
        var alg = SHA1.Create();
        byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(pakket.Msg));

        DSA dsa = DSA.Create();
        dsa.FromXmlString(pakket.Pubkey);
        bool isValid = dsa.VerifyData(hash, pakket.Sign, HashAlgorithmName.SHA1);
        Console.WriteLine(isValid ? "Hij is goed" : "Hij is fout");
    }

    private static (string Msg, byte[] Sign, string pubKey) DSASender()
    {
        string message = "Hello World";
        var alg = SHA1.Create();
        byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(message));

        DSA dsa = DSA.Create();
        string pubKey = dsa.ToXmlString(false);
        byte[] signature = dsa.SignData(hash, HashAlgorithmName.SHA1);

        return (message, signature, pubKey);

    }

    private static void HMACOntvanger((string Msg, byte[] Hash) pakkertje)
    {
        HMACSHA256 hash = new HMACSHA256();
        hash.Key = Encoding.UTF8.GetBytes("Password");
        byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(pakkertje.Msg));

        Console.WriteLine(Convert.ToBase64String(pakkertje.Hash));
        Console.WriteLine(Convert.ToBase64String(result));
    }

    private static (string Msg, byte[] Hash) HMACSender()
    {
        string message = "Hello World";
        HMACSHA256 hash = new HMACSHA256();
        hash.Key = Encoding.UTF8.GetBytes("Password");
        byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(message));

        return (message, result);
    }

    private static void TestHash(string extra = "")
    {
        string message = "Hello World" + extra;

        SHA256 sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
        Console.WriteLine(Convert.ToBase64String(hash));
    }
}
