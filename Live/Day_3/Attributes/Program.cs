using MyLib;

namespace Attributes;

[CanUse(MinMaxAge = 67)]
internal class Program
{
    static void Main(string[] args)
    {
        Building bld = new Building();
        Person p = new Person { LastName = "Janssen" };
        Child c = new Child { LastName = "Pieters" };
        bld.Entrance(p);

        bld.Entrance(c);

        bld.ShowMovie();
    }
}
