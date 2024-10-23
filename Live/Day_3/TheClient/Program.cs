//using MyLib;


using System.Reflection;

namespace TheClient;

internal class Program
{
    static void Main(string[] args)
    {
        //Person person = new Person
        //{
        //    FirstName = "Jan",
        //    LastName = "Hendriks",
        //    Age = 42
        //};
        //person.Introduce();
        //LoadDll();
        DoeErIetsMee();
    }

    private static void DoeErIetsMee()
    {
        Assembly assembly = Assembly.LoadFile(@"E:\Temp\MyLib.dll");
        Type? tp = assembly.GetType("MyLib.Person");
        Console.WriteLine(tp.FullName);
        object? p1 = Activator.CreateInstance(tp);
        PropertyInfo? pFn = tp?.GetProperty("FirstName");
        pFn?.SetValue(p1, "Piet");
        PropertyInfo? pLn = tp?.GetProperty("LastName");
        pLn?.SetValue(p1, "Meesters");
        PropertyInfo? pAge = tp?.GetProperty("Age");
        pAge?.SetValue(p1, 33);

        MethodInfo? intro = tp?.GetMethod("Introduce");
        intro?.Invoke(p1, []);

        FieldInfo? fAge = tp?.GetField("_age", BindingFlags.Instance | BindingFlags.NonPublic);
        Console.WriteLine(fAge?.GetValue(p1));
        fAge?.SetValue(p1, -42);

        intro?.Invoke(p1, []);

        dynamic? p2 = Activator.CreateInstance(tp);
        p2.FirstName = "Marieke";
        p2.LastName = "Otten";
        p2.Age = 41;

        p2.Introduce();
    }

    private static void LoadDll()
    {
        Assembly assembly = Assembly.LoadFile(@"E:\Temp\MyLib.dll");
        Console.WriteLine(assembly.FullName);
        ShowContent(assembly);
    }

    private static void ShowContent(Assembly assembly)
    {
        foreach(var type in assembly.GetTypes())
        {
            Console.WriteLine(type.FullName);
            Console.WriteLine($"Erft van {type.BaseType?.FullName}");
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"Field: {field.Name}");
            }
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"Property: {prop.Name}");
            }
            foreach (var con in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"Constructor: {con.Name}");
            }
            foreach (var meth in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"Method: {meth.Name}");
                foreach(var par in meth.GetParameters())
                {
                    Console.WriteLine($"\t{par.ParameterType.FullName} {par.Name}");
                }
            }
        }
    }
}
