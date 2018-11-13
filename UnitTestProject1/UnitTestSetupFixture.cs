using Library.App.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Library.App.Tests.Controllers
{
    [SetUpFixture]
    public class UnitTestSetupFixture
    {

        [AssemblyInitialize]
        public void SetUp()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}