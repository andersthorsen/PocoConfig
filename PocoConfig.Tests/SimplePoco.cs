using System.Xml.Serialization;

namespace PocoConfig.Tests
{
    public class SimplePoco
    {
        [XmlAttribute]
        public string AnAttribute { get; set; }

        [XmlElement]
        public string AnElement { get; set; }
    }
}