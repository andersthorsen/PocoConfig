using System;
using System.Configuration;
using System.Globalization;
using System.Runtime.Serialization;

namespace PocoConfig
{
    public static class ConfigurationExtensions
    {
        public static T GetPocoSection<T>(this Configuration configuration, string sectionName)
        {
            var pocoSection = (PocoSection<T>) configuration.GetSection(sectionName);

            if (pocoSection == null) {

                var suggestedString = "";

                try {
                    var helper = new ConfigurationHelper();

                    suggestedString = string.Format(CultureInfo.InvariantCulture,
                                                    "\nPossible reason: Incorrect section configuration in .config file. Suggested configuration: \n{0}",
                                                    helper.GetConfigSection<T>(sectionName));
                }
// ReSharper disable EmptyGeneralCatchClause
                catch {
                    /* Inside Exception handler. Do not confuse by reporting internal errors irrelevant to the real error */
                }
// ReSharper restore EmptyGeneralCatchClause

                throw new PocoSectionNotFouncException(string.Format(CultureInfo.InvariantCulture, "Unable to locate section {0}{1}", sectionName, suggestedString));
            }

            return pocoSection.Content;
        }

        public static void AddPocoSection<T>(this Configuration configuration, string sectionName, T section)
        {
            var pocoSection = new PocoSection<T> {Content = section};

            configuration.Sections.Add(sectionName, pocoSection);
        }
    }

    [Serializable]
    public class PocoSectionNotFouncException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public PocoSectionNotFouncException() {}
        public PocoSectionNotFouncException(string message) : base(message) {}
        public PocoSectionNotFouncException(string message, Exception inner) : base(message, inner) {}

        protected PocoSectionNotFouncException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }
}