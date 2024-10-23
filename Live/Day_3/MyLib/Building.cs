using System.Reflection;

namespace MyLib;

public class Building
{
    private Person toeschouwer;

    public void Entrance(Person person)
    {
        var inf = person.GetType().GetCustomAttribute<CanUseAttribute>();
        if (inf != null)
        {
            //inf.MinMaxAge
            if (inf.ValidateUsage())
            {
                toeschouwer = person;
            }
        }
       
    }
    public void ShowMovie()
    {
        Console.WriteLine($"{toeschouwer?.LastName} kijkt naar de film");
    }
}
