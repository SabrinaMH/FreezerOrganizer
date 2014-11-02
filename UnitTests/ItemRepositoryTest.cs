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
using FreezerOrganizer.Data;

namespace UnitTests
{
    [TestClass]
    public class ItemRepositoryTest
    {
        private ItemRepository _itemRepository;
        private List<Item> _items;
        private Mock<ISerialization<Item>> _mockSerialization;

        // make sure that each test is run on the same data set (necessary as some tests may modify it)
        [TestInitialize]
        public void InitTest()
        {
            _items = (List<Item>)TestData.GetList();
            _mockSerialization = new Mock<ISerialization<Item>>();
            _itemRepository = new ItemRepository(_items, _mockSerialization.Object);
        }

        [TestMethod]
        public void CreateNewItemTest()
        {
            var item = _itemRepository.CreateNewItem();

            Assert.AreEqual(item.Name, "", "Name isn't empty");
            Assert.AreEqual(item.Number, 0, "Number isn't 0");
            Assert.IsTrue(item.DateOfFreezing.CompareTo(DateTime.Now) < 0, "The item's timestamp isn't earlier than right now");
        }

        [TestMethod]
        public void SearchTest()
        {
            var results = _itemRepository.Search("cookie");
            Assert.IsTrue(results.Count == 2, "Did not find two items containing 'cookie'");
        }

        [TestMethod]
        public void DeleteTest()
        {
            // copy by value such that oldList isn't affected when _items is modified by the Delete method
            var oldList = new List<Item>(_items);
            var itemToRemove = _items[0];

            _itemRepository.Delete(itemToRemove);
            oldList.Remove(itemToRemove);

            Assert.IsTrue(Helpers.EquivalentCollections<Item>(oldList, _items), "The first element wasn't deleted");
        }
        
        [TestMethod]
        public void SaveTest()
        {
            var nameOfDuplicateItem = "gulerod";
            var oldNoItems = _items.Count;

            /* mocks the SerializeList method just so that, if there should be a bug in this method, it doesn't affect this test.
             * The callback ensures that this objects _items reflects the item list in _itemRepository
             * (needed because _items.Except(...) in _itemRepository creates a new list in memory and 
             * then assigns the _items reference in _itemRepository to the new list. Hence the change isn't reflected in this class).
             * */
            _mockSerialization.Setup(x => x.SerializeList(It.IsAny<IList<Item>>(), It.IsAny<string>()))
                .Callback((IList<Item> list, string str) => _items = (List<Item>)list);

            // adds two new items instead of just adding an extra of an already existing item to make the test more self-contained.
            _items.Add(new Item(nameOfDuplicateItem, 1001, Item.Units.pc, DateTime.Today.Subtract(new TimeSpan(2, 0, 0, 0))));
            _items.Add(new Item(nameOfDuplicateItem, 1, Item.Units.bag, DateTime.Today.Subtract(new TimeSpan(2, 0, 0, 0))));

            _itemRepository.Save("");

            Assert.IsTrue(_items.Count == oldNoItems + 1, "Incorrect no items saved");
            Assert.IsTrue(_items.FindAll(item => item.Name == nameOfDuplicateItem).Count == 1, 
                "More (or less) than 1 item of the duplicate were saved");
            Assert.IsTrue(_items.Find(item => item.Name == nameOfDuplicateItem).Number == 1002, "Item number not incremented correctly");

        }
    }
}
