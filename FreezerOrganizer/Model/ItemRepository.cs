using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Data;
using System.IO;

namespace FreezerOrganizer.Model
{
    // provides mechanism to interact with storage.
    public class ItemRepository : IRepository<Item>
    {
        private IList<Item> _items;
        private ISerialization<Item> _serialization;

        public ItemRepository() 
        {
            _items = new List<Item>();
            //_serialization = new FileSerialization<Item>();
            _serialization = new DatabaseSerialization<Item>(new WebClientWrapper());
        }

        public ItemRepository(IEnumerable<Item> items, ISerialization<Item> serialization)
        {
            // todo: should I check whether the cast goes wrong?
            _items = (List<Item>)items;
            _serialization = serialization;
        }

        public void Save(string path)
        {
            var duplicateItems = new List<Item>();
            // Check for duplicates. Shouldn't be done in SerializeList, as this should be kept generic.
            foreach (var item in _items)
            {
                if (!duplicateItems.Contains(item))
                {
                    // construct a copy of _items. Needed because _items is of type IList, which doesn't implement FindAll.
                    var tempList = new List<Item>(_items);
                    // otherItem != item, as the list shouldn't contain the item itself
                    var identicalItems = tempList.FindAll(otherItem => otherItem != item && item.Equals(otherItem));
                    foreach (var identicalItem in identicalItems)
                    {
                        item.UpdateNumber(item.Number + identicalItem.Number);
                        duplicateItems.Add(identicalItem);
                    }
                }
            }

            _items = _items.Except(duplicateItems).OrderBy(item => item.Name).ToList();
            _serialization.SerializeList(_items, path);
        }

        public IList<Item> Load(string path)
        {
            _items = (List<Item>)_serialization.DeserializeList(path);
            return _items;
        }

        internal Item CreateNewItem()
        {
            var item = new Item("", 0, default(Item.Units));
            _items.Add(item);
            return item;
        }

        public void Delete(Item item)
        {
            _items.Remove(item);
        }

        public IList<Item> Search(string input)
        {
            // construct a copy of _items. Needed because _items is of type IList, which doesn't implement FindAll.
            var tempList = new List<Item>(_items);
            return tempList.FindAll(item => Contains(item.Name, input, StringComparison.OrdinalIgnoreCase));
        }

        // case insensitive search among the item names.
        private bool Contains(string source, string input, StringComparison comp)
        {
            return source.IndexOf(input, comp) >= 0;
        }
    }
}


