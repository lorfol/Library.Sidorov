using Library.App.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class SetupAssemblyInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
