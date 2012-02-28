using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace PocoConfig.Tests
{
    public class ReadTests
    {
        [Fact]
        public void We_Should_Be_Able_To_Read_A_Section_With_Attribute_On_Root()
        {
            var config = ConfigurationManager.OpenExeConfiguration("SingleSection.config");

            var poco = config.GetPocoSection<SimplePoco>("Poco");

            poco.AnAttribute.Should().Be("Hello");
        }
    }
}
