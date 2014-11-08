using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FreezerOrganizer.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            _mockWebClient = new Mock<IWebClient>();
            _dbSerialization = new DatabaseSerialization<TestClass>(_mockWebClient.Object);
            _testData = new List<TestClass>() {
                new TestClass() { Name = "testItem1", Unit = TestClass.Units.pc, Number = 1,  Date = DateTime.UtcNow.Date },
                new TestClass() { Name = "testItem2", Unit = TestClass.Units.dl, Number = -4, Date = DateTime.UtcNow.AddDays(-2) }
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
            public enum Units { pc, dl, g, bag, serv };

            [DataMember()]
            public string Name { get; set; }

            [DataMember()]
            public int Number { get; set; }

            [DataMember()]
            public DateTime Date { get; set; }

            [DataMember()]
            public Units Unit { get; set; }

            public bool Equals(TestClass otherObj)
            {
                return this.Name == otherObj.Name && this.Unit == otherObj.Unit && this.Date == otherObj.Date;
            }
        }
    }
}
