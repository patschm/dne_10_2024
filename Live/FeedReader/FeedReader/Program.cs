using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;

namespace FeedReader;

internal class Program
{
    static IEnumerable<int> Numbers()
    {
        Console.WriteLine("Eerste");
        yield return 1;
        Console.WriteLine("Tweede");
        yield return 2;
        Console.WriteLine("Derde");
        yield return 3;
    }
    static async Task Main(string[] args)
    {
        //foreach (var arg in Numbers())
        //{
        //    Console.WriteLine(arg);
        //}

        var http = new HttpClient();
        http.BaseAddress = new Uri("https://nu.nl/");
        var reader = new RssReader(http);
        var items = reader.ReadRssAsync();
        await ShowFeedAsync(items);
    }
    //static async Task Main(string[] args)
    //{
    //    var bldr = Host.CreateApplicationBuilder(args);
    //    bldr.Services.AddHttpClient("nu", opts =>
    //    {
    //        opts.BaseAddress = new Uri("https://nu.nl/");
    //    });
    //    bldr.Services.AddTransient<RssReader>();

    //    var host = bldr.Build();

    //    var reader = host.Services.GetRequiredService<RssReader>();
    //    var items = reader.ReadRssAsync();
    //    await ShowFeedAsync(items);
    //}

    static async Task ShowFeedAsync(IAsyncEnumerable<Item> items)
    {
        await foreach (var item in items)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(item.Category);
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(item.Title);
            Console.ResetColor();
            Console.WriteLine(item.Description);
            Console.WriteLine();
        }
    }
}
