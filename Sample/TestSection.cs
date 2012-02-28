using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public class TestSection
    {
        [XmlAttribute]
        public string Test { get; set; }
    }
}
