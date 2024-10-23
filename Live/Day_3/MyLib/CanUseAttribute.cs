namespace MyLib;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class CanUseAttribute : Attribute
{
    public int MinMaxAge { get; set; }

    public bool ValidateUsage()
    {
        if (MinMaxAge >= 18 && MinMaxAge <= 67)
        {
            Console.WriteLine("You can use this type");
            return true;
        }
        else
        {
            Console.WriteLine("You're too old or too young to use this type. Go Away!!");
            return false;
        }
    }

}
