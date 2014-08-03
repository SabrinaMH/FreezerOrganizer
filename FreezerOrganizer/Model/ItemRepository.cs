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

        public ItemRepository() { }

        internal void SaveItems()
        {
            Serialization.SerializeObject<Item>(items);
        }

        internal List<Item> GetAll()
        {
            items = Serialization.DeserializeObject<Item>();
            return items;
        }

        internal void Add(Item newItem)
        {
            items.Add(newItem);
        }

        internal void Remove(string name, int number, DateTime dateOfFreezing)
        {
            var matchingItem = FindItem(name, number, dateOfFreezing);
            if (matchingItem != null)
            {
                items.Remove(matchingItem);
            }
        }

        internal void Update(string name, int number, DateTime dateOfFreezing)
        {
            //itemToUpdate.Update(name, number, dateOfFreezing);
        }

        private Item FindItem(string name, int number, DateTime dateOfFreezing)
        {
            return items.Find(item => item.IsMatch(name, number, dateOfFreezing));
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


