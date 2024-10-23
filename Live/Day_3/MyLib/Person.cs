namespace MyLib;

//[Obsolete("Deze class niet meer gebruiken", error: true)]
[CanUse(MinMaxAge =42)]
public class Person
{
    private int _age;

    public int Age
    {
        get { return _age; }
        set 
        { 
            if (value >= 0 && value < 125) _age = value; 
        }
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public void Introduce()
    {
        Console.WriteLine($"Hello, I'm {FirstName} {LastName} ({Age})");
    }
}
