using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FreezerOrganizer.Data;
using FreezerOrganizer.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTests
{
    [TestClass]
    public class DatabaseSerializationTest
    {
        private class myWebClient : System.Net.WebClient, IWebClient { }
        private DatabaseSerialization<Item> _db;
        private static List<Item> _testData;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var url = "http://cssx.dk/Sabrina/Test/?action=/setup";
            var request = WebRequest.Create(url);
            request.Method = "POST";
            /* Unlike in the UnitTests.DatabaseSerializationTest, we here use the Item test data, because this is needed 
             * in order to test the integration between the C# project and the php REST api, as the latter is written
             * specifically for Item objects.
             * */
            _testData = TestData.GetList();

            // Chose UTF8 as php's json_decode needs to receive valid UTF8.
            var testDataAsBytes = Encoding.UTF8.GetBytes(GenericHelpers.Json<Item>.ListToJson(_testData));
            request.ContentType = "application/json";
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(testDataAsBytes, 0, testDataAsBytes.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void UpdateAll()
        {
            _testData = new List<Item>() { new Item("gulerod", 300, DateTime.Today.AddDays(-6)) };
            //_db = new DatabaseSerialization<Item>(new myWebClient(), "UTF8");
            //_db.SerializeList(_testData, "http://cssx.dk/Sabrina/Test/?action=/setup");
        }


        [ClassCleanup]
        public static void TearDown()
        {
            var url = "http://cssx.dk/Sabrina/Test/?action=/setup";
            var request = WebRequest.Create(url);
            request.Method = "DELETE";
            var response = (HttpWebResponse)request.GetResponse();
        }
    }
}
