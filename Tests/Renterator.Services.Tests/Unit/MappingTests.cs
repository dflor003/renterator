using AutoMapper;
using NUnit.Framework;

namespace Renterator.Services.Tests.Unit
{
    [TestFixture]
    public class MappingTests
    {
        [Test]
        public void TestMappings()
        {
            MappingHelper.InitializeMappings();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
