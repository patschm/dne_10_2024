
using System.Globalization;
using System.IO.Compression;
using System.Text;

namespace Stromingen;

internal class Program
{
    static void Main(string[] args)
    {
        //WriteSpartaans();
        //ReadSpaartaans();
        //WriteModern();
        //ReadModern();
        //WriteZipped();
        ReadZipped();
    }

    private static void ReadZipped()
    {
        FileStream fs = File.OpenRead(@"E:\Temp\modern.zip");
        DeflateStream zip = new DeflateStream(fs, CompressionMode.Decompress);
        StreamReader reader = new StreamReader(zip);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
            //Console.ReadKey();
        }
    }

    private static void WriteZipped()
    {
        var file = new FileInfo(@"E:\Temp\modern.zip");
        if (file.Exists)
        {
            file.Delete();
        }
        FileStream fs = file.Create();
        DeflateStream zip = new DeflateStream(fs, CompressionMode.Compress);
        StreamWriter writer = new StreamWriter(zip);

        for (int i = 0; i < 1000; i++)
        {
            writer.WriteLine($"Hello World {i}");
        }
        writer.Flush();
        zip.Close();
        fs.Close();
    }

    private static void ReadModern()
    {
        FileStream fs = File.OpenRead(@"E:\Temp\modern.txt");
        StreamReader reader = new StreamReader(fs);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }

    private static void WriteModern()
    {
        var file = new FileInfo(@"E:\Temp\modern.txt");
        if (file.Exists)
        {
            file.Delete();
        }
        FileStream fs = file.Create();
        StreamWriter writer = new StreamWriter(fs);

        for (int i = 0; i < 1000; i++)
        {
           writer.WriteLine($"Hello World {i}");
        }
        writer.Flush();
        fs.Close();
    }

    private static void ReadSpaartaans()
    {
        FileStream fs =  File.OpenRead(@"E:\Temp\spartaans.txt");
        byte[] buffer = new byte[8];
        int nrByteRead = 0;

        while((nrByteRead = fs.Read(buffer, 0, buffer.Length)) > 0)
        {
            string txt = Encoding.UTF8.GetString(buffer);
            Console.Write(txt);
            Array.Clear(buffer, 0, buffer.Length);
        }

    }

    private static void WriteSpartaans()
    {
        var file = new FileInfo(@"E:\Temp\spartaans.txt");
        if (file.Exists)
        {
            file.Delete();
        }
        FileStream fs = file.Create();

        for (int i = 0; i < 1000; i++)
        {
            byte[] data = Encoding.UTF8.GetBytes($"Hello World {i}\r\n");
            fs.Write(data, 0, data.Length);
        }
        fs.Close();
    }
}
