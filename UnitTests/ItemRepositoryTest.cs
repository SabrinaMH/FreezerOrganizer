using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FreezerOrganizer.Model;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Text.RegularExpressions;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class ItemRepositoryTest
    {
        private Assembly _assembly;
        private ItemRepository _itemRepo;
        private const string testDataFileName = "TestData";
        private const string testDataFileType = "xml";
        private string testDataFullFileName;
        private List<Item> _items;

        // make sure that each test is run on the same data set (necessary as some tests may modify it)
        [TestInitialize]
        public void InitTest()
        {
            _items = new List<Item>()
            {
                // new TimeSpan(days, hours, minutes, seconds)
                new Item("chokolade cookie", 10, DateTime.Today.Subtract(new TimeSpan(5,0,0,0))),
                new Item("fløde", 0.5, DateTime.Today),
                new Item("carob cookies", 1001, DateTime.Today.Subtract(new TimeSpan(2,0,0,0)))
            };

            _assembly = Assembly.GetExecutingAssembly();
            _itemRepo = new ItemRepository();

            var resourceNames = new List<string>(_assembly.GetManifestResourceNames());
            testDataFullFileName = resourceNames.Find(name => name.Contains(testDataFileName));
        }

        [TestMethod]
        public void CreateNewItemTest()
        {
            var item = _itemRepo.CreateNewItem();

            Assert.AreEqual(item.Name, "", "Name isn't empty");
            Assert.AreEqual(item.Number, 0, "Number isn't 0");
            Assert.IsTrue(item.DateOfFreezing.CompareTo(DateTime.Now) < 0, "The item's timestamp isn't earlier than right now");
        }

        [TestMethod]
        public void SearchTest()
        {
            var itemRepo = new Mock<ItemRepository>();
            itemRepo.Setup(x => x.Load("")).Returns(_items);

            var results = itemRepo.Object.Search("cookie");
            
            Assert.IsTrue(results.Count == 2, "Did not find two items containing 'cookie'");
        }

        [TestMethod]
        public void DeleteTest()
        {
            _itemRepo.Load(Helpers.CopyResourceToFile(testDataFullFileName, testDataFileType, _assembly));
            _itemRepo.Delete(_itemRepo.Search("cookie")[0]);
            var results = _itemRepo.Search("cookie");
            Assert.IsTrue(results.Count == 1, "Either all or none of the cookie items were deleted");
        }
        
        [TestMethod]
        public void SaveTest()
        {
            var outputFile = Helpers.CreateEmptyFile(testDataFileType);
            var testDataFilePath = Helpers.CopyResourceToFile(testDataFullFileName, testDataFileType, _assembly);
            _itemRepo.Load(testDataFilePath);
            _itemRepo.Save(outputFile);
            
            var xmlDiffer = new Microsoft.XmlDiffPatch.XmlDiff(Microsoft.XmlDiffPatch.XmlDiffOptions.IgnoreWhitespace);
            System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create("C:\\Users\\Sabrina\\AppData\\Local\\Temp\\temp.xml");
            var XmlFilesAreEqual = xmlDiffer.Compare(testDataFilePath, Path.GetFullPath(outputFile), true, writer);
            var x = XmlFilesAreEqual;
            //Assert.AreEqual(testDataFormatted, outputFormatted, "The content differ");
        }
    }
}
