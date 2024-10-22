using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace SpecifiekeStroming;

[XmlRoot("people")]
public class People
{
    [XmlElement("person")]
    public List<Person> Persons { get; set; }
}

//[XmlElement("person")]
public class Person
{
    [XmlElement("name")]
    public string? Name { get; set; }
    [XmlElement("age")]
    public int Age { get; set; }
}
