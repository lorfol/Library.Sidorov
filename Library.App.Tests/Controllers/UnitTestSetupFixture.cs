using Library.App.Mapping;
using NUnit.Framework;

namespace Library.App.Tests.Controllers
{
    [SetUpFixture]
    public class UnitTestSetupFixture
    {
        public UnitTestSetupFixture()
        {

        }

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfiguration.Configure();
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}