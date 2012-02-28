using System.Configuration;
using PocoConfig;

namespace ConsoleApplication1
{
    public class Test<T>
    {
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var t = new Test<PocoSection>();

            var c = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var section = c.GetSection("Poco");

            //var typeString = t.GetType().FullName;
        }
    }
}
