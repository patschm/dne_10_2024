namespace Vuilnis;

internal class Program
{
    static Uman um1 = new Uman();
    static Uman um2 = new Uman();

    static void Main(string[] args)
    {
        //um1 = new Uman();
        try
        {
            um1.Open();
        }
        finally
        {
            um1.Dispose();
        }
        
        um1 = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();

        //um2 = new Uman();
        using (um2)
        {
            um2.Open();
        }
        //um2.Dispose();

        Console.ReadLine();
    }
}
