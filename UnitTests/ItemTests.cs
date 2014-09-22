using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FreezerOrganizer.Model;

namespace UnitTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void ItemEqualsTest()
        {
            Item item = new Item("item", 5, new DateTime(2014, 10, 30, 1, 1, 1));
            Item sameItem = new Item("item", 5, new DateTime(2014, 10, 30, 2, 2, 2));
            Item differentNameItem = new Item("another item", 5, new DateTime(2014, 10, 30));
            Item differentNumberItem = new Item("item", 3, new DateTime(2014, 10, 30));
            Item differentDateItem = new Item("item", 5, new DateTime(2013, 10, 30));

            bool itemEqualsSameItem = item.Equals(sameItem);
            bool itemEqualsDifferentNameItem = item.Equals(differentNameItem);
            bool itemEqualsDifferentNumberItem = item.Equals(differentNumberItem);
            bool itemEqualsDifferentDateItem = item.Equals(differentDateItem);

            Assert.IsTrue(itemEqualsSameItem, "Equals distingues on time, not just date");
            Assert.IsFalse(itemEqualsDifferentNameItem, "Equals doesn't distingues on item name");
            Assert.IsTrue(itemEqualsDifferentNumberItem, "Equals shouldn't distingues on item number, as it's still the same item.");
            Assert.IsFalse(itemEqualsDifferentDateItem, "Equals doesn't distingues on item date");
        }

        [TestMethod]
        public void UpdateNumberTest()
        {
            Item item = new Item("item", 2, DateTime.Today);
            int extraItems = 10;

            double oldNumber = item.Number;
            item.UpdateNumber(item.Number + extraItems);

            Assert.AreEqual(oldNumber + extraItems, item.Number);
        }
    }
}
