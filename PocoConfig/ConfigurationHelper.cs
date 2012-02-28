using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PocoConfig
{
    internal class ConfigurationHelper
    {
        private string GetTypeName<TPoco>()
        {
            var type = typeof (PocoSection<TPoco>);

            return type.AssemblyQualifiedName;
        }

        public string GetSectionName(string sectionName)
        {
            if (!string.IsNullOrEmpty(sectionName))
            {
                var levels = sectionName.Split('/');

                return levels.Last();
            }

            return "ERROR: Unknown";
        }

        public IEnumerable<string> GetSectionGroups(string sectionName)
        {
            if (!string.IsNullOrEmpty(sectionName)) {
                var levels = sectionName.Split('/');

                for (int i = 0; i < levels.Length - 1; i++) {
                    yield return levels[i];
                }
            }

        }

        public string GetConfigSection<TPoco>(string sectionName)
        {
            /*
             * 
             * <configSections>
             *      <section name="someName" type=".."/>
             * </configSections>
             * 
             * <someName>
             * </someName>
             * 
             */
             
            var sb = new StringBuilder();

            const string secionString =
                "<section name=\"{0}\" allowDefinition=\"MachineToApplication\" allowExeDefinition=\"MachineToApplication\" allowLocation=\"false\" \n\t\ttype=\"{1}\"/>\n";

            const string sectionGroupString = "{0} <sectionGroup name=\"{1}\">\n";
            const string sectionGroupEndString = "</sectionGroup>";

            var nesting = 0;
            const int indentation = 5;

            foreach (var group in GetSectionGroups(sectionName)) {

                var padding = "".PadLeft(nesting*indentation);
                sb.AppendFormat(CultureInfo.InvariantCulture,
                                padding,
                                sectionGroupString, group);

                nesting++;
            }

            var line = string.Format(CultureInfo.InvariantCulture,
                                     secionString,
                                     GetSectionName(sectionName), 
                                     GetTypeName<TPoco>());

            var typePadding = "".PadLeft((nesting*indentation) + 10);

            line = line.Replace("\t", typePadding);

            sb.Append(line);

            foreach (var group in GetSectionGroups(sectionName).Reverse())
            {
                nesting--;
                var padding = "".PadLeft(nesting * indentation);
                sb.AppendFormat(CultureInfo.InvariantCulture,
                                padding,
                                sectionGroupEndString, group);
                
            }

            return sb.ToString();
        }
    }
}