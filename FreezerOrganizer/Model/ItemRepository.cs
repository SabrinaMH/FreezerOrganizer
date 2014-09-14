using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Data;

namespace FreezerOrganizer.Model
{
    // provides mechanism to interact with storage.
    public class ItemRepository
    {
        private List<Item> _items = new List<Item>();

        public ItemRepository() { }

        internal void Save(string path)
        {
            var duplicateItems = new List<Item>();
            // Check for duplicates. Shouldn't be done in SerializeList, as this should be kept generic.
            foreach (var item in _items)
            {
                if (!duplicateItems.Contains(item))
                {
                    // otherItem != item, as the list shouldn't contain the item itself
                    var identicalItems = _items.FindAll(otherItem => otherItem != item && item.Equals(otherItem));
                    foreach (var identicalItem in identicalItems)
                    {
                        item.UpdateNumber(item.Number + identicalItem.Number);
                        duplicateItems.Add(identicalItem);
                    }
                }
            }

            _items = _items.Except(duplicateItems).ToList<Item>();

            Serialization.SerializeList<Item>(_items, path);
        }

        internal List<Item> Load(string path)
        {
            _items = Serialization.DeserializeList<Item>(path);
            return _items;
        }

        internal Item CreateNewItem()
        {
            var item = new Item("", 0, DateTime.Now);
            _items.Add(item);
            return item;
        }

        internal void Delete(Item item)
        {
            _items.Remove(item);
        }

        internal List<Item> Search(string input)
        {
            return _items.FindAll(item => Contains(item.Name, input, StringComparison.OrdinalIgnoreCase));
        }

        // case insensitive search among the item names.
        private bool Contains(string source, string input, StringComparison comp)
        {
            return source.IndexOf(input, comp) >= 0;
        }
    }
}


