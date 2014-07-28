using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Data;

namespace FreezerOrganizer.Model
{
    // Provides mechanism to interact with storage.
    class ItemRepository
    {
        private static List<Item> items = new List<Item>();

        public ItemRepository()
        {

        }

        internal void SaveItems()
        {
            Serialization.SaveItems(items);
        }

        internal List<Item> GetAll()
        {
            items = Serialization.LoadItems();
            return items;
        }

        internal void Add(Item newItem)
        {
            items.Add(newItem);
        }

        internal void Remove(string name, int number, DateTime dateOfFreezing)
        {
            var matchingItem = items.Find(item => item.Name == name && item.Number == number && item.DateOfFreezing == dateOfFreezing);
            if (matchingItem != null)
            {
                items.Remove(matchingItem);
            }
        }

        internal List<Item> Search(string input)
        {
            return items.FindAll(item => Contains(item.Name, input, StringComparison.OrdinalIgnoreCase));
        }

        // case insensitive search among the item names.
        private bool Contains(string source, string input, StringComparison comp)
        {
            return source.IndexOf(input, comp) >= 0;
        }
    }
}


