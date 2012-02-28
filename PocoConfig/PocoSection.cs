using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PocoConfig
{
    public class PocoSection<T> : ConfigurationSection
    {
        public PocoSection()
        {
            
        } 

        public T Content { get; set; }

        protected override string SerializeSection(ConfigurationElement parentElement, string name,
                                                   ConfigurationSaveMode saveMode)
        {
            var type = typeof (T);
            var serializer = new XmlSerializer(type, new XmlRootAttribute(name));

            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb)) {
                serializer.Serialize(sw, Content);
            }

            return sb.ToString();
        }

        protected override void DeserializeSection(XmlReader reader)
        {
            var type = typeof (T);
            var serializer = new XmlSerializer(type, new XmlRootAttribute(this.SectionInformation.Name));

            var o = serializer.Deserialize(reader);

            Content = (T) o;
        }
    }
}