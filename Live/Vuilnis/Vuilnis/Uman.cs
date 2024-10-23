
namespace Vuilnis;

internal class Uman : IDisposable
{
    private static bool isOpen = false;
    private FileStream _stream;

    public void Open()
    {
        if (!isOpen)
        {
            Console.WriteLine("Geopend");
            isOpen = true;
            _stream = File.Create("bla.txt");
        }
        else
        {
            Console.WriteLine("Helaas. Uman is in gebruik");
        }
    }
    public void Close()
    {
        Console.WriteLine("Closing....");
        isOpen = false;
    }

    protected void RuimUp(bool fromDispose)
    {
        Close();
        if (fromDispose)
        {
            _stream.Dispose();
        }
    }

    public void Dispose()
    {
        RuimUp(true);
        GC.SuppressFinalize(this);  
    }

    ~Uman()
    {
        RuimUp(false);
    }
}
