using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FreezerOrganizer.Data;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestClass]
    public class DatabaseSerializationTest
    {
        private Mock<IWebClient> _mockWebClient;
        private DatabaseSerialization<TestClass> _dbSerialization;
        private List<TestClass> _testData;

        [TestInitialize]
        public void InitTest()
        {
            var codePage = "iso-8859-1"; // one.com uses latin1
            _mockWebClient = new Mock<IWebClient>();
            _dbSerialization = new DatabaseSerialization<TestClass>(_mockWebClient.Object, codePage);
            _testData = new List<TestClass>() {
                new TestClass() { Name = "testItem1", Number = 1,  Date = DateTime.UtcNow.Date },
                new TestClass() { Name = "testItem2", Number = -4, Date = DateTime.UtcNow.AddDays(-2) }
            };
        }

        [TestMethod]
        public void GetAll()
        {
            _mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(GenericHelpers.Json<TestClass>.ListToJson(_testData));

            var result = _dbSerialization.DeserializeList("non-empty path");
            Assert.IsTrue(result.Count == _testData.Count, "Incorrent number of objects deserialized");
            for (int i = 0; i < _testData.Count; i++)
            {
                Assert.IsTrue(_testData[i].Equals(result[i]), "Element " + i + " didn't deserialize correctly");
            }
        }

        /* DatabaseSerialization is generic, hence it needs a class. I've created a test class that is very similar to 
         * the Items class, because I wanted to test different data types. However, it's important not to use Item itself
         * as the test should not rely on a particular class.
         * */
        [DataContract()]
        private class TestClass 
        {
            [DataMember()]
            public string Name { get; set; }

            [DataMember()]
            public int Number { get; set; }

            [DataMember()]
            public DateTime Date { get; set; }

            public bool Equals(TestClass otherObj)
            {
                return this.Name == otherObj.Name && this.Number == otherObj.Number && this.Date == otherObj.Date;
            }
        }
    }
}
