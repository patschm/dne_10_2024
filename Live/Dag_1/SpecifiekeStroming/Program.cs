
using Bogus;
using System.Xml;
using System.Xml.Serialization;

namespace SpecifiekeStroming;

internal class Program
{
    static void Main(string[] args)
    {
        Person p1 = new Person { Age = 42, Name = "Jan" };
        //WriteToXml(p1);
        //Person p2 = ReadFromXml();
        //Console.WriteLine($"{p2.Name} {p2.Age}");
        //WriteUsingSerializers();
        ReadUsingSerializers();
    }

    private static void ReadUsingSerializers()
    {
        FileStream fs = File.OpenRead(@"E:\Temp\people.xml");
        var rdrset = new XmlReaderSettings();
       rdrset. = NamespaceHandling.Default;
        XmlReader reader = XmlReader.Create(fs);
        XmlSerializer ser = new XmlSerializer(typeof(Person));

        while (reader.ReadToFollowing("person"))
        {
            var p = (Person)ser.Deserialize(reader);
            Console.WriteLine($"{p.Name,-20} {p.Age}");
        }
    }

    private static void WriteUsingSerializers()
    {
        var people = new Bogus.Faker<Person>()
             .RuleFor(p => p.Name, f => f.Name.FullName())
             .RuleFor(p => p.Age, f => f.Random.Int(0, 123))
             .Generate(100)
             .ToList();

        foreach (var person in people)
        {
            //Console.WriteLine($"{person.Name, -20} {person.Age}");
        }

        var pps = new People { Persons = people };
        var file = new FileInfo(@"E:\Temp\people.xml");
        FileStream fs = file.Create();
        XmlWriter writer = XmlWriter.Create(fs);

        XmlSerializer ser = new XmlSerializer(typeof(People));
        ser.Serialize(writer, pps);
    }

    private static Person ReadFromXml()
    {
        FileStream fs = File.OpenRead(@"E:\Temp\person.xml");
        XmlReader reader = XmlReader.Create(fs);
        bool found = reader.ReadToFollowing("person");
        if (found)
        {
            Person p = new Person();
            found = reader.ReadToDescendant("name");
            p.Name = reader.ReadElementContentAsString();
            // Next automatically the age element
           p.Age = reader.ReadElementContentAsInt();
            return p;
        }
        return null;

        //Person? p = null;

        //while (reader.Read())
        //{
        //    if (reader.NodeType == XmlNodeType.Element)
        //    {
        //        switch (reader.Name)
        //        {
        //            case "person":
        //                p = new Person();
        //                break;
        //            case "name":
        //                reader.Read();
        //                p.Name = reader.ReadContentAsString();
        //                break;
        //            case "age":
        //                reader.Read();
        //                p.Age = reader.ReadElementContentAsInt();
        //                break;
        //        }
        //    }
        //}
    }

    private static void WriteToXml(Person p1)
    {
        var file = new FileInfo(@"E:\Temp\person.xml");
        if (file.Exists)
        {
            file.Delete();
        }
        FileStream fs = file.Create();
        XmlWriter writer = XmlWriter.Create(fs);

        writer.WriteStartElement("root");
        writer.WriteStartElement("person");

        writer.WriteStartElement("name");
        writer.WriteString(p1.Name);
        writer.WriteEndElement();

        writer.WriteStartElement(nameof(p1.Age).ToLower());
        writer.WriteString(p1.Age.ToString());
        writer.WriteEndElement();

        writer.WriteEndElement();
        writer.WriteEndElement();

        writer.Flush();
        writer.Close();
    }
}
