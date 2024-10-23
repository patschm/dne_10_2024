namespace TaskPlusPlus;

internal class Program
{
    static int x;
    static void Main(string[] args)
    {
        x = 20;
        Service svc1 = new Service();
        svc1.Tock += Svc1_Tock;

        svc1.Run();

        Console.ReadLine();
    }

    private static void Svc1_Tock(object? sender, EventArgs e)
    {
        x = 30;
        Console.WriteLine($"Handler: {Thread.CurrentThread.ManagedThreadId}");
    }
}

class Service
{
    public event EventHandler Tock;

    public void Run()
    {
        Task.Run(() =>
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            do
            {
                Task.Delay(2000).Wait();
                Tock?.Invoke(this, new EventArgs());                 
            }
            while (true);
        });
    }
}
