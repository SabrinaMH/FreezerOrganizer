using FreezerOrganizer.Data;
using FreezerOrganizer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class SerializationTest
    {
        private Assembly _assembly;
        private const string testDataFileName = "TestData";
        private const string testDataFileType = "xml";
        private string _testDataFullFileName;

        // make sure that each test is run on the same data set (necessary as some tests may modify it)
        [TestInitialize]
        public void InitTest()
        {
            _assembly = Assembly.GetExecutingAssembly();

            var resourceNames = new List<string>(_assembly.GetManifestResourceNames());
            _testDataFullFileName = resourceNames.Find(name => name.Contains(testDataFileName));
        }

        [TestMethod]
        public void DeserializeTest()
        {
            var results = Serialization.DeserializeList<Item>(Helpers.CopyResourceToFile(_testDataFullFileName, testDataFileType, _assembly));
            Assert.IsTrue(results.Count == 3, "Either Search is wrong or three items weren't loaded from the test file.");
        }
    }
}
